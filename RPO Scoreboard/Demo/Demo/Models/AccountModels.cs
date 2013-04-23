using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    /// <summary>
    /// Contains data on a specific player.
    /// </summary>
    public class UserProfile
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        /// <summary>
        /// The player's total score.
        /// </summary>
        public int TotalScore
        {
            get
            {
                int totalScore = 0;
                //foreach (int score in Score)
                //    totalScore += score;
                return totalScore;
            }
        }

        /// <summary>
        /// Each inidividual score. The key should be the ARG path.
        /// </summary>
        public virtual PointScore Score { get; set; }

        /// <summary>
        /// The task that have been completed by the player.
        /// </summary>
        public virtual ICollection<Task> CompletedTask { get; set; }
    }
}