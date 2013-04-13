using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.Web;

namespace CIS726_Assignment2.SystemBus
{
    public class Response
    {
        public Guid ID { get; set; }
        public bool Success { get; set; }
        public object Result { get; set; }
    }

    public class ResponseFormatter : IMessageFormatter
    {
        private static Type responseType = new Response().GetType();

        public bool CanRead(Message message)
        {
            return true;
        }

        /// <summary>
        /// Based off the example here: http://www.novokshanov.com/2012/02/datacontractserializer-with-msmq/
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public object Read(Message message)
        {
            DataContractSerializer serializer = new DataContractSerializer(responseType);
            return serializer.ReadObject(message.BodyStream);
        }

        /// <summary>
        /// Based off th eexample here: http://www.novokshanov.com/2012/02/datacontractserializer-with-msmq/
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        public void Write(Message message, object obj)
        {
            if (!(obj is Response))
                throw new ArgumentException("Obj must be a response type.");

            using (MemoryStream stream = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(responseType);
                serializer.WriteObject(stream, obj);
                
                stream.Position = 0;

                message.BodyStream = stream;
            }
        }

        public object Clone()
        {
            return this;
        }
    }
}