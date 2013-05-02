using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    /// <summary>
    /// Join table between userprofile and Task.
    /// </summary>
    public class CompletedTask
    {
        [Key]
        public int ID { get; set; }

        public DateTime CompletedDate { get; set; }

        public int AwardedPoints { get; set; }

        public int UserProfileID { get; set; }

        public int TaskID { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual Task Task { get; set; }
    }
}