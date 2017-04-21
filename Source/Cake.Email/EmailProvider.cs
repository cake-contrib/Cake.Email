using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using Cake.Core;
using Cake.Core.Annotations;
using HeyRed.Mime;

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
        /// <param name="htmlContent">The HTML content of the email</param>
        /// <param name="textContent">The text content of the email</param>
        /// <param name="attachments">Attachments to send with the email</param>
        /// <param name="settings">The settings to be used when sending the email</param>
        /// <returns>An instance of <see cref="EmailResult"/> indicating success/failure</returns>
        /// <example>
        /// var smtpHost = "... your smtp host ...";
        /// var port = 1234;
        /// var enableSsl = true;
        /// var username = "... your username ...";
        /// var password = "... your password ...";
        /// var attachments = new[]
        /// {
        ///     Email.CreateAttachmentFromLocalFile("C:\\temp\\MyFile.txt"),
        ///     Email.CreateAttachmentFromLocalFile("C:\\temp\\MySpreadsheet.xls"),
        ///     Email.CreateAttachmentFromLocalFile("C:\\temp\\MyFile.pdf"),
        /// };
        /// try
        /// {
        ///     var result = Email.Send(
        ///         senderName: "Bob Smith",
        ///         senderAddress: "bob@example.com",
        ///         recipientName: "Jane Doe",
        ///         recipientAddress: "jane@example.com",
        ///         subject: "This is a test",
        ///         htmlContent: "<html><body>This is a test</body></html>",
        ///         textContent: "This is the text only version of this email",
        ///         attachments: attachments,
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
        public EmailResult SendEmail(string senderName, string senderAddress, string recipientName, string recipientAddress, string subject, string htmlContent, string textContent, IEnumerable<AttachmentBase> attachments, EmailSettings settings)
        {
            var recipient = new MailAddress(recipientAddress, recipientName);
            return SendEmail(senderName, senderAddress, recipient, subject, htmlContent, textContent, attachments, settings);
        }

        /// <summary>
        /// Sends an email via the SendGrid API, based on the provided settings
        /// </summary>
        /// <param name="senderName">The name of the person sending the email</param>
        /// <param name="senderAddress">The email address of the person sending the email</param>
        /// <param name="recipient">The recipient who will receive the email</param>
        /// <param name="subject">The subject line of the email</param>
        /// <param name="htmlContent">The HTML content of the email</param>
        /// <param name="textContent">The text content of the email</param>
        /// <param name="attachments">Attachments to send with the email</param>
        /// <param name="settings">The settings to be used when sending the email</param>
        /// <returns>An instance of <see cref="EmailResult"/> indicating success/failure</returns>
        /// <example>
        /// var smtpHost = "... your smtp host ...";
        /// var port = 1234;
        /// var enableSsl = true;
        /// var username = "... your username ...";
        /// var password = "... your password ...";
        /// var attachments = new[]
        /// {
        ///     Email.CreateAttachmentFromLocalFile("C:\\temp\\MyFile.txt"),
        ///     Email.CreateAttachmentFromLocalFile("C:\\temp\\MySpreadsheet.xls"),
        ///     Email.CreateAttachmentFromLocalFile("C:\\temp\\MyFile.pdf"),
        /// };
        /// try
        /// {
        ///     var result = Email.Send(
        ///         senderName: "Bob Smith",
        ///         senderAddress: "bob@example.com",
        ///         recipient: new MailAddress("jane@example.com", "Jane Doe"),
        ///         subject: "This is a test",
        ///         htmlContent: "<html><body>This is a test</body></html>",
        ///         textContent: "This is the text only version of this email",
        ///         attachments: attachments,
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
        public EmailResult SendEmail(string senderName, string senderAddress, MailAddress recipient, string subject, string htmlContent, string textContent, IEnumerable<AttachmentBase> attachments, EmailSettings settings)
        {
            var recipients = new[] { recipient };
            return SendEmail(senderName, senderAddress, recipients, subject, htmlContent, textContent, attachments, settings);
        }

        /// <summary>
        /// Sends an email via the SendGrid API, based on the provided settings
        /// </summary>
        /// <param name="senderName">The name of the person sending the email</param>
        /// <param name="senderAddress">The email address of the person sending the email</param>
        /// <param name="recipients">An enumeration of recipients who will receive the email</param>
        /// <param name="subject">The subject line of the email</param>
        /// <param name="htmlContent">The HTML content of the email</param>
        /// <param name="textContent">The text content of the email</param>
        /// <param name="attachments">Attachments to send with the email</param>
        /// <param name="settings">The settings to be used when sending the email</param>
        /// <returns>An instance of <see cref="EmailResult"/> indicating success/failure</returns>
        /// <example>
        /// var smtpHost = "... your smtp host ...";
        /// var port = 1234;
        /// var enableSsl = true;
        /// var username = "... your username ...";
        /// var password = "... your password ...";
        /// var attachments = new[]
        /// {
        ///     Email.CreateAttachmentFromLocalFile("C:\\temp\\MyFile.txt"),
        ///     Email.CreateAttachmentFromLocalFile("C:\\temp\\MySpreadsheet.xls"),
        ///     Email.CreateAttachmentFromLocalFile("C:\\temp\\MyFile.pdf"),
        /// };
        /// try
        /// {
        ///     var result = Email.Send(
        ///         senderName: "Bob Smith",
        ///         senderAddress: "bob@example.com",
        ///         recipients: new[]
        ///         {
        ///             new MailAddress("jane@example.com", "Jane Doe"),
        ///             new MailAddress("bod@example.com", "Bob Smith"),
        ///         },
        ///         subject: "This is a test",
        ///         htmlContent: "<html><body>This is a test</body></html>",
        ///         textContent: "This is the text only version of this email",
        ///         attachments: attachments,
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
        public EmailResult SendEmail(string senderName, string senderAddress, IEnumerable<MailAddress> recipients, string subject, string htmlContent, string textContent, IEnumerable<AttachmentBase> attachments, EmailSettings settings)
        {
            try
            {
                if (recipients == null || !recipients.Any(r => r != null))
                {
                    throw new CakeException("You must specify at least one recipient");
                }

                if (attachments == null)
                {
                    attachments = Enumerable.Empty<AttachmentBase>();
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
                        message.From = from;
                        message.Subject = subject;
                        message.SubjectEncoding = Encoding.UTF8;

                        foreach (var recipient in recipients.Where(r => r != null))
                        {
                            message.To.Add(recipient);
                        }

                        foreach (var attachment in attachments.OfType<Attachment>())
                        {
                            message.Attachments.Add(attachment);
                        }

                        /*
                            IMPORTANT: the order of body parts is significant.
                            Parts in a multipart MIME message should be in order of increasing preference
                            See: https://www.ietf.org/rfc/rfc1521.txt (section 7.2.3)
                        */

                        if (!string.IsNullOrEmpty(textContent))
                        {
                            var textView = AlternateView.CreateAlternateViewFromString(textContent, Encoding.UTF8, "text/plain");
                            message.AlternateViews.Add(textView);
                        }

                        if (!string.IsNullOrEmpty(htmlContent))
                        {
                            var htmlView = AlternateView.CreateAlternateViewFromString(htmlContent, Encoding.UTF8, "text/html");
                            foreach (var item in attachments.OfType<LinkedResource>())
                            {
                                htmlView.LinkedResources.Add(item);
                            }

                            message.AlternateViews.Add(htmlView);
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

        /// <summary>
        /// Convenience method that creates an attachment from a local file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="mimeType">Optional: MIME type of the attachment. If this parameter is null, the MIME type will be derived from the file extension.</param>
        /// <param name="contentId">Optional: the unique identifier for this attachment IF AND ONLY IF the attachment should be embedded in the body of the email. This is useful, for example, if you want to embbed an image to be displayed in the HTML content. For standard attachment, this value should be null.</param>
        /// <returns>The attachment</returns>
        /// <exception cref="System.IO.FileNotFoundException">Unable to find the local file</exception>
        public AttachmentBase CreateAttachmentFromLocalFile(string filePath, string mimeType = null, string contentId = null)
        {
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("Unable to find the local file", filePath);
            }

            if (string.IsNullOrEmpty(mimeType))
            {
                mimeType = MimeTypesMap.GetMimeType(filePath);
            }

            if (string.IsNullOrEmpty(contentId))
            {
                var attachment = new Attachment(filePath, mimeType);
                attachment.Name = Path.GetFileName(filePath);
                attachment.ContentDisposition.CreationDate = fileInfo.CreationTime;
                attachment.ContentDisposition.ModificationDate = fileInfo.LastWriteTime;
                attachment.ContentDisposition.ReadDate = fileInfo.LastAccessTime;
                return attachment;
            }
            else
            {
                var linkedResource = new LinkedResource(filePath);
                linkedResource.ContentId = contentId;
                linkedResource.ContentType.Name = Path.GetFileName(filePath);
                linkedResource.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                return linkedResource;
            }
        }

        /// <summary>
        /// Convenience method that creates an attachment from a stream.
        /// </summary>
        /// <param name="contentStream">The stream.</param>
        /// <param name="fileName">The name of the attachment.</param>
        /// <param name="mimeType">Optional: MIME type of the attachment. If this parameter is null, the MIME type will be derived from the fileName extension.</param>
        /// <param name="contentId">Optional: the unique identifier for this attachment IF AND ONLY IF the attachment should be embedded in the body of the email. This is useful, for example, if you want to embbed an image to be displayed in the HTML content. For standard attachment, this value should be null.</param>
        /// <returns>The attachment</returns>
        public AttachmentBase CreateAttachmentFromStream(Stream contentStream, string fileName, string mimeType = null, string contentId = null)
        {
            if (string.IsNullOrEmpty(mimeType))
            {
                mimeType = MimeTypesMap.GetMimeType(fileName);
            }

            if (string.IsNullOrEmpty(contentId))
            {
                var attachment = new Attachment(contentStream, mimeType);
                attachment.Name = Path.GetFileName(fileName);
                return attachment;
            }
            else
            {
                var linkedResource = new LinkedResource(contentStream);
                linkedResource.ContentId = contentId;
                linkedResource.ContentType.Name = Path.GetFileName(fileName);
                linkedResource.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                return linkedResource;
            }
        }
    }
}