using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Demo.Models;

namespace Demo.ViewModels
{
    public class ScoreboardViewModel
    {
        [Display(Name = "Scoreboard")]
        public List<ScoreboardRecord> Scoreboard { get; set; }

        [Display(Name = "Point Types")]
        public ScoreboardPointType ScoreboardPointTypes { get; set; }

        [Display(Name = "Page Number")]
        public int PageNumber { get; set; }
    }

    public class ScoreboardRecord
    {
        [Display(Name = "User")]
        public UserProfile User { get; set; }

        [Display(Name = "Score")]
        public int Score { get; set; }
    }

    public class ScoreboardJsonResult
    {
        [Display(Name = "Page Number")]
        public int PageNum { get; set; }

        [Display(Name = "Finished Loading")]
        public bool FinishedLoading { get; set; }

        [Display(Name = "Scoreboard")]
        public List<ScoreboardUser> Users { get; set; }
    }

    public class ScoreboardUser
    {
        [Display(Name = "Username")]
        public String Username { get; set; }

        [Display(Name = "Score")]
        public int Score { get; set; }

        [Display(Name = "Milestones")]
        public List<ScoreboardMilestone> Milestones { get; set; }
    }

    public class ScoreboardMilestone
    {
        [Display(Name = "Name")]
        public String Name { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "IconLink")]
        public String IconLink { get; set; }
    }

    public class ScoreboardPointType
    {
        [Display(Name = "Point Types")]
        public List<PointType> PointTypes { get; set; }

        [Display(Name = "Selected Point Type")]
        public string SelectedPointType { get; set; }
    }
}