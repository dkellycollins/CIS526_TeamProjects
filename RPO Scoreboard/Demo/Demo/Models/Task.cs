using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    /// <summary>
    /// Stores task information.
    /// </summary>
    public class Task
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// Task name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the task.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// True if the task is a milestone task.
        /// </summary>
        public bool IsMilestone { get; set; }

        /// <summary>
        /// Points to be awarded on completion.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// The number of bonus points to award.
        /// </summary>
        public int BonusPoints { get; set; }

        /// <summary>
        /// The maximum number of times this milestone will award the BonusPoints to a player.
        /// </summary>
        public int MaxBonusAwards { get; set; }

        /// <summary>
        /// Date and tme when this task becomes availible. Null means that it is always avalible until the end date.
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Date and time when this task becomes unvailible. Null means that this task never expires.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// The link to the icon image.
        /// </summary>
        public string IconLink { get; set; }

        /// <summary>
        /// Represents the point path.
        /// </summary>
        public virtual PointType PointPath { get; set; }

        /// <summary>
        /// Users who have completed this task.
        /// </summary>
        public virtual ICollection<UserProfile> CompletedBy { get; set; }
    }
}