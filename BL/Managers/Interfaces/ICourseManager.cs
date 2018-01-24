using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
   public interface ICourseManager
    {
        List<Course> getAll();

        Course getById(int id);

        Course CreateCourse(Course course);

        string Delete(Course course);
    }
}
