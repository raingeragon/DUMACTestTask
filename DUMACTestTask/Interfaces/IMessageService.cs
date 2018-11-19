using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DUMACTestTask.Models;

namespace DUMACTestTask.Interfaces
{
    public interface IMessageService
    {
        Task<string> Add (Message message);
        void SendToNotification(Message message);
        Task<List<Message>> GetAll();
    }
}