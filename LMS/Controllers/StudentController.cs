using AutoMapper;
using BL.Managers.Interfaces;
using BL.Util;
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
    [RoutePrefix("api")]
    public class StudentController : ApiController
    {
        private readonly IStudentManager _studentManager;

        public StudentController(IStudentManager StudentManager)
        {
            _studentManager = StudentManager;
        }
        // GET: api/Student
       
        [Route("students/{id}/withcourses")]
        public IHttpActionResult GetStudentByIdWithCourses(int id)
        {
            var studentcoursedto = _studentManager.GetStudentByIdWithCourses(id);
            if (studentcoursedto == null) return BadRequest();
           else return Ok(_studentManager.GetStudentByIdWithCourses(id));
        }

        //GET: api/Student/5
        [Route("students/getall")]
        public IHttpActionResult GetAll()
        {
            var students = _studentManager.GetAll();
            return Ok(students);
        }

        [Route("students/{id}")]
        public IHttpActionResult GetStudentsById(int id )
        {
            var student = _studentManager.GetStudentById(id);
            if (student == null) return BadRequest();
            else return Ok(Mapper.Map<Student,StudentDto>(student));
        }

        // POST: api/Student
        [Route("students/create")]
        public IHttpActionResult Post(Student student)
        {
            var stu = _studentManager.CreateStudent(student);
            if (stu != null) return Ok(student);
            else return Conflict();
        }

        // PUT: api/Student/5
        [HttpPut]
        [Route("students")]
        public IHttpActionResult ModifiyDetails([FromBody]Student student)
        {
            var st = _studentManager.ModifyDetails(student);
            if (st != null)
                return Ok(Mapper.Map<Student, StudentDto>(student));
            else return BadRequest();   
        }

        // DELETE: api/Student/5
        [HttpPost]
        [Route("students/delete")]
        public IHttpActionResult Delete(int id)
        {
            var student = _studentManager.GetStudentById(id);
            if (student == null) return BadRequest();
            else return Ok(_studentManager.Delete(student));

           
        }
        //string sortString = "id", string sortOrder = "asc", string searchValue = "", int pageSize = 10, int pageNumber = 1
        [Route("students")]
        public IHttpActionResult SortStudent(string sortString = "id", string sortOrder = "asc", string searchValue = "", int pageSize = 10, int pageNumber = 1)
        {
            SearchAttribute search = new SearchAttribute
            {
                SearchValue = searchValue,
                SortOrder = sortOrder,
                SortString = sortString,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            StudentSearchDto students = _studentManager.SearchStudent(search);

            return Ok(students);
        }
        [Route("students/enroll")]
        public IHttpActionResult Enroll(StudentCourse studentCourse)
        {
            var temp = _studentManager.EnrollCourse(studentCourse);

            if (temp == null) return Ok("already enrolled");

            else return Ok(temp);
        }
    }
}
