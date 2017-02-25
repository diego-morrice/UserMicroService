using System.Data.Entity.Migrations;

namespace Infrastructure.Data.User.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Context.UserContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Context.UserContext context)
        {

        }
    }
}