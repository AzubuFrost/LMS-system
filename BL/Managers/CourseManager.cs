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
        private IStudentCourseRepository _studentCourseRepository;
        private ILectureCourseRepository _lectureCourseRepository;

        public CourseManager(ICourseRepository courseRepository, IStudentCourseRepository studentCourseRepository, ILectureCourseRepository lectureCourseRepository)
        {
            _courseRepository = courseRepository;
            _studentCourseRepository = studentCourseRepository;
            _lectureCourseRepository = lectureCourseRepository;
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
            if (_courseRepository.Records.Any(c => c.Id == course.Id))
            {
                if (_lectureCourseRepository.Records.Any(lc => lc.CourseId == course.Id))
                {
                    var lecturecourses = _lectureCourseRepository.Records.Where(lc => lc.CourseId == course.Id);

                    _lectureCourseRepository.Records.RemoveRange(lecturecourses);

                    _lectureCourseRepository.SaveChanges();
                }
                //--------------------------------------------------------------
                if (_studentCourseRepository.Records.Any(sc => sc.CourseId == course.Id))
                {
                    var studentcourses = _studentCourseRepository.Records.Where(sc => sc.CourseId == course.Id);

                    _studentCourseRepository.Records.RemoveRange(studentcourses);

                    _studentCourseRepository.SaveChanges();
                }
                //----------------------------------------------------------------
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
