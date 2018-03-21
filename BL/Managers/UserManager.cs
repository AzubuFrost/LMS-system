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
    public class UserManager : IUserManager
    {
        private IUserRepository _userRepository;
        private IStudentRepository _studentRepository;
        private ILecturerRepository _lecturerRepository;
        private IStudentCourseRepository _studentCourseRepository;
        private ILecturerCourseRepository _lecturerCourseRepository;
        private ICourseRepository _courseRepository;

        public UserManager(IUserRepository userRepository, IStudentRepository studentRepository, ILecturerRepository lecturerRepository, 
                           IStudentCourseRepository studentCourseRepository, ILecturerCourseRepository lecturerCourseRepository,
                           ICourseRepository courseRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _lecturerRepository = lecturerRepository;
            _lecturerCourseRepository = lecturerCourseRepository;
            _studentCourseRepository = studentCourseRepository;
            _courseRepository = courseRepository;
        }

        private Student _linkUserWithStudent(UserDisplayDto user)
        {
            if (_studentRepository.Records.Any(st => st.Id == user.PersonalId))
            {
                    return _studentRepository.GetById(user.PersonalId);
            }
                else return null;
        }

        private Lecturer _linkUserWithLecturer(UserDisplayDto user)
        {
            if (_lecturerRepository.Records.Any(lc => lc.Id == user.PersonalId))
            {
                return _lecturerRepository.GetById(user.PersonalId);
            }
            else return null;
        }

        private Boolean _isLecturer(String AccountType)
        {
            if (AccountType == "lecturer")
                return true;
            else return false;
        }

        private Boolean _isStudent(String AccountType)
        {
            if (AccountType == "student")
                return true;
            else return false;
        }
           
        

        public List<Course> getCourseFromUser(UserDisplayDto user)
        {
            var courses = new List<Course>();
            if (user.AccountType == "lecturer")
            {
                var personnel = _linkUserWithStudent(user);
                var courseIds = _lecturerCourseRepository.Records.Where(sc => sc.LecturerId == personnel.Id).Select(sc => sc.CourseId).ToList();

                foreach (int id in courseIds)
                {
                    courses.Add(_courseRepository.GetById(id));
                }
            }
            if (user.AccountType == "student")
            {
                var personnel = _linkUserWithStudent(user);
                var courseIds = _studentCourseRepository.Records.Where(sc => sc.StudentId == personnel.Id).Select(sc => sc.CourseId).ToList();
                
                foreach (int id in courseIds) {
                    courses.Add(_courseRepository.GetById(id));
                }
            }
            return courses;
            
        }



        public UserDisplayDto CreateUser(UserRegisterDto user)
        {
            var isAvailableToCreate = false;
            User createdUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = Util.HashHelper.GetMD5HashData(user.Password),
                UserName = user.UserName,
                CreatedOn = DateTime.Now,
                AccountType = user.AccountType,
                PersonalId = user.PersonalId
            };
            if (_isStudent(user.AccountType))
            {
                isAvailableToCreate = _studentRepository.Records.Any(st => st.Id == user.PersonalId);
            }
            if (_isLecturer(user.AccountType))
            {
                isAvailableToCreate = _lecturerRepository.Records.Any(st => st.Id == user.PersonalId);
            }
            if (isAvailableToCreate)
            {
                createdUser = _userRepository.Add(createdUser);

                UserDisplayDto displayUser = new UserDisplayDto
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PersonalId = user.PersonalId,
                    AccountType = user.AccountType
                };

                return displayUser;
            }
            else return null;
        }

        public User FindUser(string userName, string password)
        {
            var passwordHash = Util.HashHelper.GetMD5HashData(password);
            return _userRepository.FindUser(userName, passwordHash);
        }
    }


}
