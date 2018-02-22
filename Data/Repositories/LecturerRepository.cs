using Data.Database;
using Data.Repositories.Interfaces;
using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace Data.Repositories
{
  
    public class LecturerRepository : GenericRepository<Lecturer>, ILecturerRepository
    {
        public LecturerRepository(LMSEntities context) : base(context)
        {

        }
        public LecturerCourseDto GetLectureByIdWithCourses(int id)
        {
            //其中用_context合并Lecture &Lecturecourse
            var query = from Lecture in _context.Lecturers
                        join lecturecourse in _context.LecturerCourses on Lecture.Id equals lecturecourse.Id into Lecturecoursedto
                        select new
                        {
                            LectureId = Lecture.Id,
                            Name = Lecture.Name,
                            CourseId = Lecturecoursedto.Select(i => i.CourseId).ToList()
                        };
            var results = query.ToList();
            var aim = results.Where(s => s.LectureId == id).FirstOrDefault();
            if (aim == null) return null;
            else
                return new LecturerCourseDto
                {
                    Id = aim.LectureId,
                    Name = aim.Name,
                    CourseId = aim.CourseId
                };
        }

        public string AddLectureToCourse(int lectureid,int courseid)
        {
            if (!_context.LecturerCourses.Any(lc => lc.LecturerId == lectureid && lc.CourseId == courseid))
            {
                _context.LecturerCourses.Add(new LecturerCourse { LecturerId = lectureid, CourseId = courseid });
                _context.SaveChanges();
                return "Ok";
            }
            else return null;
                
        }
    }
}
