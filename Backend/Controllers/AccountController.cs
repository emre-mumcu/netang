using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Backend.App_Data;
using Backend.App_Data.DTO;
using Backend.App_Data.Entities;
using Backend.App_Lib.Common;
using Backend.AppLib.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

public class AccountController(AppDbContext appDbContext, ITokenService tokenService) : MyControllerBase
{
    [HttpPost("[action]")]
    public async Task<ApiResponse<UserDto>> Register(RegisterDto registerDto)
    {
        try
        {
            using var hmac = new HMACSHA512();

            User user = new User()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            appDbContext.Users.Add(user);

            await appDbContext.SaveChangesAsync();

            UserDto userDto = new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user.UserName)
            };

            var response = ApiResponses.Success<UserDto>(userDto);
            
            return response;
        }
        catch (Exception ex)
        {
            var response = ApiResponses.Exception<UserDto>(ex);
            return response;
        }
    }
}
