using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Messaging;

namespace CIS726_Assignment2
{
    public class SystemBus
    {
        private MessageQueue _queue;

        public SystemBus(string queueName)
        {
            _queue = new MessageQueue(queueName);
        }

        public void SendMessage(string label, string message)
        {
            Message msg = new Message();
            msg.Body = message;
            msg.Label = label;
            _queue.Send(msg);
        }

        public string CheckMessage()
        {
            var message = _queue.Receive(new TimeSpan(0, 0, 1));
            if (message != null)
            {
                message.Formatter = new XmlMessageFormatter(new String[] { "System.String,mscorlib" });
                return message.Body.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}