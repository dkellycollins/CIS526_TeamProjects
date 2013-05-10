using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Demo.Models;

namespace TestApplication
{
    class Program
    {
        const string TASK_TOKEN = "A";
        const string SITE = "localhost:1054/Task/CompleteTask/"
        
        static void Main(string[] args)
        {
            string buffer;
            while(true)
            {
                buffer = Console.In.ReadLine();
                if(buffer == "q")
                    Application.Exit();
                else
                    sendPacket(buffer);
            }
        }

        static void sendPacket(string input)
        {
            TaskCompletePacket packet = new TaskCompletePacket()
            {
                UserID = Int32.Parse(input),
                TaskToken = TASK_TOKEN,
                Source = "TestApp"
            };
        }
    }
}
