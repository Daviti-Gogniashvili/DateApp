using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseAPIController {
        private readonly DataContext _context;

        public BuggyController(DataContext context) {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret() {
            return "Error of Auth Text";
        }
        
        [HttpGet("not-found")]
        public async Task<ActionResult<AppUsers>> GetNotFound() {
            if(await Thing() == null) return NotFound();

            return Ok(Thing());
        }

        [HttpGet("server-error")]
        public async Task<ActionResult<string>> GetServerError() {
            var thingToReturn = await Thing();

            return thingToReturn.ToString();
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest() {
            return BadRequest("Bad Request");
        }

        public async Task<AppUsers> Thing() {
            var thing = await _context.Users.FindAsync(-1);
            return thing;
        }
    }
}