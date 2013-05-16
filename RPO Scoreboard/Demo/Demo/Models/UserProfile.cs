using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    /// <summary>
    /// Contains data on a specific player.
    /// </summary>
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        public bool IsAdmin { get; set; }

        /// <summary>
        /// The player's total score.
        /// </summary>
        [Display(Name = "Total Score")]
        public int TotalScore
        {
            get
            {
                if (CompletedTask == null)
                    return 0;
                int score = 0;
                foreach (CompletedTask completed in CompletedTask)
                    score += completed.AwardedPoints;
                return score;
            }
        }

        public int ScoreFor(string pointType)
        {
            int score = 0;
            foreach (CompletedTask completed in CompletedTask.Where((x) => x.Task.PointPath.Name == pointType))
                score += completed.AwardedPoints;
            return score;
        }

        /// <summary>
        /// The task that have been completed by the player.
        /// </summary>
        public virtual ICollection<CompletedTask> CompletedTask { get; set; }
    }
}