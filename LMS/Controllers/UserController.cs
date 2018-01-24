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
        [Route("api/test/createuser")]
        public IHttpActionResult Post(UserRegisterDto user)
        {
            var userDisplay = _userManager.CreateUser(user);
            return Ok(userDisplay);
        }




        [HttpGet]
        [Route("api/test")]
        public IHttpActionResult Test()
        {
            using (LMSEntities context = new LMSEntities())
            {
               
                var students = context.Students.ToList();
                return Ok(students);
            }
        }

    }
}
