using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Runtime.Serialization;
using System.Web;

namespace CIS726_Assignment2.SystemBus
{
    /// <summary>
    /// Wrapper for a request to the database.
    /// </summary>
    /// <typeparam name="T">Type of the Data</typeparam>
    public class Request<T>
    {
        /// <summary>
        /// ID of the message.
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Action for the data base to perform.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Data the database will need to perform the action.
        /// </summary>
        public T Data { get; set; }
    }

    /// <summary>
    /// Handles formatting Request of type T
    /// </summary>
    /// <typeparam name="T">Type of the request.</typeparam>
    public class RequestFormatter<T> : IMessageFormatter
    {
        /// <summary>
        /// The actual type of the request.
        /// </summary>
        private Type requestType = typeof(Request<T>);

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
            DataContractSerializer serializer = new DataContractSerializer(requestType, new DataContractSerializerSettings());
            return serializer.ReadObject(message.BodyStream);
        }

        /// <summary>
        /// Based off the example here: http://www.novokshanov.com/2012/02/datacontractserializer-with-msmq/
        /// </summary>
        /// <param name="message"></param>
        /// <param name="obj"></param>
        public void Write(Message message, object obj)
        {
            MemoryStream stream = new MemoryStream();
            DataContractSerializer serializer = new DataContractSerializer(requestType);
            serializer.WriteObject(stream, obj);

            stream.Position = 0;

            message.BodyStream = stream;
        }

        public object Clone()
        {
            return new RequestFormatter<T>();
        }
    }
}