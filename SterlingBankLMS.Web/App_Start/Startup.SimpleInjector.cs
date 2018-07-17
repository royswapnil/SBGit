using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Nop.Core.Caching;
using Orchard.Logging;
using Rentrdoid.Common.Logger;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using SimpleSoapClient;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Database;
using SterlingBankLMS.Data.Repository;
using SterlingBankLMS.Data.UnitofWork;
using SterlingBankLMS.Web.Infrastructure.Auth;
using SterlingBankLMS.Web.Infrastructure.DataContext;
using SterlingBankLMS.Web.Infrastructure.Messaging.Email;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using WebLogic.Infrastructure.Logger;

namespace SterlingBankLMS.Web
{
    public partial class Startup
    {
        /// <summary>
        /// Construct a Dipendency Injector container and register all application dependencies
        /// </summary>
        /// <returns>container</returns>
        public static Container UseSimpleInjector()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = Lifestyle.CreateHybrid(
                                                  new WebRequestLifestyle(),
                                                  new AsyncScopedLifestyle());

            RegisterComponents(container);

            container.Verify();

            //register simple injector with mvc/api
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.Services.Add(typeof(IExceptionLogger), container.GetInstance<IExceptionLogger>());
            return container;
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

            container.Register<IWorkContext, WorkContext>(Lifestyle.Scoped);
            container.Register<IPermissionService, PermissionService>(Lifestyle.Scoped);

            container.Register<IUserStore<ApplicationUser, int>, ApplicationUserStore>(Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<IUserAccountService, UserAccountService>(Lifestyle.Scoped);

            container.Register(() => {
                IOwinContext owincontext = null;
                if (container.GetInstance<HttpContextBase>() != null && container.GetInstance<HttpContextBase>().Items[AppConstants.Owin] == null && container.IsVerifying)
                    owincontext = new OwinContext(new Dictionary<string, object>());
                else owincontext = container.GetInstance<HttpContextBase>().GetOwinContext();

                return owincontext.Authentication;
            }, Lifestyle.Scoped);

            container.Register<IRoleStore<ApplicationRole, int>, ApplicationRoleStore>(Lifestyle.Scoped);
            container.Register<ApplicationRoleManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register<IAuthenticationService, AuthenticationService>(Lifestyle.Scoped);
            container.Register<IUserRoleService, UserRoleService>(Lifestyle.Scoped);

            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(GenericRepository<>), Lifestyle.Scoped);

            container.Register<ILoggerFactory, CastleLoggerFactory>();
            container.Register<Castle.Core.Logging.ILoggerFactory>(() => new NLogFactory());
            container.RegisterSingleton(() => {
                return container.GetInstance<ILoggerFactory>().CreateLogger(AppConstants.AppErrorLogger);
            });

            container.Register<IExceptionLogger, NLogExceptionHandler>(Lifestyle.Scoped);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            var coreAssembly = typeof(UserFactory).Assembly;

            var factories = from type in coreAssembly.GetExportedTypes()
                            where type.Namespace == "SterlingBankLMS.Core.Factories"
                            select new { Implementation = type };

            foreach (var reg in factories)
                container.Register(reg.Implementation, reg.Implementation, Lifestyle.Scoped);

            container.Register<ICacheManager>(() => {
                var useRedisCache = CommonHelper.GetAppSetting<bool>(AppConstants.Keys.UseRedisCacheService);

                if (useRedisCache) {
                    //return new RedisCacheManager(CommonHelper.GetAppSetting<string>(AppConstants.Keys.RedisCacheConnectionString));
                   return new MemoryCacheManager();
                }
                else {
                    return new MemoryCacheManager();
                }

            }, Lifestyle.Scoped);

            container.Register<ISterlingActiveDirectoryService, SterlingActiveDirectoryService>(Lifestyle.Scoped);
            container.Register<ISoapClient>(() => {

                return new SoapClient(SoapVersion.Soap11);

            }, Lifestyle.Scoped);

            container.Register<IMailerService>(() => {
                var emailConfig = new MailerConfig
                {
                    SmtpEnableSSl = CommonHelper.GetAppSetting<bool>(AppConstants.Keys.SmtpUseSslKey),
                    SmtpPassword = CommonHelper.GetAppSetting<string>(AppConstants.Keys.SmtpPasswordKey),
                    SmtpPort = CommonHelper.GetAppSetting<int>(AppConstants.Keys.SmtpPortKey),
                    SmtpServer = CommonHelper.GetAppSetting<string>(AppConstants.Keys.SmtpServerKey),
                    SmtpUserName = CommonHelper.GetAppSetting<string>(AppConstants.Keys.SmtpUsernameKey),
                    SmtpUseDefaultCredentials = CommonHelper.GetAppSetting<bool>(AppConstants.Keys.SmtpUseDefautlCredentialsKey),
                };
                var logger = container.GetInstance<ILogger>();
                return new MailerService(emailConfig, logger);
            }, Lifestyle.Scoped);
        }
    }
}