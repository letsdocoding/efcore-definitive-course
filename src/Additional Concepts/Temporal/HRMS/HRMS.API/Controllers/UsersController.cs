using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using HRMS.API.Models;
using HRMS.Dal;
using HRMS.Dal.Contracts.Entities;
using HRMS.Dal.Migrations.MsSql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HrmsDbContext _dbContext;

        public UsersController(HrmsDbContext  dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("{id}")]
        public ActionResult GetUserById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(b => b.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(user), "application/json");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody]UserUpdateModel updateModel)
        {
            var user = _dbContext.Users.FirstOrDefault(b => b.UserId == id);
            if (user == null)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(updateModel.FirstName))
            {
                user.FirstName = updateModel.FirstName;
            }
            if (!string.IsNullOrEmpty(updateModel.LastName))
            {
                user.LastName = updateModel.LastName;
            }
            if (!string.IsNullOrEmpty(updateModel.OfficeName))
            {
                user.OfficeName = updateModel.OfficeName;
            }

            var updateResult = _dbContext.SaveChanges() > 0;
            return Content(updateResult ? "true" : "no-change");
        }
    }
}
