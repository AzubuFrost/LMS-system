using AutoMapper;
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
    public class LecturerController : ApiController
    {
        private readonly ILecturerManager _lectureManager;

        public LecturerController(ILecturerManager LectureManager)
        {
            _lectureManager = LectureManager;
        }
        // GET: api/Lecture
        [Route("lecturers/{id}/withcourses")]
        public IHttpActionResult GetLectureByIdWithCourses(int id)
        {
            var lecturecoursedto = _lectureManager.GetLectureByIdWithCourses(id);
            if (lecturecoursedto == null) return BadRequest();
            else return Ok(_lectureManager.GetLectureByIdWithCourses(id));
        }

        //GET: api/Lecture/5
        [Route("lecturers/getall")]
        public IHttpActionResult GetAll()
        {
            return Ok(Mapper.Map<List<Lecturer>,List<LecturerDto>>(_lectureManager.GetAll()));
        }

        [Route("lecturers/{id}")]
        public IHttpActionResult GetLecturesById(int id)
        {
            var Lecture = _lectureManager.GetLectureById(id);
            if (Lecture == null) return BadRequest();
            else return Ok(Mapper.Map<Lecturer,LecturerDto>(Lecture));
        }

        // POST: api/Lecture
        [Route("lecturers/create")]
        public IHttpActionResult Post(Lecturer Lecture)
        {
            var stu = _lectureManager.CreateLecture(Lecture);
            if (stu != null) return Ok(Lecture);
            else return Conflict();
        }

        // PUT: api/Lecture/5
        [HttpPut]
        [Route("lecturers")]
        public IHttpActionResult Put([FromBody]Lecturer lecturer)
        {
            var lec = _lectureManager.ModifyDetails(lecturer);
            if (lec != null)

                return Ok(Mapper.Map<Lecturer, LecturerDto>(lecturer));

            else return BadRequest();
        }

        // DELETE: api/Lecture/5
        [HttpDelete]
        [Route("lecturers/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var Lecture = _lectureManager.GetLectureById(id);
            if (Lecture == null) return BadRequest();
            else return Ok(_lectureManager.Delete(Lecture));

        }
        [Route("lecturers/enroll")]
        public IHttpActionResult Enroll(LecturerCourse lectureCourse)
        {
            var temp = _lectureManager.EnrollCourse(lectureCourse);

            if (temp == null) return Ok("already enrolled");

            else return Ok(temp);
        }
    }
}
