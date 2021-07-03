using AutoMapper;
using Core.contracts.response;
using Core.Contracts.Response.Notifications;
using Core.Enums;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Notifications
{
    public class NotificationsService : INotificationsService
    {
        private IUnitOfWork UnitOfWork { get; }
        private IMapper Mapper { get; }
        private ITournamentsService TournamentsService { get; }

        public NotificationsService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ITournamentsService tournamentsService)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            TournamentsService = tournamentsService;
        }

        public async Task<List<NotificationType>> GetNotificationTypes()
        {
            return await UnitOfWork.Notifications.GetNotificationTypes();
        }

        public async Task<List<NotificationDto>> GetUserNotifications(int userId)
        {
            var notifications = await UnitOfWork.Notifications.GetUserNotifications(userId);
            
            return Mapper.Map<List<NotificationDto>>(notifications);
        }

        public async Task<NotificationsCountDto> GetNotificationsCount(int userId)
        {
            return new NotificationsCountDto
            {
                IncomingRequests = await UnitOfWork.Notifications.GetIncomingNotificationsCount(userId),
                OutgoingRequest = await UnitOfWork.Notifications.GetOutgoinNotificationsCount(userId)
            };
        }

        public async Task<List<ErrorModel>> ResolveRequest(NotificationDto notification, int userId)
        {
            var resolver = await ResolveRequestFactoryMethod(notification, userId);

            var dbNotification = await UnitOfWork.Notifications.GetByIdAsync(notification.Id);
            dbNotification.Active = false;

            UnitOfWork.Notifications.Update(dbNotification);
            await UnitOfWork.CommitAsync();

            return resolver;
        }

        private async Task<List<ErrorModel>> ResolveRequestFactoryMethod(NotificationDto notification, int userId)
        {
            var errors = new List<ErrorModel>();

            switch (notification.NotificationTypeId)
            {
                case (int)NotificationTypes.RequestToJoinTournament:
                    return await ResolveRequestToJoinTournament(notification, userId, errors);
                case (int)NotificationTypes.Alert:
                    return await ResolveRequestRejected(notification, errors);
                default:
                    return new List<ErrorModel>();
            }
        }

        private async Task<List<ErrorModel>> ResolveRequestRejected(NotificationDto notification, List<ErrorModel> errors)
        {
            var dbNotification = await UnitOfWork.Notifications.GetByIdAsync(notification.Id);

            if (notification == null) {
                errors.Add(
                    new ErrorModel
                    {
                        Error = "Notification does not exist"
                    });
            }

            dbNotification.Active = false;

            await UnitOfWork.CommitAsync();

            return errors;
        }

        private async Task<List<ErrorModel>> ResolveRequestToJoinTournament(NotificationDto notification, int userId, List<ErrorModel> errors)
        {
            var tournament = await UnitOfWork.Tournaments.GetTournamentQueryable()
                .Select(x => new {
                    Name = x.Name,
                    Id = x.TournamentId
                }).FirstOrDefaultAsync(x => x.Id == notification.EntityId);

            if (notification.Accepted)
            {
                var dbNotification = await UnitOfWork.Notifications.GetByIdAsync(notification.Id);
                var response = await TournamentsService.JoinTournament(tournament.Id, notification.SenderId, true);

                if (response.Any())
                {
                    return response;
                }

                dbNotification.Active = false;

                var acceptNotification = new Notification
                {
                    Message = $"Your request to join tournament '{tournament.Name}' has been accepted",
                    NotificationTypeId = (int)NotificationTypes.Alert,
                    ReceiverId = notification.SenderId,
                    SenderId = userId
                };

                await UnitOfWork.Notifications.AddAsync(acceptNotification);

                await UnitOfWork.CommitAsync();

                return response;
            }

            var rejectNotification = new Notification
            {
                Message = $"Your request to join tournament '{tournament.Name}' has been declined",
                NotificationTypeId = (int)NotificationTypes.Alert,
                ReceiverId = notification.SenderId,
                SenderId = userId
            };

            await UnitOfWork.Notifications.AddAsync(rejectNotification);

            return errors;
        }
    }
}
