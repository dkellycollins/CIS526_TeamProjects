using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Web;

namespace CIS726_Assignment2.SystemBus
{
    public class QueueHelpers
    {
        /// <summary>
        /// Creates a pair of queues, one for a producer and one for a consumer.
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="producerQueue"></param>
        /// <param name="consumerQueue"></param>
        public static void CreateProducerAndConsumerQueues(string queueName,
            out MessageQueue producerQueue,
            out MessageQueue consumerQueue)
        {
            string producerQueueName = queueName + "-p";
            if (MessageQueue.Exists(producerQueueName))
                producerQueue = new MessageQueue(producerQueueName);
            else
                producerQueue = MessageQueue.Create(producerQueueName);

            string consumerQueueName = queueName + "-c";
            if (MessageQueue.Exists(consumerQueueName))
                consumerQueue = new MessageQueue(consumerQueueName);
            else
                consumerQueue = MessageQueue.Create(consumerQueueName);
        }
    }
}