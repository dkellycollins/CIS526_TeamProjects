using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int ID { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        /// <summary>
        /// The player's total score.
        /// </summary>
        [Display(Name = "Total Score")]
        public int TotalScore
        {
            get
            {
                int totalScore = 0;
                foreach (PointScore score in Score)
                    totalScore += score.Score;
                return totalScore;
            }
        }

        public int ScoreFor(string pointType)
        {
            foreach (PointScore score in Score)
            {
                if (score.PointPath.Name == pointType)
                    return score.Score;
            }
            return 0;
        }

        /// <summary>
        /// Each inidividual score. The key should be the ARG path.
        /// </summary>
        public virtual ICollection<PointScore> Score { get; set; }

        /// <summary>
        /// The task that have been completed by the player.
        /// </summary>
        public virtual ICollection<Task> CompletedTask { get; set; }
    }
}