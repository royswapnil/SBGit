namespace SterlingBankLMS.Web.Migrations
{
    using SterlingBankLMS.Web.Infrastructure.DataContext;
    using SterlingBankLMS.Web.Migrations.Seed;
    using SterlingBankLMS.Web.Utilities;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.IO;

    internal sealed class Configuration : DbMigrationsConfiguration<SterlingBankLmsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed( SterlingBankLmsContext context )
        {
            var sql = File.ReadAllText(CommonHelper.MapPath("~/App_Data/dbo.sql"));
            context.Database.ExecuteSqlCommand(sql);

            var userId = UserSeed.InsertAdminUser(context).Result;

            var org = OurDBSeed.CreateSterlingOrganization(context, userId);
            PermissionSeed.InsertDefaultRolesAndPermissions(context, org.Id);

            UserSeed.InsertOtherRoleUsers(context, org.Id).Wait();
            UserSeed.InsertTestUsers(context, org.Id).Wait();

            OurDBSeed.CreateDB(context, org.Id);
            OurDBSeed.UpdateUsersDetails(context);

            TrainingSeed.CreateTrainings(context);
            BudgetExpenditure.CreateExpenditure(context);


            //Start sql dependency
            string connectionString = ConfigurationManager.ConnectionStrings["SterlingBankDbContext"].ConnectionString;
            SqlDependency.Start(connectionString);


            ///leave commented out
            //var usersql = File.ReadAllText(CommonHelper.MapPath("~/App_Data/tempuser.sql"));
            //context.Database.ExecuteSqlCommand(usersql);
            //UserSeed.InsertRoleForUser(context).Wait();

        }


        public static void InitializeDb()
        {
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<SterlingBankLmsContext, Configuration>());

            //Newly Added
            SterlingBankLmsContext contextnewdb = new SterlingBankLmsContext();
            bool exists = contextnewdb.Database.CreateIfNotExists();
            if (exists)
            {
                new Configuration().Seed(contextnewdb);
            }
        }

    }
}