using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    /// <summary>
    /// Join table between UserProfile and PointPath.
    /// </summary>
    public class PointScore
    {
        public int ID { get; set; }

        public virtual UserProfile PlayerProfile { get; set; }

        public virtual PointPath PointPath { get; set; }
    }
}