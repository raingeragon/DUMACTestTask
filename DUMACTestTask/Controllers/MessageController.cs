using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using DUMACTestTask.Interfaces;
using DUMACTestTask.Models;

namespace DUMACTestTask.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MessageController : ApiController
    {
        private IMessageService _service;

        public MessageController(IMessageService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Gets all messages in database
        /// </summary>
        /// <returns>List of Messages</returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            try
            {
                var list = await _service.GetAll();
                var result = new List<MessageModel>();

                foreach (var mes in list)
                {
                    result.Add(new MessageModel()
                    {
                        Id = mes.Id,
                        Subject = mes.Subject,
                        Body = mes.Body,
                        IsSent = mes.IsSent,
                        Recipients = mes.Recipients.Split(';')
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, result );
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, String.Format("Sorry, exception occured: {0}", e.Message));
            }

        }

        /// <summary>
        /// Adds new message to database and sends notification to external service
        /// </summary>
        /// <param name="message">Message model</param>
        /// <returns>Id of added message</returns>
        [HttpPost]
        public async Task<HttpResponseMessage> AddMessage(NewMessageModel message)
        {
            try
            {
                var recipientsStr = "";
                foreach (var rec in message.Recipients)
                {
                    recipientsStr += rec + ";";
                }

                recipientsStr = recipientsStr.Substring(0, recipientsStr.Length - 2);

                var messageToAdd = new Message()
                {
                    Subject = message.Subject,
                    IsSent = false,
                    Recipients = recipientsStr,
                    Body = message.Body
                };

                var id = await _service.Add(messageToAdd);
                _service.SendToNotification(messageToAdd);
                return Request.CreateResponse(HttpStatusCode.OK, id);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, String.Format("Sorry, exception occured: {0}", e.Message));
            }

        }
    }
}
