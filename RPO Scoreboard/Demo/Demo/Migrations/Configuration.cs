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

        protected override void Seed(MasterContext context)
        {
            context.Database.Delete();
            context.Database.CreateIfNotExists();

            seedUser(context);
            seedTypes(context);
            seedScores(context);
        }

        private void seedUser(MasterContext context)
        {
            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "JoeJiggty"
            });

            context.SaveChanges();
        }

        private void seedTypes(MasterContext context)
        {
            context.PointTypes.Add(new PointType()
            {
                Name = "Total"
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
                    PointPath = context.PointTypes.Single(pt=>pt.Name.Equals("Total"))
                });

            }
            context.SaveChanges();
        }
    }
}
