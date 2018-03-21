using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Util
{
    public static class Extension
    {
        public static IEnumerable<Student> Search(this IEnumerable<Student> students, string searchValue)
        {
            var newstudents = students.Where(x => x.FirstName.ToLower().Contains(searchValue.ToLower()) || x.LastName.ToLower().Contains(searchValue.ToLower()) || x.Email.ToLower().Contains(searchValue.ToLower()));
            var a1 = newstudents.ToList();
            return newstudents;
        }
        public static IEnumerable<Student> ApplySort(this IEnumerable<Student> students, string sortOrder, string sortString)
        {
            var isAscending = false;
            if (!string.IsNullOrEmpty(sortOrder))
            {
                isAscending = !(sortOrder.ToLower() == "desc");
            }
            if (string.IsNullOrEmpty(sortString))
            {
                sortString = "id";
            }
            switch (sortString.ToLower())
            {
                case "id":
                    return isAscending ? students.OrderBy(x => x.Id) : students.OrderByDescending(x => x.Id);
                case "name":
                    return isAscending ? students.OrderBy(x => x.FirstName) : students.OrderByDescending(x => x.FirstName);
                case "email":
                    return isAscending ? students.OrderBy(x => x.Email) : students.OrderByDescending(x => x.Email);
                case "dateofbirth":
                    return isAscending ? students.OrderBy(x => x.DateOfBirth) : students.OrderByDescending(x => x.DateOfBirth);
                case "credit":
                    return isAscending ? students.OrderBy(x => x.Credit) : students.OrderByDescending(x => x.Credit);
                case "gender":
                    return isAscending ? students.OrderBy(x => x.Gender) : students.OrderByDescending(x => x.Gender);
                default:
                    return students.OrderBy(x => x.Id);
            }
        }

        public static List<Message> ApplySort(this List<Message> messages)
        {
            return messages.OrderBy(msg => msg.CreateOn).ToList();
        }
    }
}
