using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS.Dal;
using HRMS.Dal.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRMS.API.Controllers
{
    [Route("api/usermanager")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        //WARNING: DO NOT USE CODE BELOW. THIS IS TEST CODE
        private readonly HrmsDbContext _dbContext;

        public UserManagerController(HrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAllRoles")]
        public IList<UserRole> GetAllRoles()
        {
            return _dbContext.UserRoles.ToList();
        }

        [HttpPost("UpdateUser")]
        public async Task<IList<User>> UpdateUser(int id = 1)
        {
            var user = _dbContext.Users.FirstOrDefault(b => b.UserId == id);
            user.DateOfBirth = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            var allUsers = await _dbContext.Users.ToListAsync();
            return allUsers;
        }

        [HttpGet("GetAllHistory")]
        public async Task<dynamic>  GetAllHistory(int id = 1)
        {
            var history = await _dbContext.Users
                .TemporalAll()
                .Where(e => e.UserId == id)
                .OrderBy(e => EF.Property<DateTime>(e, "StartDate"))
                .Select(
                    e => new
                    {
                        Employee = e,
                        ValidFrom = EF.Property<DateTime>(e, "StartDate"),
                        ValidTo = EF.Property<DateTime>(e, "EndDate")
                    })
                .ToListAsync();

            return history;
        }

    }
}
