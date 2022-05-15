using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using HRMS.API.Models;
using HRMS.Dal;
using HRMS.Dal.Contracts.Entities;
using HRMS.Dal.Migrations.MsSql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("{id}/all")]
        public ActionResult GetUserByIdALlChanges(int id)
        {
            var user = _dbContext.Users
                .TemporalAll()
                .Where(b => b.UserId == id)
                .Select(b=> new 
                {
                    User = b,
                    From = EF.Property<DateTime>(b, "PeriodStart"),
                    Till= EF.Property<DateTime>(b, "PeriodEnd"),
                })
                .OrderBy(x=>x.From)
                .ToList();
            if (!user .Any())
            {
                return NotFound();
            }

            return Content(JsonSerializer.Serialize(user), "application/json");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody]UserUpdateModel updateModel)
        {
            var user = _dbContext.Users.FirstOrDefault(b => b.UserId == id);
            var hasChanges = false;
            if (user == null)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(updateModel.FirstName))
            {
                user.FirstName = updateModel.FirstName;
                hasChanges = true;
            }
            if (!string.IsNullOrEmpty(updateModel.LastName))
            {
                user.LastName = updateModel.LastName;
                hasChanges = true;
            }
            if (!string.IsNullOrEmpty(updateModel.OfficeName))
            {
                user.OfficeName = updateModel.OfficeName;
                hasChanges = true;
            }

            if (hasChanges)
            {
                user.ModifiedOn = DateTimeOffset.Now;
                user.ModifiedBy = "app_user";
            }

            var updateResult = _dbContext.SaveChanges() > 0;
            return Content(updateResult ? "true" : "no-change");
        }
    }
}
