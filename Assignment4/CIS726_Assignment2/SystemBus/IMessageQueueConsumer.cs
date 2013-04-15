using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CIS726_Assignment2.SystemBus
{
    public delegate T GetMessageHandler<T>(T data);
    public delegate List<T> GetAllMessageHandler<T>();
    public delegate T CreateMessageHandler<T>(T data);
    public delegate void UpdateMessageHandler<T>(T data);
    public delegate void RemoveMessageHandler<T>(T data);

    /// <summary>
    /// An interface for the consumer to continually recieve data from the queue.
    /// </summary>
    public interface IMessageQueueConsumer<T>
        : IDisposable
    {
        /// <summary>
        /// Raised when a Get request comes in.
        /// </summary>
        event GetMessageHandler<T> Get;

        /// <summary>
        /// Raise when a GetAll request comes in.
        /// </summary>
        event GetAllMessageHandler<T> GetAll;

        /// <summary>
        /// Raised when a create message comes in. Data will be model to create.
        /// </summary>
        event CreateMessageHandler<T> Create;

        /// <summary>
        /// Raised when an update messsage comes in. Data will be model to update.
        /// </summary>
        event UpdateMessageHandler<T> Update;

        /// <summary>
        /// Raised when a remove message comes in. Data will be model to remove.
        /// </summary>
        event RemoveMessageHandler<T> Remove;

        /// <summary>
        /// Starts recieving data from the queue.
        /// </summary>
        void BeginProcessing();

        /// <summary>
        /// Stops recieving data from the queue.
        /// </summary>
        void StopProcessing();
    }
}