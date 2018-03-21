using AutoMapper;
using BL.Managers.Interfaces;
using Data.Database;
using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers
{
    //[Authorize]
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
            if (userDisplay != null)
            {
                return Ok(userDisplay);
            }
            else return Ok("Your Personal ID is not valid, please try again");
        }

        public IHttpActionResult GetCourseListByUser(UserDisplayDto user)
        {
            if (_userManager.getCourseFromUser(user).Count != 0)
            {
                return Ok(Mapper.Map<List<Course>, List<CourseDto>>(_userManager.getCourseFromUser(user)));
            }

            else return Ok("You don't have any courses enrolled");
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
