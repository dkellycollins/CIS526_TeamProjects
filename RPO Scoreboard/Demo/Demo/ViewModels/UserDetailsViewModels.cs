using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.ViewModels
{
    public class UserDetailsViewModel
    {
        public string UserName { get; set; }
        public Dictionary<string, int> Scores { get; set; }
        public int TotalScore { get; set; }
        public List<UserDetialsTaskViewModel> CompletedTask { get; set; }
        public List<UserDetialsMilestoneViewModel> CompletedMilestones { get; set; }
    }

    public class UserDetialsTaskViewModel
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public DateTime CompletedOn { get; set; }
        public int PointsEarned { get; set; }
    }

    public class UserDetialsMilestoneViewModel : UserDetialsTaskViewModel
    {
        public string ImgLink { get; set; }
    }
}