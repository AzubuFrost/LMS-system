using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
   public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Fee { get; set; }
        public int MaxStudent { get; set; }
        public string Description { get; set; }
    }
}
