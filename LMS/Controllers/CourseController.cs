using BL.Managers.Interfaces;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers
{
    [RoutePrefix("api")]
    public class CourseController : ApiController
    {
        private ICourseManager _courseManager;

        public CourseController(ICourseManager courseManager)
        {
            _courseManager = courseManager;
        }
        // GET: api/Course
        [Route("courses")]
        public IHttpActionResult Get()
        {
           
            return Ok(_courseManager.getAll());
        }

        // GET: api/Course/5
        [Route("courses/{id}")]
        public IHttpActionResult GetById(int id)
        {
            var course = _courseManager.getById(id);
            if (course == null)
                return NotFound();
            else return Ok(course);
        }

        // POST: api/Course
        [Route("courses/create")]
        public IHttpActionResult Post(Course course)
        {
            var corse = _courseManager.CreateCourse(course);
            if (course == null)
                return Conflict();
            else return Ok(course);
        }

            // PUT: api/Course/5
            public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Course/5
        [Route("courses/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var course = _courseManager.getById(id);
            if(course ==null) return BadRequest("deleted");
            else return Ok(_courseManager.Delete(course));
            
        }
    }
}
