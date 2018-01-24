using BL.Managers.Interfaces;
using Data.Repositories.Interfaces;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
    public class CourseManager : ICourseManager
    {
        private ICourseRepository _courseRepository;

        public CourseManager(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public Course getById(int id)
        {
            if (_courseRepository.Records.Any(c => c.Id == id))
                return _courseRepository.GetById(id);
            else return null;
        }

        public Course CreateCourse(Course course)
        {
            if (_courseRepository.Records.Any(c => c.Title == course.Title))
                
                return null;

            else return _courseRepository.Add(course);
        }

        public string Delete(Course course)
        {
            if (_courseRepository.Records.Any(c => c.Equals(course)))
            {
                _courseRepository.Delete(course);
                return "successfully deleted";
            }
            else return "failed";
        }

       public List<Course> getAll()
        {
            return _courseRepository.GetAll().ToList();
        }
    }
}
