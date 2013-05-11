using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Demo.Models;

namespace Demo.ViewModels
{
    public class TaskViewModel
    {
        [Display(Name = "Milestones")]
        public List<Task> MileStones { get; set; }

        [Display(Name = "Tasks")]
        public List<Task> Tasks { get; set; }
    }

    public class TaskCompleteViewModel
    {
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string TaskToken { get; set; }
    }
}