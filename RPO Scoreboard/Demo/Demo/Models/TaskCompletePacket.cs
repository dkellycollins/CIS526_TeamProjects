using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class TaskCompletePacket
    {
        public TaskCompletePacket()
        { }

        public TaskCompletePacket(string data)
        {
            string[] args = data.Split(';');
            UserID = args[0].Trim();
            TaskToken = args[1].Trim();
            Solution = args[2].Trim();
            Source = args[3].Trim();
        }

        public string UserID { get; set; }
        public string TaskToken { get; set; }
        public string Solution { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return string.Format("{0}; {1}; {2}; {3};",
                UserID,
                TaskToken,
                Solution,
                Source);
        }
    }
}