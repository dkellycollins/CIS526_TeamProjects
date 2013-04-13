using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.Web;
using CIS726_Assignment2.Models;

namespace CIS726_Assignment2.SystemBus
{
    public class Request
    {
        public Guid ID { get; set; }
        public string Action { get; set; }
        public object Data { get; set; }
    }

    [DataContract()]
    [KnownType(typeof(Course))]
    public class Request<T>
    {
        public Guid ID { get; set; }
        public string Action { get; set; }
        public T Data { get; set; }
    }

    public class RequestFormatter : IMessageFormatter
    {
        private static Type requestType = typeof(Request);

        public bool CanRead(Message message)
        {
            return true;
        }

        public object Read(Message message)
        {
            DataContractSerializer serializer = new DataContractSerializer(requestType, new DataContractSerializerSettings() );
            return serializer.ReadObject(message.BodyStream);
        }

        public void Write(Message message, object obj)
        {
            //if (!(obj is Request))
            //    throw new ArgumentException("Obj must be a request type.");

            MemoryStream stream = new MemoryStream();
            DataContractSerializer serializer = new DataContractSerializer(requestType);
            serializer.WriteObject(stream, obj);

            stream.Position = 0;

            message.BodyStream = stream;
        }

        public object Clone()
        {
            return new RequestFormatter();
        }
    }
}