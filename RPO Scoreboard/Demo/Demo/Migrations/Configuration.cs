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
            seedUser(context);
            seedScores(context);
        }

        private void seedUser(MasterContext context)
        {
            context.UserProfiles.Add(new UserProfile()
            {
                UserName = "JoeJiggty"
            });
        }

        private void seedScores(MasterContext context)
        {
            foreach (UserProfile user in context.UserProfiles)
            {
                user.Score.Add(new PointScore()
                {
                    Score = 100
                });
            }
        }
    }
}
