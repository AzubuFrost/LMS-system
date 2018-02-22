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

        public class LecturerManager : ILecturerManager
        {
            private ILecturerRepository _lectureRepository;
            private ILecturerCourseRepository _lectureCourseRepository;

        public LecturerManager(ILecturerRepository LectureRepository,ILecturerCourseRepository lectureCourseRepository)
            {
                _lectureRepository = LectureRepository;
                _lectureCourseRepository = lectureCourseRepository;

            }

        public LecturerCourse EnrollCourse(LecturerCourse lectureCourse)
        {
            
            if (!_lectureCourseRepository.Records.Any(lc => lc.CourseId == lectureCourse.CourseId && lc.LecturerId == lectureCourse.LecturerId))

            {
                _lectureCourseRepository.Add(lectureCourse);

                return lectureCourse;
            }

            else return null;
        }

        public Lecturer CreateLecture(Lecturer Lecture)
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

            public List<Lecturer> GetAll()
            {
                var lectures = _lectureRepository.GetAll().ToList();

                return lectures;

            }

            public Lecturer GetLectureById(int id)
            {
                var Lecture = _lectureRepository.GetById(id);
                if (Lecture != null)
                    return _lectureRepository.GetById(id);
                else return null;
            }

        public string Delete(Lecturer lecture)
        {
            if (_lectureRepository.Records.Any(s => s.Id == lecture.Id))
            {
                



                if (_lectureCourseRepository.Records.Any(sc => sc.LecturerId == lecture.Id))
                {
                    var lecturecourses = _lectureCourseRepository.Records.Where(sc => sc.LecturerId == lecture.Id);

                    _lectureCourseRepository.Records.RemoveRange(lecturecourses);

                    _lectureCourseRepository.SaveChanges();
                }

                _lectureRepository.Delete(lecture);

                return "successfully deleted";
            }
            else return "No such lecture";

        }

        public LecturerCourseDto GetLectureByIdWithCourses(int id)
            {
                return _lectureRepository.GetLectureByIdWithCourses(id);
            }

        public Lecturer ModifyDetails(Lecturer lecturer)
        {
            if (_lectureRepository.Records.Any(lc => lc.Id == lecturer.Id))
            {
                _lectureRepository.Update(lecturer);

                return lecturer;
            }
            else return null;
        }
    }

        

}

