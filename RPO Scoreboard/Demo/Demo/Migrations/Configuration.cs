namespace Demo.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using Demo.Models;
    using WebMatrix.WebData;
    using Demo.Filters;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<Demo.Models.MasterContext>
    {
        List<Task> milestones = new List<Task>();

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
            //seedScores(context);
            seedTasks(context);
            seedMilestones(context);
            //seedLogin();
            seedScores(context);
            seedCompletedTasks(context);

        }

        private void seedUser(MasterContext context)
        {
            context.UserProfiles.Add(new UserProfile()
            {
                ID = 0,
                UserName = "playerOne",
                IsAdmin = true
            });            
            for (int i = 0; i < 200; i++)            
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

        public enum SeedTypes
        {
            Attendance,
            Puzzle,
            Crosscurricular,
            Cooperation,
            Story
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
                foreach (PointType pt in context.PointTypes)
                {
                    context.PointScores.Add(new PointScore()
                    {
                        UserProfile = user,
                        Score = 0,
                        PointPath = pt
                    });
                }
            }
            context.SaveChanges();
        }

        private void seedTasks(MasterContext context)
        {
            Random rnd = new Random();
            Array enumVals = Enum.GetValues(typeof(SeedTypes));
            SeedTypes st;
            string typeString = "";

            context.Tasks.Add(new Task()
            {
                Name = "Test Task",
                Description = "This task is for testing purposes only",
                IsMilestone = false,
                Points = 100,
                BonusPoints = 0,
                MaxBonusAwards = 0,
                StartTime = DateTime.Now,
                PointPath = context.PointTypes.Single(pt=>pt.Name.Equals("Puzzle")),
                EndTime = DateTime.Now.AddDays(14)
            });

            context.SaveChanges();

            for (int i = 0; i < 20; i++)
            {
                st = (SeedTypes)enumVals.GetValue(rnd.Next(enumVals.Length));
                typeString = st.ToString();

                context.Tasks.Add(new Task()
                {
                    Name = "Random Task " + i,
                    Description = "A task randomly generated at iteration " + i,
                    IsMilestone = false,
                    Points = rnd.Next(1000),
                    BonusPoints = 0,
                    MaxBonusAwards = 0,
                    StartTime = DateTime.Now,
                    PointPath = context.PointTypes.Single(pt => pt.Name.Equals(typeString)),
                    EndTime = DateTime.Now.AddHours(12141)
                });
            }
            context.SaveChanges();
        }

        String[] linkArray = {
                                @"\Content\images\Gate.png",
                                @"\Content\images\key.png",
                                @"\Content\images\quarter.png"
                             };

        private void seedMilestones(MasterContext context)
        {
            Random rnd = new Random();
            Array enumVals = Enum.GetValues(typeof(SeedTypes));
            SeedTypes st;
            Task currentMilestone;
            string typeString = "";

            st = (SeedTypes)enumVals.GetValue(rnd.Next(enumVals.Length));
            typeString = st.ToString();

            currentMilestone = new Task()
            {
                Name = "Test Milestone",
                Description = "This milestone is for testing purposes only",
                IsMilestone = true,
                Points = rnd.Next(1000),
                BonusPoints = rnd.Next(200),
                MaxBonusAwards = rnd.Next(20),
                StartTime = DateTime.Now,
                PointPath = context.PointTypes.Single(pt => pt.Name.Equals(typeString)),
                EndTime = DateTime.Now.AddYears(1),
                IconLink = linkArray[rnd.Next(3)]
            };

            context.Tasks.Add(currentMilestone);
            milestones.Add(currentMilestone);
            context.SaveChanges();

            for (int i = 0; i < 10; i++)
            {
                st = (SeedTypes)enumVals.GetValue(rnd.Next(enumVals.Length));
                typeString = st.ToString();

                currentMilestone = new Task()
                {
                    Name = "Random Milestone " + i,
                    Description = "A milestone randomly generated at iteration " + i,
                    IsMilestone = true,
                    Points = rnd.Next(1000),
                    BonusPoints = rnd.Next(200),
                    MaxBonusAwards = rnd.Next(20),
                    StartTime = DateTime.Now,
                    PointPath = context.PointTypes.Single(pt => pt.Name.Equals(typeString)),
                    EndTime = DateTime.Now.AddHours(12141),
                    IconLink = linkArray[rnd.Next(3)]
                };

                context.Tasks.Add(currentMilestone);
                milestones.Add(currentMilestone);
            }

            context.SaveChanges();
        }

        private void seedLogin()
        {
            Demo.Filters.InitializeSimpleMembershipAttribute.SimpleMembershipInitializer init = new Demo.Filters.InitializeSimpleMembershipAttribute.SimpleMembershipInitializer();

            if (!Roles.RoleExists(Util.ProjectRoles.ADMIN))
                Roles.CreateRole(Util.ProjectRoles.ADMIN);

            //This is bad. We need to change this eventually.
            if (!WebSecurity.UserExists(Util.ProjectRoles.ADMIN))
            {
                WebSecurity.CreateUserAndAccount(
                    "admin",
                    "admin");
            }
            if (!Roles.GetRolesForUser("admin").Contains(Util.ProjectRoles.ADMIN))
            {
                Roles.AddUserToRole("admin", Util.ProjectRoles.ADMIN);
            }
        }

        private void seedCompletedTasks(MasterContext context)
        {
            Random rnd = new Random();
            int nbMilestones = 0;
            int score = 0;
            Task milestone;
            PointScore selectedPointType;

            foreach (UserProfile user in context.UserProfiles)
            {
                nbMilestones = rnd.Next(milestones.Count);

                for (int i = 0; i < nbMilestones; i++)
                {
                    score = rnd.Next(1,1000);
                    milestone = milestones[rnd.Next(milestones.Count)];

                    context.CompletedTasks.Add(new CompletedTask()
                    {
                        UserProfile = user,
                        Task = milestone,
                        AwardedPoints = score,
                        CompletedDate = DateTime.Today.AddDays(rnd.Next(15)),
                    });

                    selectedPointType = context.PointScores.Single(ps => (ps.PointPath.Name == milestone.PointPath.Name) && (ps.UserProfile.ID == user.ID));
                    //throw new Exception("selected Point type = " + selectedPointType.PointPath.Name + " with " + selectedPointType.Score + " points");
                    selectedPointType.Score += score;
                }
            }
            context.SaveChanges();
        }
    }
}
