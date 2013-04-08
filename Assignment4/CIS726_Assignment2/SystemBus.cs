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

        const string queueName = @".\private$\Assignment4Queue";

        public SystemBus(string queueName)
        {
            _queue = new MessageQueue(queueName);
        }

        public void SendMessage<T>(T message)
        {            
             MessageQueue msMq = null;

            if (!MessageQueue.Exists(queueName))
                {
                   msMq = MessageQueue.Create(queueName);
                }
            else
                {
                    msMq = new MessageQueue(queueName);
                }
        try
        {        
            msMq.Send(message);
        }
        catch (MessageQueueException ee)
        {
            Console.Write(ee.ToString());
        }
        catch (Exception eee)
        {
            Console.Write(eee.ToString());
        }
        finally
        {
            msMq.Close();
        }
            Console.WriteLine("Message sent ......");
             
            /*
            Message msg = new Message();
            msg.Body = message;
            msg.Label = label;
            _queue.Send(msg);
             * */
        }

        public void CheckMessage<T>()
        {
            MessageQueue msMq = msMq = new MessageQueue(queueName);
            
            try
            {

                msMq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });

                var message = (T)msMq.Receive().Body;

                //Console.WriteLine("FirstName: " + message.FirstName + ", LastName: " + message.LastName);

                // Console.WriteLine(message.Body.ToString());

            }

            catch (MessageQueueException ee)
            {

                Console.Write(ee.ToString());

            }

            catch (Exception eee)
            {

                Console.Write(eee.ToString());

            }

            finally
            {

                msMq.Close();

            }

            Console.WriteLine("Message received ......");

            /*
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
             * */
        }
    }
}
