﻿using AutoMapper;
using BL.Managers.Interfaces;
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
    [RoutePrefix("api")]
    public class CourseController : ApiController
    {
        private ICourseManager _courseManager;

        public CourseController(ICourseManager courseManager)
        {
            _courseManager = courseManager;
        }
        // GET: api/Course
        [Route("courses/getall")]
        public IHttpActionResult Get()
        {
           
            return Ok(Mapper.Map<List<Course>,List<CourseDto>>(_courseManager.getAll()));
        }

        // GET: api/Course/5
        [Route("courses/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var course = _courseManager.getById(id);
            if (course == null)
                return NotFound();
            else return Ok(Mapper.Map<Course,CourseDto>(course));
        }

        // POST: api/Course
        [HttpPost]
        [Route("courses/create")]
        public IHttpActionResult Post(Course course)
        {
            var corse = _courseManager.CreateCourse(course);
            if (course == null)
                return Conflict();
            else return Ok(course);
        }

            // PUT: api/Course/5
            [HttpPut]
            [Route("courses")]
        public IHttpActionResult Put([FromBody]Course course)
        {

            var cr = _courseManager.ModifyDetails(course);
            if (cr != null)

                return Ok(Mapper.Map<Course, CourseDto>(course));

            else return BadRequest();
        }

        // DELETE: api/Course/5
        [HttpDelete]
        [Route("courses/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var course = _courseManager.getById(id);
            if(course ==null) return BadRequest("deleted");
            else return Ok(_courseManager.Delete(course));
            
        }
    }
}
