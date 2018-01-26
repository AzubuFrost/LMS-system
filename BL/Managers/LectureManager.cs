using BL.Managers.Interfaces;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{

        public class LectureManager : ILectureManager
        {
            private ILectureRepository _lectureRepository;
            private ILectureCourseRepository _lectureCourseRepository;

        public LectureManager(ILectureRepository LectureRepository,ILectureCourseRepository lectureCourseRepository)
            {
                _lectureRepository = LectureRepository;
                _lectureCourseRepository = lectureCourseRepository;

            }

        public LectureCourse EnrollCourse(LectureCourse lectureCourse)
        {
            
            if (!_lectureCourseRepository.Records.Any(lc => lc.CourseId == lectureCourse.CourseId && lc.LectureId == lectureCourse.LectureId))

            {
                _lectureCourseRepository.Add(lectureCourse);

                return lectureCourse;
            }

            else return null;
        }

        public Lecture CreateLecture(Lecture Lecture)
            {

            if (!_lectureRepository.Records.Any(x => x.Name == Lecture.Name))
                {
                    return _lectureRepository.Add(Lecture);
                }
                else
                {
                    return null;
                }
            }

            public List<Lecture> GetAll()
            {
                var lectures = _lectureRepository.GetAll().ToList();

                return lectures;

            }

            public Lecture GetLectureById(int id)
            {
                var Lecture = _lectureRepository.GetById(id);
                if (Lecture != null)
                    return _lectureRepository.GetById(id);
                else return null;
            }

        public string Delete(Lecture lecture)
        {
            if (_lectureRepository.Records.Any(s => s.Id == lecture.Id))
            {
                



                if (_lectureCourseRepository.Records.Any(sc => sc.LectureId == lecture.Id))
                {
                    var lecturecourses = _lectureCourseRepository.Records.Where(sc => sc.LectureId == lecture.Id);

                    _lectureCourseRepository.Records.RemoveRange(lecturecourses);

                    _lectureCourseRepository.SaveChanges();
                }

                _lectureRepository.Delete(lecture);

                return "successfully deleted";
            }
            else return "No such lecture";

        }

        public LectureCourseDto GetLectureByIdWithCourses(int id)
            {
                return _lectureRepository.GetLectureByIdWithCourses(id);
            }
        }

}

