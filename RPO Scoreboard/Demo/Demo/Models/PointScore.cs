using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    /// <summary>
    /// Join table between UserProfile and PointPath.
    /// </summary>
    public class PointScore
    {
        [Key]
        public int ID { get; set; }

        public int Score { get; set; }

        //public int UserProfileID { get; set; }

        //public int PointPathId { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual PointType PointPath { get; set; }
    }
}