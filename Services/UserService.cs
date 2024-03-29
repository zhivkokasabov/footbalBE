﻿using AutoMapper;
using Core;
using Core.contracts.response;
using Core.Contracts.Request;
using Core.Contracts.Response;
using Core.Contracts.Response.Users;
using Core.InternalObjects;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class UserService : IUsersService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly UserManager<User> UserManager;
        private readonly IMapper Mapper;
        private readonly string UserImagesPath;
        private readonly string ImageFileName;

        public UserService(
            IUnitOfWork unitOfWork,
            UserManager<User> userManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            UnitOfWork = unitOfWork;
            UserManager = userManager;
            Mapper = mapper;
            UserImagesPath = configuration["UserImage"];
            ImageFileName = configuration["ImageFileName"];
        }

        public async Task<LoginDto> CreateUser(UserDto user)
        {
            var userModel = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                NormalizedEmail = user.Email.ToUpper(),
                NormalizedUserName = user.Email.ToUpper(),
                Email = user.Email,
                UserName = user.Email,
                Nickname = user.Nickname,
                LockoutEnabled = true,
                Active = true,
                UserPositions = user.Positions.Select(x => new UserPosition
                {
                    Active = true,
                    PlayerPositionId = x.Id
                }).ToList()
            };
            userModel.PasswordHash = UserManager.PasswordHasher.HashPassword(userModel, user.Password);

            var result = await UserManager.CreateAsync(userModel, user.Password);

            await UserManager.AddToRolesAsync(userModel, user.Roles.Select(x => x.Name));

            var roles = await UserManager.GetRolesAsync(userModel);

            userModel.UserRoles = roles.Select(role => new UserRole
            {
                Role = new Role
                {
                    Name = role
                }
            }).ToList();

            if (!result.Succeeded)
            {
                var errors = new ErrorResponse();

                foreach (var error in result.Errors)
                {
                    errors.Errors.Add(
                        new ErrorModel
                        {
                            Error = error.Description
                        });
                }

                return new LoginDto
                {
                    Errors = errors
                };
            }

            var newUser = await UnitOfWork.Users.GetUserAsync(user.Email);

            UploadImage(newUser.Id, user.Picture);

            return new LoginDto
            {
                User = newUser
            };
        }

        public async Task<LoginDto> Login(UserDto user)
        {
            var foundUser = await UserManager.FindByEmailAsync(user.Email);
            var passwordIsCorrect = await UserManager.CheckPasswordAsync(foundUser, user.Password);

            if (passwordIsCorrect)
            {
                var roles = await UserManager.GetRolesAsync(foundUser);

                foundUser.UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        Role = new Role
                        {
                            Name = roles.First()
                        }
                    }
                };

                return new LoginDto
                {
                    User = foundUser
                };
            }

            return new LoginDto
            {
                Errors = new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel
                        {
                            Error = "Wrong credentials!"
                        }
                    }
                }
            };
        }

        public string GenerateToken(User user)
        {
            //var user = await UserManager.FindByEmailAsync(username);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRoles.First().Role.Name),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")),
                        SecurityAlgorithms.HmacSha256
                        )
                    ),
                    new JwtPayload(claims)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<UserOutputDto> GetUser(string userName)
        {
            var user = await UnitOfWork.Users.GetUserAsync(userName);

            if (File.Exists($"{UserImagesPath}{user.Id}/{ImageFileName}"))
            {
                var text = File.ReadAllText($"{UserImagesPath}{user.Id}/{ImageFileName}");

                user.Picture = text;
            }

            var mapped = Mapper.Map<UserOutputDto>(user);

            return mapped;
        }

        public async Task<UpdateProfileModel> UpdateUser(UserDto userDto)
        {
            var response = new UpdateProfileModel();
            var user = await UserManager.FindByEmailAsync(userDto.Email);
            var roles = await UserManager.GetRolesAsync(user);
            var passwordIsCorrect = await UserManager.CheckPasswordAsync(user, userDto.Password);

            if (!passwordIsCorrect)
            {
                response.Errors.Add(new ErrorModel { Error = "Wrong password provided" });

                return response;
            }

            var positions = await UnitOfWork.Users.GetUserPositionsAsync(user.Id);

            positions.ForEach(x =>
            {
                x.Active = true;
            });

            var newPositions = userDto.Positions.Select(x => new UserPosition
            {
                PlayerPositionId = x.Id,
                PlayerPosition = x
            })
            .ToList();

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Nickname = userDto.Nickname;
            user.UserPositions = newPositions;

            UploadImage(user.Id, userDto.Picture);

            await UserManager.UpdateAsync(user);
            await UnitOfWork.CommitAsync();

            user.Picture = userDto.Picture;

            user.UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        Role = new Role
                        {
                            Name = roles.First()
                        }
                    }
                };

            response.User = user;

            return response;
        }

        private void UploadImage(int userId, string picture)
        {
            if (!Directory.Exists($"{UserImagesPath}{userId}"))
            {
                Directory.CreateDirectory($"{UserImagesPath}{userId}");
            }

            if (File.Exists($"{UserImagesPath}{userId}/{ImageFileName}"))
            {
                using (FileStream fs = File.Open($"{UserImagesPath}{userId}/{ImageFileName}", FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    var bytes = Encoding.ASCII.GetBytes(picture);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
            else
            {
                using (FileStream fs = File.Create($"{UserImagesPath}{userId}/{ImageFileName}"))
                {
                    var bytes = Encoding.ASCII.GetBytes(picture);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }
        }
    }
}
