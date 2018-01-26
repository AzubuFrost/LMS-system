using Data.Database;
using Data.Repositories.Interfaces;
using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(LMSEntities context) : base(context)
        {

        }

        public StudentCourseDto GetStudentByIdWithCourses(int id)
        {
            //其中用_context合并student &studentcourse
            var query = from student in _context.Students
                        join studentcourse in _context.StudentCourses on student.Id equals studentcourse.Id into studentcoursedto
                        select new
                        {
                            StudentId = student.Id,
                            FirstName = student.FirstName,
                            LastName = student.LastName,
                            Gender = student.Gender,
                            DateOfBirth = student.DateOfBirth,
                            Email = student.Email,
                            Credit = student.Credit,
                            CourseId = studentcoursedto.Select(i => i.CourseId).ToList()
                        };
            var results = query.ToList();
            var aim = results.Where(s => s.StudentId == id).FirstOrDefault();
            if (aim == null) return null;
            else
                return new StudentCourseDto
                {
                    Id = aim.StudentId,
                    FirstName = aim.FirstName,
                    LastName = aim.LastName,
                    Gender = aim.Gender,
                    DateOfBirth = aim.DateOfBirth,
                    Email = aim.Email,
                    Credit = aim.Credit,
                    CourseId = aim.CourseId
                };
        }




    }

}
