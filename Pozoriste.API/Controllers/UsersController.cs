using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pozoriste.Domain.Common;
using Pozoriste.Domain.Interfaces;
using Pozoriste.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("username/{username}")]
        public async Task<ActionResult<UserDomainModel>> GetbyUserNameAsync(string username)
        {
            var userModel = await _userService.GetUserByUserName(username);

            if (userModel == null)
            {
                return NotFound(Messages.USER_NOT_FOUND);
            }

            return Ok(userModel);
        }

    }
}
