using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    /// <summary>
    /// Contains data on a specific player.
    /// </summary>
    public class PlayerProfile
    {
        public int ID { get; set; }

        /// <summary>
        /// The player's total score.
        /// </summary>
        public int TotalScore
        {
            get
            {
                int totalScore = 0;
                foreach (int score in Score.Values)
                    totalScore += score;
                return totalScore;
            }
        }

        /// <summary>
        /// Each inidividual score. The key should be the ARG path.
        /// </summary>
        public Dictionary<string, int> Score { get; set; }

        /// <summary>
        /// The task that have been completed by the player.
        /// </summary>
        public virtual ICollection<Task> CompletedTask { get; set; }

        /// <summary>
        /// The milestones that have been completed by the player.
        /// </summary>
        public virtual ICollection<MilestoneTask> CompletedMileStone { get; set; }
    }
}