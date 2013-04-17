using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class PointScore
    {
        public int ID { get; set; }

        public virtual PlayerProfile PlayerProfile { get; set; }

        public virtual PointPath PointPath { get; set; }
    }
}