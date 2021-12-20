using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserAccountController : BaseAPIController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public UserAccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUsersDto>> Register(UserRegisterDto registerDto) {
            if(await UniqueUser(registerDto.userName))return BadRequest("User Name Is Taken");

            using var hmac = new HMACSHA512();

            var user = new AppUsers {
                UserName = registerDto.userName,
                UserPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                UserPasswordSalt = hmac.Key
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return new AppUsersDto {
                userName = registerDto.userName,
                token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUsersDto>> Login(UserLoginDto loginDto) {

            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDto.userName);

            if(user == null)return Unauthorized("Invalid Username!");

            using var hmac = new HMACSHA512(user.UserPasswordSalt);

            var userHashFromDataBase = user.UserPasswordHash;

            var userHashFromLoginForm = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for(int i = 0; i<userHashFromDataBase.Length; i++) {
                if(userHashFromDataBase[i] != userHashFromLoginForm[i])return Unauthorized("Invalid Passowrd!");
            }

            return new AppUsersDto {
                userName = loginDto.userName,
                token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UniqueUser(string username) {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}
