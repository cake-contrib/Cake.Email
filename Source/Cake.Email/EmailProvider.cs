using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Email
{
    /// <summary>
    /// Contains functionality related to emails
    /// </summary>
    [CakeAliasCategory("Email")]
    public sealed class EmailProvider
    {
        private readonly ICakeContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailProvider"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EmailProvider(ICakeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Sends an email via the SendGrid API, based on the provided settings
        /// </summary>
        /// <param name="senderName">The name of the person sending the email</param>
        /// <param name="senderAddress">The email address of the person sending the email</param>
        /// <param name="recipientName">The name of the person who will receive the email</param>
        /// <param name="recipientAddress">The email address of the person who will recieve the email</param>
        /// <param name="subject">The subject line of the email</param>
        /// <param name="content">The body of the email</param>
        /// <param name="sendAsHtml">Indicates if the content should be sent as HTML</param>
        /// <param name="settings">The settings to be used when sending the email</param>
        /// <returns>An instance of <see cref="EmailResult"/> indicating success/failure</returns>
        /// <example>
        /// var smtpHost = "... your smtp host ...";
        /// var port = 1234;
        /// var enableSsl = true;
        /// var username = "... your username ...";
        /// var password = "... your password ...";
        /// try
        /// {
        ///     var result = Email.Send(
        ///         senderName: "Bob Smith",
        ///         senderAddress: "bob@example.com",
        ///         recipientName: "Jane Doe",
        ///         recipientAddress: "jane@example.com",
        ///         subject: "This is a test",
        ///         content: "<html><body>This is a test</body></html>",
        ///         sendAsHtml: true,
        ///         settings: new EmailSettings
        ///         {
        ///             SmtpHost = smtpHost,
        ///             Port = port,
        ///             EnableSsl = enableSsl,
        ///             Username = username,
        ///             Password = password
        ///         }
        ///     );
        ///     if (result.Ok)
        ///     {
        ///         Information("Email succcessfully sent");
        ///     }
        ///     else
        ///     {
        ///         Error("Failed to send email: {0}", result.Error);
        ///     }
        /// }
        /// catch(Exception ex)
        /// {
        ///     Error("{0}", ex);
        /// }
        /// </example>
        public EmailResult SendEmail(string senderName, string senderAddress, string recipientName, string recipientAddress, string subject, string content, bool sendAsHtml, EmailSettings settings)
        {
            var recipient = new MailAddress(recipientAddress, recipientName);
            return SendEmail(senderName, senderAddress, recipient, subject, content, sendAsHtml, settings);
        }

        /// <summary>
        /// Sends an email via the SendGrid API, based on the provided settings
        /// </summary>
        /// <param name="senderName">The name of the person sending the email</param>
        /// <param name="senderAddress">The email address of the person sending the email</param>
        /// <param name="recipient">The recipient who will receive the email</param>
        /// <param name="subject">The subject line of the email</param>
        /// <param name="content">The body of the email</param>
        /// <param name="sendAsHtml">Indicates if the content should be sent as HTML</param>
        /// <param name="settings">The settings to be used when sending the email</param>
        /// <returns>An instance of <see cref="EmailResult"/> indicating success/failure</returns>
        /// <example>
        /// var smtpHost = "... your smtp host ...";
        /// var port = 1234;
        /// var enableSsl = true;
        /// var username = "... your username ...";
        /// var password = "... your password ...";
        /// try
        /// {
        ///     var result = Email.Send(
        ///         senderName: "Bob Smith",
        ///         senderAddress: "bob@example.com",
        ///         recipient: new MailAddress("jane@example.com", "Jane Doe"),
        ///         subject: "This is a test",
        ///         content: "<html><body>This is a test</body></html>",
        ///         sendAsHtml: true,
        ///         settings: new EmailSettings
        ///         {
        ///             SmtpHost = smtpHost,
        ///             Port = port,
        ///             EnableSsl = enableSsl,
        ///             Username = username,
        ///             Password = password
        ///         }
        ///     );
        ///     if (result.Ok)
        ///     {
        ///         Information("Email succcessfully sent");
        ///     }
        ///     else
        ///     {
        ///         Error("Failed to send email: {0}", result.Error);
        ///     }
        /// }
        /// catch(Exception ex)
        /// {
        ///     Error("{0}", ex);
        /// }
        /// </example>
        public EmailResult SendEmail(string senderName, string senderAddress, MailAddress recipient, string subject, string content, bool sendAsHtml, EmailSettings settings)
        {
            var recipients = new[] { recipient };
            return SendEmail(senderName, senderAddress, recipients, subject, content, sendAsHtml, settings);
        }

        /// <summary>
        /// Sends an email via the SendGrid API, based on the provided settings
        /// </summary>
        /// <param name="senderName">The name of the person sending the email</param>
        /// <param name="senderAddress">The email address of the person sending the email</param>
        /// <param name="recipients">An enumeration of recipients who will receive the email</param>
        /// <param name="subject">The subject line of the email</param>
        /// <param name="content">The body of the email</param>
        /// <param name="sendAsHtml">Indicates if the content should be sent as HTML</param>
        /// <param name="settings">The settings to be used when sending the email</param>
        /// <returns>An instance of <see cref="EmailResult"/> indicating success/failure</returns>
        /// <example>
        /// var smtpHost = "... your smtp host ...";
        /// var port = 1234;
        /// var enableSsl = true;
        /// var username = "... your username ...";
        /// var password = "... your password ...";
        /// try
        /// {
        ///     var result = Email.Send(
        ///         senderName: "Bob Smith",
        ///         senderAddress: "bob@example.com",
        ///         recipient: new MailAddress("jane@example.com", "Jane Doe"),
        ///         subject: "This is a test",
        ///         content: "<html><body>This is a test</body></html>",
        ///         sendAsHtml: true,
        ///         settings: new EmailSettings
        ///         {
        ///             SmtpHost = smtpHost,
        ///             Port = port,
        ///             EnableSsl = enableSsl,
        ///             Username = username,
        ///             Password = password
        ///         }
        ///     );
        ///     if (result.Ok)
        ///     {
        ///         Information("Email succcessfully sent");
        ///     }
        ///     else
        ///     {
        ///         Error("Failed to send email: {0}", result.Error);
        ///     }
        /// }
        /// catch(Exception ex)
        /// {
        ///     Error("{0}", ex);
        /// }
        /// </example>
        public EmailResult SendEmail(string senderName, string senderAddress, IEnumerable<MailAddress> recipients, string subject, string content, bool sendAsHtml, EmailSettings settings)
        {
            try
            {
                if (recipients == null || !recipients.Any(r => r != null))
                {
                    throw new CakeException("You must specify at least one recipient");
                }

                using (var client = new SmtpClient
                {
                    Host = settings.SmtpHost,
                    Port = settings.Port,
                    EnableSsl = settings.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = string.IsNullOrEmpty(settings.Username),
                    Credentials = string.IsNullOrEmpty(settings.Username) ? null : new NetworkCredential(settings.Username, settings.Password)
                })
                {
                    var from = new MailAddress(senderAddress, senderName);

                    using (var message = new MailMessage())
                    {
                        message.Body = content;
                        message.BodyEncoding = Encoding.UTF8;
                        message.From = from;
                        message.Subject = subject;
                        message.SubjectEncoding = Encoding.UTF8;
                        message.IsBodyHtml = sendAsHtml;

                        foreach (var recipient in recipients.Where(r => r != null))
                        {
                            message.To.Add(recipient);
                        }

                        client.Send(message);
                    }
                }

                return new EmailResult(true, DateTime.UtcNow.ToString("u"), string.Empty);
            }
            catch (Exception e)
            {
                if (settings.ThrowOnFail.HasValue && settings.ThrowOnFail.Value)
                {
                    throw new CakeException(e.Message);
                }
                else
                {
                    return new EmailResult(false, DateTime.UtcNow.ToString("u"), e.Message);
                }
            }
        }
    }
}