using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers.Interfaces
{
   public interface IMessageManager
    {
       List<Message> getMessageByStudentId(int StudentId);
       List<Message> getMessageByLecturerId(int LecturerId);
        String setMessage(Message message);
        String DeleteMessage(Message message);
    }
}
