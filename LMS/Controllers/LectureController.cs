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
    public class LectureController : ApiController
    {
        private readonly ILectureManager _lectureManager;

        public LectureController(ILectureManager LectureManager)
        {
            _lectureManager = LectureManager;
        }
        // GET: api/Lecture
        [Route("Lecture/{id}/withcourses")]
        public IHttpActionResult GetLectureByIdWithCourses(int id)
        {
            var lecturecoursedto = _lectureManager.GetLectureByIdWithCourses(id);
            if (lecturecoursedto == null) return BadRequest();
            else return Ok(_lectureManager.GetLectureByIdWithCourses(id));
        }

        //GET: api/Lecture/5
        [Route("Lectures")]
        public IHttpActionResult GetAll()
        {
            return Ok(_lectureManager.GetAll());
        }

        [Route("Lectures/{id}")]
        public IHttpActionResult GetLecturesById(int id)
        {
            var Lecture = _lectureManager.GetLectureById(id);
            if (Lecture == null) return BadRequest();
            else return Ok(Lecture);
        }

        // POST: api/Lecture
        [Route("Lectures/create")]
        public IHttpActionResult Post(Lecture Lecture)
        {
            var stu = _lectureManager.CreateLecture(Lecture);
            if (stu != null) return Ok(Lecture);
            else return Conflict();
        }

        // PUT: api/Lecture/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Lecture/5
        public IHttpActionResult Delete(int id)
        {
            var Lecture = _lectureManager.GetLectureById(id);
            if (Lecture == null) return BadRequest();
            else return Ok(_lectureManager.Delete(Lecture));

        }
    }
}
