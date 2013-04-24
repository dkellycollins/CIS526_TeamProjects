using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class MasterContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<CompletedTask> CompletedTasks { get; set; }
        public DbSet<PointType> PointTypes { get; set; }
        public DbSet<PointScore> PointScores { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public MasterContext()
            : base("DefaultConnection")
        { }
    }
}