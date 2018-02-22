using BL.Managers.Interfaces;
using Data.Database;
using Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("api/user/createuser")]
        public IHttpActionResult Post(UserRegisterDto user)
        {
            var userDisplay = _userManager.CreateUser(user);
            return Ok(userDisplay);
        }




        [HttpPost]
        [Route("api/user/login")]
        public IHttpActionResult Login(String username,String password)
        {
            var user = _userManager.FindUser(username, password);

            if (user == null)

                return NotFound();

            else return Ok(user);
        }

    }
}
