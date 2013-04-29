namespace Demo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Demo.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Demo.Models.MasterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Demo.Models.MasterContext context)
        {
            context.Database.Delete();
            context.Database.CreateIfNotExists();

            seedUser(context);
            seedTypes(context);
            seedScores(context);
            seedTasks(context);
            seedMilestones(context);
        }

        private void seedUser(MasterContext context)
        {
            Random rn = new Random();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "JoeJiggty"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Billy Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Joe Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "John Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Steve Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Mama Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Baby Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Bobby Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Red Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Green Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Blue Bob"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Blue Bob1"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Blue Bob2"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Blue Bob3"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Blue Bob4"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Blue Bob5"
            });

            context.SaveChanges();

            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "Blue Bob6"
            });

            context.SaveChanges();
        }

        private void seedTypes(MasterContext context)
        {
            context.PointTypes.Add(new PointType()
            {
                Name = "Misc"
            });

            context.SaveChanges();
        }

        private void seedScores(MasterContext context)
        {
            Random rnd = new Random();

            foreach (UserProfile user in context.UserProfiles)
            {
                /*user.Score.Add(new PointScore()
                {
                    Score = 100
                });*/

                context.PointScores.Add(new PointScore()
                {
                    UserProfile = user,
                    Score = rnd.Next(0,1000),
                    PointPath = context.PointTypes.Single(pt=>pt.Name.Equals("Misc"))
                });

            }
            context.SaveChanges();
        }

        private void seedTasks(MasterContext context)
        {
            context.Tasks.Add(new Task()
            {
                Name = "Test Task",
                Description = "This task is for testing purposes only",
                IsMilestone = false,
                Points = 100,
                BonusPoints = 0,
                MaxBonusAwards = 10,
                StartTime = DateTime.Now,
                PointPath = context.PointTypes.Single(pt=>pt.Name.Equals("Misc")),
                EndTime = DateTime.Now.AddDays(14)
            });

            context.SaveChanges();
        }

        private void seedMilestones(MasterContext context)
        {
            context.Tasks.Add(new Task()
            {
                Name = "Test Milestone",
                Description = "This milestone is for testing purposes only",
                IsMilestone = true,
                Points = 1000,
                BonusPoints = 100,
                MaxBonusAwards = 10,
                StartTime = DateTime.Now,
                PointPath = context.PointTypes.Single(pt => pt.Name.Equals("Misc")),
                EndTime = DateTime.Now.AddYears(1),
                IconLink = @"~\Content\Images\Milestones\iconLink.jpg"
            });
            context.SaveChanges();
        }
    }
}
