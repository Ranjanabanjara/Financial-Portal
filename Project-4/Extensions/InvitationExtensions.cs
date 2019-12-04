using Microsoft.AspNet.Identity.Owin;
using Project_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Project_4.Extensions
{
    public static class InvitationExtensions
    {
        public static async Task EmailInvitation(this Invitation invitation)
        {
            var Url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var callbackUrl = Url.Action("AcceptInvitation", "Account", new { receipentEmail = invitation.ReceipentEmail, code = invitation.Code }, protocol: HttpContext.Current.Request.Url.Scheme);
            var senderEmail = $"Financial Portal<{WebConfigurationManager.AppSettings["emailto"]}>";
            var emailMessage = new MailMessage(senderEmail, invitation.ReceipentEmail)
            {
                Subject = $"You have been invited to join the Financial Portal Application!",
                Body = $"Please accept the inviation and register as a new household member <a href=\"{callbackUrl}\">here.. </a>",
                IsBodyHtml = true
            };

            var svc = new PersonalEmail();
            await svc.SendAsync(emailMessage);

        }

    }

    public static class Extensions
    {
        public static async Task RefreshAuthentication(this HttpContextBase context, ApplicationUser user)
        {
            context.GetOwinContext().Authentication.SignOut();
            await context.GetOwinContext().Get<ApplicationSignInManager>().SignInAsync(user, isPersistent: false, rememberBrowser: false);

        }

    }
   
}