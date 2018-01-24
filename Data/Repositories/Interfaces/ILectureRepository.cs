using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
   public interface ILectureRepository : IGenericRepository<Lecture>
    {
        LectureCourseDto GetLectureByIdWithCourses(int id);
    }
}
