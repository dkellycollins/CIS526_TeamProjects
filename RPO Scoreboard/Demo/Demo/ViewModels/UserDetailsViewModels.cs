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
        public List<TaskViewModel> CompletedTask { get; set; }
        public List<MilestoneViewModel> CompletedMilestones { get; set; }
    }

    public class TaskViewModel
    {
        public int TaskID { get; set; }
        public string Name { get; set; }
        public DateTime CompletedOn { get; set; }
        public int PointsEarned { get; set; }
    }

    public class MilestoneViewModel : TaskViewModel
    {
        public string ImgLink { get; set; }
    }
}