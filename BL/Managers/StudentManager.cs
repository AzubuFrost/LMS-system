using AutoMapper;
using BL.Managers.Interfaces;
using BL.Util;
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
    public class StudentManager : IStudentManager
    {
        private IStudentRepository _studentRepository;
        private IStudentCourseRepository _studentCourseRepository;

        public StudentManager(IStudentRepository studentRepository,IStudentCourseRepository studentCourseRepository)
        {
            _studentRepository = studentRepository;
            _studentCourseRepository = studentCourseRepository;
            
        }

        public StudentCourse EnrollCourse(StudentCourse studentCourse)
        {
            if (!_studentCourseRepository.Records.Any(sc => sc.CourseId == studentCourse.CourseId && sc.StudentId == studentCourse.StudentId)
                && _studentRepository.Records.Where(sc =>sc.Id == studentCourse.StudentId).FirstOrDefault().Credit > 0)

            {
                _studentCourseRepository.Add(studentCourse);

                var student = _studentRepository.Records.Where(sc => sc.Id == studentCourse.StudentId).FirstOrDefault();

                student.Credit = student.Credit - 4;
                //need to change futher
                
                _studentRepository.Update(student);

                return studentCourse;
            }

            else return null;
        }

        public Student CreateStudent(Student student)
        {

            if (!_studentRepository.Records.Any(x => x.Email == student.Email))
            {
                return _studentRepository.Add(student);
            }
            else
            {
                return null;
            }
        }

        public List<StudentDto> GetAll()
        {
            var students = _studentRepository.GetAll().ToList();

            var studentDtos = Mapper.Map<List<Student>, List<StudentDto>>(students);

            return studentDtos.ToList();

        }

        public Student GetStudentById(int id)
        {
            var student = _studentRepository.GetById(id);
         
            if (student != null)
                return student;
            else return null;
        }

        public string Delete(Student student)
        {
            if (_studentRepository.Records.Any(s => s.Id == student.Id))
            {
                if (_studentCourseRepository.Records.Any(sc => sc.StudentId == student.Id))
                {
                    var studentcourses = _studentCourseRepository.Records.Where(sc => sc.StudentId == student.Id);

                    _studentCourseRepository.Records.RemoveRange(studentcourses);

                    _studentCourseRepository.SaveChanges();
                }

                _studentRepository.Delete(student);

                return "successfully deleted";
            }
            else return "No such student";
           
        }

        public StudentCourseDto GetStudentByIdWithCourses(int id)
        {
            return _studentRepository.GetStudentByIdWithCourses(id);
        }

        public StudentSearchDto SearchStudent(SearchAttribute search)
        {
            if (search.PageNumber == 0)
            { 
                search.PageNumber = 1;
            }
            if (search.PageSize == 0)
            {
                search.PageSize = 20; 
            }
            var students = _studentRepository.Records.Search(search.SearchValue);

            students = students.ApplySort(search.SortOrder, search.SortString);

            var SearchResult = new StudentSearchDto
            {
                PageSize = search.PageSize,
                TotalPage = students.Count() / search.PageSize + (students.Count() % search.PageSize == 0 ? 0 : 1)
            };

            SearchResult.PageNumber = search.PageNumber > SearchResult.TotalPage ? 1 : search.PageNumber;

            SearchResult.Students = Mapper.Map<List<Student>, List<StudentDto>>(students.Skip((SearchResult.PageNumber - 1) * SearchResult.PageSize ).Take(SearchResult.PageSize).ToList());

            return SearchResult;
        }

        public Student ModifyDetails(Student student)
        {
            if (_studentRepository.Records.Any(st => st.Id == student.Id))
            {
                _studentRepository.Update(student);
                return student;
            }
            else return null;
                

        }
    }
}
