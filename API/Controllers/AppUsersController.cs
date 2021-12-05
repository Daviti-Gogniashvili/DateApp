using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AppUsersController : BaseAPIController
    {
        private readonly DataContext _context;
        public AppUsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<AppUsers>>> GetUsers() { 
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<AppUsers>> GetUser(int id) { 
            return await _context.Users.FindAsync(id);
        }
    }
}