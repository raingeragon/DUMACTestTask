using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DUMACTestTask.Models;

namespace DUMACTestTask.Database
{
    public class MessagesContext : DbContext
    {
        public MessagesContext() : base("DefaultConnection")
        {}

        public DbSet<Message> Messages { get; set; }
    }
}