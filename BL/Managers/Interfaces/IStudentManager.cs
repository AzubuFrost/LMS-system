using BL.Util;
using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
    public interface IStudentManager
    {
        Student CreateStudent(Student student);

        Student GetStudentById(int id);

        List<StudentDto> GetAll();

        StudentCourseDto GetStudentByIdWithCourses(int id);

        string Delete(Student student);

        StudentSearchDto SearchStudent(SearchAttribute search);

        StudentCourse EnrollCourse(StudentCourse studentCourse);
    }

}
