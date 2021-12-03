using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UserAccountController : BaseAPIController
    {
        private readonly DataContext _context;
        public UserAccountController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AppUsers>> Register(RegisterDto registerDto) {
            
            if(await UserUniquenessCheck(registerDto.userName))return BadRequest("The Username is Taken");
            using var hmac = new HMACSHA512();

            var user = new AppUsers {
                UserName = registerDto.userName,
                UserPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                UserPasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUsers>> Login(LoginDto loginDto) {
            var user = await _context.Users
                .SingleOrDefaultAsync(x => x.UserName == loginDto.userName);
            if(user == null)return Unauthorized("Invalid User Name");

            using var hmac = new HMACSHA512(user.UserPasswordSalt);

            var Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));

            for(int i = 0; i<Hash.Length; i++) {
                if(Hash[i] != user.UserPasswordHash[i])return Unauthorized("Invalid Password");
            }

            return user;

        }

        private async Task<bool> UserUniquenessCheck(string username) {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}