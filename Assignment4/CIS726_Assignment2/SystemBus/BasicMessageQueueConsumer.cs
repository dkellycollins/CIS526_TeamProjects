using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;

namespace CIS726_Assignment2.SystemBus
{
    /// <summary>
    /// A basic implementation of the MessageQueueConsumer.
    /// </summary>
    /// <typeparam name="T">IModel type</typeparam>
    public class BasicMessageQueueConsumer<T>
        : IMessageQueueConsumer<T>
    {
        MessageQueue _producerQueue; //This is the queue used by the Controller
        MessageQueue _consumerQueue; //This is the queue used by the database

        private bool _recieving;

        public BasicMessageQueueConsumer()
        {
            string baseQueueName = @".\Private$\" + typeof(T).FullName;
            QueueHelpers.CreateProducerAndConsumerQueues(baseQueueName,
                out _producerQueue,
                out _consumerQueue);
            _producerQueue.Formatter = new RequestFormatter<T>();
            _producerQueue.ReceiveCompleted += _producerQueue_ReceiveCompleted;
        }

        #region IMessageQueueConsumer members

        public event GetMessageHandler<T> Get;
        public event GetAllMessageHandler<T> GetAll;
        public event CreateMessageHandler<T> Create;
        public event UpdateMessageHandler<T> Update;
        public event RemoveMessageHandler<T> Remove;

        public void BeginProcessing()
        {
            _recieving = true;
            _producerQueue.BeginReceive();
        }

        public void StopProcessing()
        {
            _recieving = false;
        }

        public void Dispose()
        {
            StopProcessing();
            _producerQueue.Close();
            _producerQueue.Dispose();

            _consumerQueue.Close();
            _consumerQueue.Dispose();
        }

        #endregion

        private void _producerQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            if (!_recieving)
                return;

            Message recievedMessage = _producerQueue.EndReceive(e.AsyncResult);
            //Let what ever owns this class process the data.
            Request<T> request = (Request<T>)recievedMessage.Body;
            switch (request.Action)
            {
                case "GET":
                    HandleGetAction(request.ID, request.Data);
                    break;
                case "GET_ALL":
                    HandleGetAllAction(request.ID);
                    break;
                case "CREATE":
                    HandleCreateAction(request.ID, request.Data);
                    break;
                case "UPDATE":
                    HandleUpdateAction(request.ID, request.Data);
                    break;
                case "REMOVE":
                    HandleRemoveAction(request.ID, request.Data);
                    break;
            }

            //Look for the next message.
            _producerQueue.BeginReceive();
        }

        private void HandleGetAction(Guid id, T data)
        {
            Response<List<T>> response = new Response<List<T>>()
            {
                ID = id
            };

            try
            {
                T result = Get(data);
                response.Result = new List<T> { result };
                response.ErrorMessage = null;
            }
            catch (Exception e)
            {
                response.Result = new List<T>();
                response.ErrorMessage = e.Message;
            }

            Message message = new Message()
            {
                Formatter = new ResponseFormatter<List<T>>(),
                Label = id.ToString(),
                Body = response
            };

            _consumerQueue.Send(message);
        }

        private void HandleGetAllAction(Guid id)
        {
            Response<List<T>> response = new Response<List<T>>()
            {
                ID = id
            };

            try
            {
                response.Result = GetAll();
                response.ErrorMessage = null;
            }
            catch (Exception e)
            {
                response.Result = new List<T>();
                response.ErrorMessage = e.Message;
            }

            Message message = new Message()
            {
                Formatter = new ResponseFormatter<List<T>>(),
                Label = id.ToString(),
                Body = response
            };

            _consumerQueue.Send(message);
        }

        private void HandleCreateAction(Guid id, T data)
        {
            Response<List<T>> response = new Response<List<T>>()
            {
                ID = id
            };

            try
            {
                T createdData = Create(data);
                response.Result = new List<T>() { createdData };
                response.ErrorMessage = null;
            }
            catch (Exception e)
            {
                response.Result = new List<T>();
                response.ErrorMessage = e.Message;
            }

            Message message = new Message()
            {
                Formatter = new ResponseFormatter<List<T>>(),
                Label = id.ToString(),
                Body = response
            };

            _consumerQueue.Send(message);
        }

        private void HandleUpdateAction(Guid id, T data)
        {
            Update(data);
        }

        private void HandleRemoveAction(Guid id, T data)
        {
            Remove(data);
        }

    }
}