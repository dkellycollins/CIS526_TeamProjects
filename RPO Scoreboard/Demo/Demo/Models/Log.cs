using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class Log
    {
        public int ID { get; set; }

        //Who
        public string UserName { get; set; }

        //What
        public string Action { get; set; }

        //When
        public DateTime Time { get; set; }

        //Where
        public string Controller { get; set; }
    }
}