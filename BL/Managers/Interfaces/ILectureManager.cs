using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
   public interface ILectureManager
    {
        Lecture CreateLecture(Lecture Lecture);

        Lecture GetLectureById(int id);

        List<Lecture> GetAll();

        LectureCourseDto GetLectureByIdWithCourses(int id);

        string Delete(Lecture Lecture);
    }
}
