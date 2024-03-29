﻿using Project_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Project_4.Helpers
{
    public class EmailHelper
    {

     
        
            private static string ConfiguredEmail = WebConfigurationManager.AppSettings["emailto"];
            public static async Task ComposeEmailAsync(EmailModel email)
            {
                try
                {
                    var senderEmail = $"{email.FromEmail}<{ConfiguredEmail}>";
                    var mailMsg = new MailMessage(senderEmail, ConfiguredEmail)
                    {
                        Subject = email.Subject,
                        Body = $"<strong>{email.FromName} has sent you the following message</strong><hr/>{email.Body}",
                        IsBodyHtml = true

                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(mailMsg);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);


                }

            }
            public static async Task ComposeEmailAsync(RegisterViewModel model, string callbackUrl)
            {
                try
                {
                    var senderEmail = $"Financial Portal Admin<{ConfiguredEmail}>";
                    var mailMsg = new MailMessage(senderEmail, model.Email)
                    {
                        Subject = "Account Confirmation",
                        Body = $" :) Thank you for joining.Please <a href=\"{callbackUrl}\"> Click here! <a/>  to confirm your account.",
                        IsBodyHtml = true

                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(mailMsg);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);


                }

            }
            public static async Task ComposeEmailAsync(ForgotPasswordViewModel model, string callbackUrl)
            {
                try
                {
                    var senderEmail = $"Financial Portal Admin<{ConfiguredEmail}>";
                    var mailMsg = new MailMessage(senderEmail, model.Email)
                    {
                        Subject = "Reset Password",
                        Body = $"Please <a href=\"{callbackUrl}\"> Click here! <a/> to reset your password.",
                        IsBodyHtml = true

                    };
                    var svc = new PersonalEmail();
                    await svc.SendAsync(mailMsg);


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);


                }

            }

     }
}
