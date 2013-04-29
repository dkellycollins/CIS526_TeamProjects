namespace Demo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using Demo.Models;
    using WebMatrix.WebData;

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
            seedLogin();
        }

        private void seedUser(MasterContext context)
        {
            for (int i = 0; i < 1000; i++)
            {
                context.UserProfiles.Add(new UserProfile()
                {
                    UserName = "Joe Jiggty " + i
                });
            }
            context.SaveChanges();
        }

        private void seedTypes(MasterContext context)
        {
            context.PointTypes.Add(new PointType()
            {
                Name = "Attendance"
            });

            context.PointTypes.Add(new PointType()
            {
                Name = "Puzzle"
            });

            context.PointTypes.Add(new PointType()
            {
                Name = "Crosscurricular"
            });

            context.PointTypes.Add(new PointType()
            {
                Name = "Cooperation"
            });

            context.PointTypes.Add(new PointType()
            {
                Name = "Story"
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
                    PointPath = context.PointTypes.Single(pt=>pt.Name.Equals("Story"))
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
                PointPath = context.PointTypes.Single(pt=>pt.Name.Equals("Puzzle")),
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
                PointPath = context.PointTypes.Single(pt => pt.Name.Equals("Attendance")),
                EndTime = DateTime.Now.AddYears(1),
                IconLink = @"~\Content\Images\Milestones\iconLink.jpg"
            });
            context.SaveChanges();
        }

        private void seedLogin()
        {
            Demo.Filters.InitializeSimpleMembershipAttribute.SimpleMembershipInitializer init = new Demo.Filters.InitializeSimpleMembershipAttribute.SimpleMembershipInitializer();

            if (!Roles.RoleExists("admin"))
                Roles.CreateRole("admin");

            //This is bad. We need to change this eventually.
            if (!WebSecurity.UserExists("admin"))
            {
                WebSecurity.CreateUserAndAccount(
                    "admin",
                    "admin");
            }
            if (!Roles.GetRolesForUser("admin").Contains("admin"))
            {
                Roles.AddUserToRole("admin", "admin");
            }
        }
    }
}
