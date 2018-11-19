using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DUMACTestTask.Database;
using DUMACTestTask.Interfaces;
using DUMACTestTask.Models;

namespace DUMACTestTask.Services
{
    public class MessagesService : IMessageService
    {
        private MessagesContext _db = new MessagesContext();

        public async Task<string> Add(Message message)
        {
            _db.Messages.Add(message);
            await _db.SaveChangesAsync();
            var addedMessage = await _db.Messages.FirstOrDefaultAsync(m =>
                (m.Body == message.Body && m.Recipients == message.Recipients && m.Subject == message.Subject));
            return addedMessage.Id.ToString();
        }

        public void SendToNotification(Message message)
        {
            try
            {
                //invoke sending to external service

                message.IsSent = true;
                _db.Entry(message).State = EntityState.Modified;
                _db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                message.IsSent = false;
                _db.Entry(message).State = EntityState.Modified;
                _db.SaveChangesAsync();
            }
           
        }

        public async Task<List<Message>> GetAll()
        {
            return await _db.Messages.ToListAsync();
        }
    }
}