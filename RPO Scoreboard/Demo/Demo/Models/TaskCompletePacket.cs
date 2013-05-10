using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class TaskCompletePacket
    {
        public int UserID { get; set; }
        public string TaskToken { get; set; }
        public string Solution { get; set; }
        public string Source { get; set; }
    }
}