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
        private readonly IStudentManager _StudentManager;

        public StudentController(IStudentManager StudentManager)
        {
            _StudentManager = StudentManager;
        }
        // GET: api/Student
       
        [Route("students/{id}/withcourses")]
        public IHttpActionResult GetStudentByIdWithCourses(int id)
        {
            var studentcoursedto = _StudentManager.GetStudentByIdWithCourses(id);
            if (studentcoursedto == null) return BadRequest();
           else return Ok(_StudentManager.GetStudentByIdWithCourses(id));
        }

        //GET: api/Student/5
        [Route("students/getall")]
        public IHttpActionResult GetAll()
        {
            var students = _StudentManager.GetAll();
            return Ok(students);
        }

        [Route("students/{id}")]
        public IHttpActionResult GetStudentsById(int id )
        {
            var student = _StudentManager.GetStudentById(id);
            if (student == null) return BadRequest();
            else return Ok(Mapper.Map<Student,StudentDto>(student));
        }

        // POST: api/Student
        [Route("students/create")]
        public IHttpActionResult Post(Student student)
        {
            var stu = _StudentManager.CreateStudent(student);
            if (stu != null) return Ok(student);
            else return Conflict();
        }

        // PUT: api/Student/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Student/5
        [Route("students/delete/{id}")]
        public IHttpActionResult Delete(int id)
        {
            var student = _StudentManager.GetStudentById(id);
            if (student == null) return BadRequest();
            else return Ok(_StudentManager.Delete(student));

           
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
            StudentSearchDto students = _StudentManager.SearchStudent(search);

            return Ok(students);
        }
    }
}
