using System;
using System.Linq;
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
    public class UsersAccountController : BaseAPIController
    {
        private readonly DataContext _dataContext;
        private readonly ITokenService _tokenService;

        public UsersAccountController(DataContext dataContext, ITokenService tokenService)
        {
            _dataContext = dataContext;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUsersDto>> Register(UserRegisterDto userRegisterDto) {
            if(await UserUniquenessCheck(userRegisterDto.userName))return BadRequest("The Username is Taken");

            using var hmac = new HMACSHA512();

            var user = new AppUsers {
                UserName = userRegisterDto.userName,
                UserPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userRegisterDto.password)),
                UserPasswordSalt = hmac.Key
            };

            _dataContext.Add(user);
            await _dataContext.SaveChangesAsync();

            return new AppUsersDto {
                userName = user.UserName,
                token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUsersDto>> Login(UserLoginDto userLoginDto) {
            var user = await _dataContext.Users
                .SingleOrDefaultAsync(x => x.UserName.ToLower() == userLoginDto.userName.ToLower());

            using var hmac = new HMACSHA512(user.UserPasswordSalt);

            var HashToCompare = hmac.ComputeHash(Encoding.UTF8.GetBytes(userLoginDto.password));

            var userHash = user.UserPasswordHash;

            for(int i = 0; i<userHash.Length; i++) {
                if(userHash[i] != HashToCompare[i])return Unauthorized("Username or Password is not corrext");
            }

            return new AppUsersDto {
                userName = user.UserName,
                token = _tokenService.CreateToken(user)
            };
        }

        private async Task<bool> UserUniquenessCheck(string userName) {
            return await _dataContext.Users.AnyAsync(x => x.UserName.ToLower() == userName.ToLower());
        }
    }
}
