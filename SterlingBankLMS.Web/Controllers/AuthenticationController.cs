using Microsoft.Owin.Security;
using SterlingBankLMS.Core.Factories;
using SterlingBankLMS.Data.Models.Entities;
using SterlingBankLMS.Data.Models.Enums;
using SterlingBankLMS.Web.App_Start.Filters;
using SterlingBankLMS.Web.Infrastructure.Messaging.Email;
using SterlingBankLMS.Web.Infrastructure.Services;
using SterlingBankLMS.Web.Models.IdentityModels;
using SterlingBankLMS.Web.Utilities;
using SterlingBankLMS.Web.Utilities.Extensions;
using SterlingBankLMS.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SterlingBankLMS.Web.Controllers
{
    /// <summary>
    /// Authentication Controller
    /// </summary>
    public class AuthenticationController : BaseController
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMailerService _mailerService;
        private readonly ISterlingActiveDirectoryService _sterlingADService;

        private readonly MailsFactory _mailFactory;
        private readonly UserFactory _userFactory;
        private readonly HttpContextBase _httpCxt;
        private readonly MailExemptedUsersFactory _mailExemptedUsersFactory;

        public AuthenticationController(
            IUserAccountService userAccountService, IAuthenticationService authenticationService,
            UserFactory userFactory, IMailerService mailerService,
            MailsFactory mailFactory, HttpContextBase httpCtxt,
            ISterlingActiveDirectoryService sterlingADService,
            MailExemptedUsersFactory mailExemptedUsersFactory
            )
        {
            _userAccountService = userAccountService;
            _authenticationService = authenticationService;
            _mailerService = mailerService;
            _mailFactory = mailFactory;
            _sterlingADService = sterlingADService;
            _userFactory = userFactory;
            _httpCxt = httpCtxt;
            _mailExemptedUsersFactory = mailExemptedUsersFactory;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [RedirectAuthenticatedRequest]
        public ActionResult Login()
        {
            return View(new LoginModel { RememberMe = true });
        }

        /// <summary>
        /// Authenticate email and password against  SterlingBank active directory Service or Application Persistence Layer
        /// </summary>
        /// <param name="model"> LoginModel</param>
        /// <param name="location">return URL</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [RedirectAuthenticatedRequest]
        public async Task<ActionResult> Login(LoginModel model, string location)
        {
            ApplicationUser userAccount = null;

            if (ModelState.IsValid) {

                var useActiveDirectory = CommonHelper.GetAppSetting<bool>(AppConstants.Keys.UseActiveDirectoryKey);

                if (useActiveDirectory) {
                    var isAdUser = await _sterlingADService.ValidateUser(model.Email, model.Password);

                    if (isAdUser) {

                        userAccount = _userAccountService.FindUserByEmail(model.Email);

                        //Covers new AD user without active account on the system
                        if (userAccount == null) {
                            userAccount = CreateNewEmployeeAccount(model);
                        }
                    }
                    else {
                        userAccount = _userAccountService.FindUserByEmail(model.Email);
                        if (userAccount == null || !_userAccountService.ValidPassword(userAccount, model.Password)) {
                            userAccount = null;
                        }
                    }
                }
                else {
                    userAccount = _userAccountService.FindUserByEmail(model.Email);
                    if (userAccount == null || !_userAccountService.ValidPassword(userAccount, model.Password)) {
                        userAccount = null;
                    }
                }

                if (userAccount.IsNull()) {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(model);
                }

                if (!userAccount.IsAdminUser()) {

                    if (userAccount.HasExceededExpiryDate()) {
                        userAccount.LockoutEnabled = true;
                        _userAccountService.Update(userAccount);
                    }

                    if (userAccount.AccountLocked()) {
                        ModelState.AddModelError("", "Account locked. Please contact administrator.");
                        return View(model);
                    }

                    if (userAccount.IsNormalUser() && userAccount.EmailNotConfirmed()) {
                        ModelState.AddModelError("", "Account not confirmed. Please check your email for account activation link.");
                        return View(model);
                    }
                }

                //valid user. proceed to create claims identity
                await _userAccountService.UpdateSecurityStampAsync(userAccount.Id);
                var identity = await _userAccountService.CreateIdentityAsync(userAccount, AppConstants.AuthenticationType);

                //sign in the claims
                _authenticationService.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

                //take me to previous location or home
                return RedirectOrHome(location);
            }

            return View(model);
        }

        private ApplicationUser CreateNewEmployeeAccount(LoginModel login)
        {
            var userAccount = new ApplicationUser
            {
                UserName = login.Email,
                Email = login.Email,
                UserType = UserType.ActiveDirectory,
            };

            _userAccountService.CreateUser(userAccount);
            _userAccountService.AddToRole(userAccount.Id, AppConstants.Role.Employee);

            var user = new User
            {
                ApplicationUserId = userAccount.Id,
            };

            _userFactory.Add(user);

            InsertNewUserMail(user);

            return userAccount;
        }

        /// <summary>
        /// Logout and redirect to a specified Url
        /// </summary>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Logout()
        {
            _authenticationService.SignOut(AppConstants.AuthenticationType);
            return RedirectOrHome();
        }

        [AllowAnonymous]
        [RedirectAuthenticatedRequest]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [RedirectAuthenticatedRequest]
        public async Task<ActionResult> ForgotPassword(ForgetPasswordModel model)
        {
            if (ModelState.IsValid) {
                var user = await _userAccountService.FindUserByEmailAsync(model.Email);

                if (!user.IsNull()) {

                    if (user.IsActiveDirectoryUser()) {
                        ModelState.AddModelError("", "Please contact your administrator for password reset.");
                        return View(model);
                    }

                    var resetToken = await _userAccountService.GeneratePasswordResetTokenAsync(user.Id);

                    var appBaseurl = _httpCxt.Request.Url.GetLeftPart(UriPartial.Authority);

                    await SendPasswordResetTokenEmail(user, resetToken, appBaseurl);

                    return RedirectToAction("PasswordResetStatus", new { area = "" });
                }
                else {

                    ModelState.AddModelError("", "Your input is invalid. Please check and try again.");
                    return View(model);
                }
            }
            ModelState.AddModelError("", "Your input is invalid. Please check and try again.");
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [RedirectAuthenticatedRequest]
        public async Task<ActionResult> ResetPassword(PasswordResetModel vm)
        {
            if (ModelState.IsValid) {
                var userAcct = _userAccountService.FindUserByEmail(vm.Email);

                if (userAcct.IsNull())
                    ModelState.AddModelError("", "Your input is invalid. Please check and try again.");

                else {
                    var result = await _userAccountService.ResetPasswordAsync(userAcct.Id, vm.Code, vm.Password);
                    if (result.Succeeded) {
                        TempData["notification.success"] = "Password reset was successful.";
                        return RedirectToAction("login", new { area = "" });
                    }
                }
            }
            return View(vm);
        }

        [AllowAnonymous]
        [RedirectAuthenticatedRequest]
        public ActionResult ResetPassword([Required] string resettoken)
        {
            if (ModelState.IsValid) {
                var errors = new List<string>();
                var resetModel = DataEncoder.Unprotect<PasswordResetModel>(resettoken);

                if (resetModel.IsValid(out errors))
                    return View(resetModel);

                return Content(errors.FirstOrDefault());
            }

            return Content("An error occured. Please try again.");
        }

        public async Task SendPasswordResetTokenEmail(ApplicationUser user, string code, string appBaseUrl)
        {
            try {

                var mailTemplatePath = CommonHelper.MapPath(AppConstants.PasswordReset, false);
                var appName = "SterlingBank LMS";
                var subject = $"{appName} - Reset Password Request";

                var appEmail = CommonHelper.GetAppSetting<string>(AppConstants.Keys.AppEmailKey);
                var mail = new ApplicationEmail(appEmail, subject, user.Email)
                {
                    SenderDisplayName = appName,
                    BodyIsFile = true,
                    BodyPath = mailTemplatePath
                };

                var passwordResetToken = new PasswordResetModel { Code = code, Email = user.Email };
                var activationLink = string.Format("{0}/authentication/resetpassword?resettoken={1}", appBaseUrl, DataEncoder.Protect(passwordResetToken));

                var replacement = new StringDictionary
                {
                    ["username"] = user.UserName,
                    ["activationLink"] = activationLink,
                    ["siteUrl"] = appBaseUrl,
                    ["appName"] = appName,
                };

                await _mailerService.SendMailAsync(mail, replacement);
            }
            catch (Exception) {
                // ignored
            }
        }

        [RedirectAuthenticatedRequest]
        [AllowAnonymous]
        public ActionResult PasswordResetStatus()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            //Response.StatusCode = (int) HttpStatusCode.Forbidden;
            return View();
        }

        private void InsertNewUserMail(User user)
        {
            var mail = new Mails
            {
                Subject = "New Employee Account",
                MailType = NotificationType.NewAccount,
                MailBody = "New Active Directory User not found on portal just logged in",
                MailTo = CommonHelper.GetAppSetting<string>(AppConstants.Keys.AdminEmailKey),
                MailFrom = CommonHelper.GetAppSetting<string>(AppConstants.Keys.AppEmailKey),

            };

            mail.CreatedById = mail.LastModifiedById = user.Id;
            mail.ModifiedDate = mail.CreatedDate = CommonHelper.GetCurrentDate();
            _mailFactory.Add(mail);
        }

        public ActionResult Mailexempt(NotificationType emailType, int userId)
        {
            var user = _userAccountService.FindUserById(userId);
            if (user != null) {
                ViewBag.UserEmail = user.Email;

                var exempt = new MailExemptedUsers
                {
                    CreatedById = userId,
                    CreatedDate = DateTime.Now,
                    MailType = emailType,
                    OrganizationId = user.OrganizationId ?? 0,
                    UserId = user.Id
                };

                _mailExemptedUsersFactory.Add(exempt);
                return View();
            }
            return RedirectToAction("AccessDenied");
        }

    }
}