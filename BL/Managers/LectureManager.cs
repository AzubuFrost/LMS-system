using BL.Managers.Interfaces;
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
            private ILectureRepository _LectureRepository;


            public LectureManager(ILectureRepository LectureRepository)
            {
                _LectureRepository = LectureRepository;

            }

            public Lecture CreateLecture(Lecture Lecture)
            {

                if (!_LectureRepository.Records.Any(x => x.Name == Lecture.Name))
                {
                    return _LectureRepository.Add(Lecture);
                }
                else
                {
                    return null;
                }
            }

            public List<Lecture> GetAll()
            {
                var lectures = _LectureRepository.GetAll().ToList();

                return lectures;

            }

            public Lecture GetLectureById(int id)
            {
                var Lecture = _LectureRepository.GetById(id);
                if (Lecture != null)
                    return _LectureRepository.GetById(id);
                else return null;
            }

            public string Delete(Lecture Lecture)
            {
                if (_LectureRepository.Records.Any(s => s.Id == Lecture.Id))
                {
                    _LectureRepository.Delete(Lecture);
                    return "successfully deleted";
                }
                else return "deleted failed";
            }

            public LectureCourseDto GetLectureByIdWithCourses(int id)
            {
                return _LectureRepository.GetLectureByIdWithCourses(id);
            }
        }

}

