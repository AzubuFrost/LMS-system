using AutoMapper;
using BL.Managers.Interfaces;
using Model.Dto;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers
{
    [RoutePrefix("api")]
    public class MessageController : ApiController
    {
        private readonly IMessageManager _messageManager;

        public MessageController(IMessageManager messageManager)
        {
            _messageManager = messageManager;
        }

        // GET: api/Message
        [Route("messages/lecturer/{id}")]
        public IHttpActionResult GetByLecturerId(int id)
        {
            var message = _messageManager.getMessageByLecturerId(id);
            if (message != null)
            {
                return Ok(Mapper.Map<List<Message>,List<MessageDto>>(message));
            }
            else return Ok("No message has been found");
        }

        // GET: api/Message/5
        [Route("messages/student/{id}")]
        public IHttpActionResult GetByStudentId(int id)
        {
            var message = _messageManager.getMessageByStudentId(id);
            if (message != null)
            {
                return Ok(Mapper.Map<List<Message>, List<MessageDto>>(message));
            }
            else return Ok("No message has been found");
        }

        [HttpPost]
        [Route("messages/create")]
        public IHttpActionResult CollectMessage([FromBody]Message message)
        {
            if (message.Details != "")
            {
                return Ok(_messageManager.setMessage(message));
            }

            else return BadRequest("cannot send void message");
        }

        [HttpDelete]
        [Route("messages/delete")]
        public IHttpActionResult DiscardMessage(Message message)
        {
            //to be continued
            return Ok(_messageManager.DeleteMessage(message));
            
        }
    }
}
