using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dto
{
   public  class MessageDto
    {
        public int StudentId { get; set; }
        public int LecturerId { get; set; }
        public int Id { get; set; }
        public string Details { get; set; }
        public System.DateTime CreateOn { get; set; }
    }
}
