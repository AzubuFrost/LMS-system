using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
    public class LecturerCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> CourseId { get; set; }
    }
}
