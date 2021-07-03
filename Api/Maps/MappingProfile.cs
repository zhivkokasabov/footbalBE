using Api.Controllers;
using AutoMapper;
using Core.contracts.Request;
using Core.contracts.Response;
using Core.Contracts.Response;
using Core.Contracts.Response.Notifications;
using Core.Contracts.Response.Teams;
using Core.Contracts.Response.Tournaments;
using Core.Contracts.Response.Users;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Add as many of these lines as you need to map your objects
        CreateMap<TournamentDto, Tournament>()
            .ForMember(
                x => x.HalfTimeLength,
                opt => opt.MapFrom(src => new TimeSpan(0, src.HalfTimeLength, 0))
            )
            .ForMember(
                x => x.MatchLength,
                opt => opt.MapFrom(src => new TimeSpan(0, src.MatchLength, 0))
            )
            .ForMember(
                x => x.FirstMatchStartAt,
                opt => opt.MapFrom(src => DateTime.ParseExact(src.FirstMatchStartsAt, "H:mm", null, System.Globalization.DateTimeStyles.None))
            )
            .ForMember(
                x => x.LastMatchStartsAt,
                opt => opt.MapFrom(src => DateTime.ParseExact(src.LastMatchStartsAt, "H:mm", null, System.Globalization.DateTimeStyles.None))
            );

        CreateMap<Tournament, TournamentOutputDto>()
            .ForMember(
                x => x.HalfTimeLength,
                opt => opt.MapFrom(src => src.HalfTimeLength.Minutes))
            .ForMember(
                x => x.MatchLength,
                opt => opt.MapFrom(src => src.MatchLength.Minutes))
            .ForMember(
                x => x.FirstMatchStartsAt,
                opt => opt.MapFrom(src => $"{src.FirstMatchStartAt.Hour}:{src.FirstMatchStartAt.Minute}"))
            .ForMember(
                x => x.LastMatchStartsAt,
                opt => opt.MapFrom(src => $"{src.LastMatchStartsAt.Hour}:{src.LastMatchStartsAt.Minute}"))
            .ForMember(
                x => x.EnrolledTeams,
                opt => opt.MapFrom(src => src.TournamentParticipants.Where(tp => tp.TeamId != null).Count()));

        CreateMap<TournamentParticipant, TournamentParticipantOutputDto>();
        CreateMap<TournamentMatch, TournamentMatchOutputDto>();

        CreateMap<Role, RoleDto>();
        CreateMap<RoleDto, Role>();

        CreateMap<User, UserOutputDto>()
            .ForMember(
                x => x.Positions,
                opt => opt.MapFrom(src => src.UserPositions.Select(x => new PlayerPosition {
                    Position = x.PlayerPosition.Position,
                    Id = x.PlayerPosition.Id
                })))
            .ForMember(
                x => x.Roles,
                opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role)));

        CreateMap<Team, TeamOutputDto>()
            .ForMember(
                x => x.CaptainName,
                opt => opt.MapFrom(src => src.Members
                    .FirstOrDefault(x => x.IsTeamCaptain).Nickname)
            )
            .ForMember(
                x => x.Id,
                opt => opt.MapFrom(src => src.TeamId)
            );

        CreateMap<TournamentMatchTeam, TournamentMatchTeamOutputDto>()
            .ForMember(
                x => x.Name,
                opt => opt.MapFrom(src => src.Team.Name)
            );

        CreateMap<Notification, NotificationDto>()
            .ReverseMap();
    }
}