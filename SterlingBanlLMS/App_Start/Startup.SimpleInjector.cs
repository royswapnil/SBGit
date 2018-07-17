using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Database;
using SterlingBankLMS.Data.Repository;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Infrastructure.Auth;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utility;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SterlingBankLMS.Web
{
    public partial class Startup
    {
        public static void UseSimpleInjector()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = Lifestyle.CreateHybrid(
                                                  new WebRequestLifestyle(),
                                                  new AsyncScopedLifestyle());

            RegisterComponents(container);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void RegisterComponents(Container container)
        {
            container.Register<IDbContext>(() => {
                return new SterlingBankLmsContext();
            }, Lifestyle.Scoped);

            container.Register(() => {
                return container.GetInstance<IDbContext>() as DbContext;
            }, Lifestyle.Scoped);

            container.Register(() => {
                return new HttpContextWrapper(HttpContext.Current) as HttpContextBase;
            }, Lifestyle.Scoped);

            container.Register<IdentityFactoryOptions<ApplicationUserManager>>(Lifestyle.Scoped);
            container.Register<IUserStore<ApplicationUser, int>, ApplicationUserStore>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<IUserAccountService, UserAccountService>(Lifestyle.Scoped);

            container.Register(() => {
                IOwinContext owincontext = null;
                if (container.GetInstance<HttpContextBase>() != null && container.GetInstance<HttpContextBase>().Items[AppConstants.Keys.Owin] == null && container.IsVerifying)
                    owincontext = new OwinContext(new Dictionary<string, object>());
                else owincontext = container.GetInstance<HttpContextBase>().GetOwinContext();

                return owincontext.Authentication;
            }, Lifestyle.Scoped);

            container.Register<IRoleStore<ApplicationRole, int>, ApplicationRoleStore>(Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<IUserRoleService, UserRoleService>(Lifestyle.Scoped);

            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(GenericRepository<>), Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            var coreAssembly = typeof(UserFactory).Assembly;

            var factories = from type in coreAssembly.GetExportedTypes()
                            where type.Namespace == "SterlingBankLMS.Core.Factories"
                            where type.GetInterfaces().Any()
                            select new { Service = type.GetInterfaces().Single(), Implementation = type };

            foreach (var reg in factories)
                container.Register(reg.Service, reg.Implementation, Lifestyle.Scoped);
        }
    }
}