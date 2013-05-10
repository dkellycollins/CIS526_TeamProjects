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
        /// Unqiue token for this task.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Task name.
        /// </summary>
        [Display(Name="Task Name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the task.
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// True if the task is a milestone task.
        /// </summary>
        [Display(Name = "Milestone")]
        public bool IsMilestone { get; set; }

        /// <summary>
        /// Points to be awarded on completion.
        /// </summary>
        [Display(Name = "Points")]
        public int Points { get; set; }

        /// <summary>
        /// The number of bonus points to award.
        /// </summary>
        [Display(Name = "Bonus Points")]
        public int BonusPoints { get; set; }

        /// <summary>
        /// The maximum number of times this milestone will award the BonusPoints to a player.
        /// </summary>
        [Display(Name = "Max Awards")]
        public int MaxBonusAwards { get; set; }

        /// <summary>
        /// Date and tme when this task becomes availible. Null means that it is always avalible until the end date.
        /// </summary>
        [Display(Name = "Begins")]
        public DateTime StartTime { get; set; }

        public string StartTimeDisplay
        {
            get
            {
                return this.StartTime.ToShortDateString();
            }
        }

        /// <summary>
        /// Date and time when this task becomes unvailible. Null means that this task never expires.
        /// </summary>
        [Display(Name = "Ends")]
        public DateTime EndTime { get; set; }

        public string EndTimeDisplay
        {
            get
            {
                return this.EndTime.ToShortDateString();
            }
        }

        public string Solution { get; set; }

        /// <summary>
        /// The link to the icon image.
        /// </summary>
        [Display(Name = "Image")]
        public string IconLink { get; set; }

        /// <summary>
        /// Represents the point path.
        /// </summary>
        [Display(Name = "Point Type")]
        public virtual PointType PointPath { get; set; }

        /// <summary>
        /// Users who have completed this task.
        /// </summary>
        public virtual ICollection<CompletedTask> CompletedBy { get; set; }
    }
}