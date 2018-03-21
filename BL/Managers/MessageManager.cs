using BL.Managers.Interfaces;
using Data.Repositories.Interfaces;
using Model.Model;
using BL.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Managers
{
   public class MessageManager : IMessageManager
    {
        private IMessageRepository _messageRepository;

        public MessageManager(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public List<Message> getMessageByStudentId(int StudentId)
        {
            if (_messageRepository.Records.Any(msg => msg.StudentId == StudentId))
            {
                var message = _messageRepository.Records.Where(msg => msg.StudentId == StudentId).ToList();

                return message.ApplySort();
            }
            else return null;
        }          

        public List<Message> getMessageByLecturerId(int LecturerId)
        {
            if (_messageRepository.Records.Any(msg => msg.LecturerId == LecturerId))
            {
                var messages = _messageRepository.Records.Where(msg => msg.LecturerId == LecturerId).ToList();

                return messages.ApplySort();
            }
            else return null;

        }

        public String setMessage(Message message)
        {
            var msg = new Message
            {
                StudentId = message.StudentId,
                LecturerId = message.LecturerId,
                Details = message.Details,
                CreateOn = DateTime.Now
            };

            if (message.Details != "")
            {
                _messageRepository.Add(msg);
                return "Message Sent!";
            }
            else return "cannot send void message!";
        }

        public String DeleteMessage(Message message)
        {
            if (_messageRepository.Records.Any(m => m.LecturerId == message.LecturerId &&
                                                    m.StudentId == message.StudentId &&
                                                    m.Details == message.Details))
            {
                _messageRepository.Delete(message);

                return "successfully Deleted";
            }

            else return "No such message";

        }
    }
}
