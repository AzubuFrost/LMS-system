using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
   public interface ILecturerManager
    {
        Lecturer CreateLecture(Lecturer Lecture);

        Lecturer GetLectureById(int id);

        List<Lecturer> GetAll();

        LecturerCourseDto GetLectureByIdWithCourses(int id);

        string Delete(Lecturer Lecture);

        LecturerCourse EnrollCourse(LecturerCourse lectureCourse);

        Lecturer ModifyDetails(Lecturer lecturer);
    }
}
