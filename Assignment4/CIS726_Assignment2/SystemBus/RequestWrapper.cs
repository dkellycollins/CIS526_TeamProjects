using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;

namespace CIS726_Assignment2.SystemBus
{
    public class Request
    {
        public Guid ID { get; set; }
        public string Action { get; set; }
        public object Data { get; set; }
    }

    public class RequestFormatter : IMessageFormatter
    {
        public bool CanRead(Message message)
        {
            return message.Body is Request;
        }

        public object Read(Message message)
        {
            return message.Body;
        }

        public void Write(Message message, object obj)
        {
            message.Body = obj;
        }

        public object Clone()
        {
            return this;
        }
    }
}