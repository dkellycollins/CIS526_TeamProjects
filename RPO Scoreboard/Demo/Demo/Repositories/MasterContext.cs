using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Demo.Models;

namespace Demo.Repositories
{
    public class MasterContext : DbContext
    {
        private static MasterContext _instance;
        public static MasterContext Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MasterContext();
                return _instance;
            }
        }

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