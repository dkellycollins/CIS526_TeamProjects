using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;

namespace CIS726_Assignment2.SystemBus
{
    /// <summary>
    /// A basic implementation of the IMessageQueuePublisher
    /// </summary>
    public class BasicMessageQueueProducer<T>
        : IMessageQueueProducer<T>
    {
        private MessageQueue _producerQueue = null; //This is the queue used by the Controller
        private MessageQueue _consumerQueue = null; //This is the queue used by the database

        private Guid _id;

        public BasicMessageQueueProducer()
        {
            string baseQueueName = @".\Private$\" + typeof(T).FullName;
            QueueHelpers.CreateProducerAndConsumerQueues(baseQueueName,
                out _producerQueue,
                out _consumerQueue);
            _consumerQueue.Formatter = new ResponseFormatter<List<T>>();
            _id = Guid.NewGuid();
        }

        #region IMessageQueueProducer members

        public T Get(T data)
        {
            sendMessage("GET", data);
            return recieveMessage().FirstOrDefault();
        }

        public List<T> GetAll()
        {
            sendMessage("GET_ALL", default(T));
            return recieveMessage();
        }

        public void Update(T data)
        {
            sendMessage("UPDATE", data);
        }

        public void Create(T data)
        {
            sendMessage("CREATE", data);
        }

        public void Remove(T data)
        {
            sendMessage("REMOVE", data);
        }

        public void Dispose()
        {
            _producerQueue.Close();
            _producerQueue.Dispose();

            _consumerQueue.Close();
            _consumerQueue.Dispose();
        }

        #endregion IMessageQueueProducer members

        /// <summary>
        /// Puts a message on the queue with the given action as the label and the data as the body.
        /// </summary>
        /// <param name="action">Action for the database to perform.</param>
        /// <param name="data">Data for the database to use.</param>
        /// <returns>The id of the message. This can be used to get the response from the queue.</returns>
        private string sendMessage(string action, T data)
        {
            Message message = new Message()
            {
                Formatter = new RequestFormatter<T>(),
                Label = _id.ToString(),
                Body = new Request<T>()
                {
                    ID = _id,
                    Action = action,
                    Data = data
                }
            };
            _producerQueue.Send(message);

            return "";
        }

        private List<T> recieveMessage()
        {
            Response<List<T>> response = null;
            while (response == null)
            {
                foreach (Message m in _consumerQueue.GetAllMessages())
                {
                    if (m.Label == _id.ToString())
                        response = (Response<List<T>>)_consumerQueue.ReceiveById(m.Id).Body;
                }
            }

            return response.Result;
        }
    }
}