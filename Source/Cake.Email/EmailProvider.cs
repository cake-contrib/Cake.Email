using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Cake.Email.Common;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

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
		/// Sends an email via SMTP, based on the provided settings
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
		/// <code>
		/// using Cake.Email.Common;
		///
		/// var smtpHost = "... your smtp host ...";
		/// var port = 1234;
		/// var enableSsl = true;
		/// var username = "... your username ...";
		/// var password = "... your password ...";
		/// var attachments = new[]
		/// {
		///     Attachment.FromLocalFile("C:\\temp\\MyFile.txt"),
		///     attachment.FromLocalFile("C:\\temp\\MySpreadsheet.xls"),
		///     Attachment.FromLocalFile("C:\\temp\\MyFile.pdf")
		/// };
		/// try
		/// {
		///     var result = Email.SendEmail(
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
		/// </code>
		/// </example>
		public EmailResult SendEmail(string senderName, string senderAddress, string recipientName, string recipientAddress, string subject, string htmlContent, string textContent, IEnumerable<Attachment> attachments, EmailSettings settings)
		{
			var recipient = new MailAddress(recipientAddress, recipientName);
			return SendEmail(senderName, senderAddress, recipient, subject, htmlContent, textContent, attachments, settings);
		}

		/// <summary>
		/// Sends an email via SMTP, based on the provided settings
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
		/// <code>
		/// using Cake.Email.Common;
		///
		/// var smtpHost = "... your smtp host ...";
		/// var port = 1234;
		/// var enableSsl = true;
		/// var username = "... your username ...";
		/// var password = "... your password ...";
		/// var attachments = new[]
		/// {
		///     Attachment.FromLocalFile("C:\\temp\\MyFile.txt"),
		///     Attachment.FromLocalFile("C:\\temp\\MySpreadsheet.xls"),
		///     Attachment.FromLocalFile("C:\\temp\\MyFile.pdf")
		/// };
		/// try
		/// {
		///     var result = Email.SendEmail(
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
		/// </code>
		/// </example>
		public EmailResult SendEmail(string senderName, string senderAddress, MailAddress recipient, string subject, string htmlContent, string textContent, IEnumerable<Attachment> attachments, EmailSettings settings)
		{
			var recipients = new[] { recipient };
			return SendEmail(senderName, senderAddress, recipients, subject, htmlContent, textContent, attachments, settings);
		}

		/// <summary>
		/// Sends an email via SMTP, based on the provided settings
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
		/// <code>
		/// using Cake.Email.Common;
		///
		/// var smtpHost = "... your smtp host ...";
		/// var port = 1234;
		/// var enableSsl = true;
		/// var username = "... your username ...";
		/// var password = "... your password ...";
		/// var attachments = new[]
		/// {
		///     Attachment.FromLocalFile("C:\\temp\\MyFile.txt"),
		///     Attachment.FromLocalFile("C:\\temp\\MySpreadsheet.xls"),
		///     Attachment.FromLocalFile("C:\\temp\\MyFile.pdf")
		/// };
		/// try
		/// {
		///     var result = Email.SendEmail(
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
		/// </code>
		/// </example>
		public EmailResult SendEmail(string senderName, string senderAddress, IEnumerable<MailAddress> recipients, string subject, string htmlContent, string textContent, IEnumerable<Attachment> attachments, EmailSettings settings)
		{
			try
			{
				if (recipients == null || !recipients.Any(r => r != null))
				{
					throw new CakeException("You must specify at least one recipient");
				}

				var safeRecipients = recipients.Where(r => r != null && !string.IsNullOrEmpty(r.Address));
				if (!safeRecipients.Any())
				{
					throw new CakeException("None of the recipients you specified have an email address");
				}

				if (attachments == null)
				{
					attachments = Enumerable.Empty<Attachment>();
				}

				using (var client = new SmtpClient())
				{
					_context.Log.Verbose("Sending email to {0} via SMTP...", string.Join(", ", safeRecipients.Select(r => r.Address).ToArray()));

					client.Connect(settings.SmtpHost, settings.Port, settings.EnableSsl);

					if (!string.IsNullOrEmpty(settings.Username))
					{
						client.Authenticate(settings.Username, settings.Password);
					}

					var from = new MailboxAddress(senderName, senderAddress);

					var message = new MimeMessage();
					message.From.Add(from);
					message.Subject = subject;

					foreach (var recipient in safeRecipients)
					{
						message.To.Add(new MailboxAddress(recipient.Name, recipient.Address));
					}

					/*
					   IMPORTANT: the order of body parts is significant.
					   Parts in a multipart MIME message should be in order of increasing preference
					   See: https://www.ietf.org/rfc/rfc1521.txt (section 7.2.3)
				   */
					var content = new MultipartAlternative();

					if (!string.IsNullOrEmpty(textContent))
					{
						content.Add(new TextPart("plain") { Text = textContent });
					}

					if (!string.IsNullOrEmpty(htmlContent))
					{
						content.Add(new TextPart("html") { Text = htmlContent });
					}

					var multipart = new Multipart("mixed")
					{
						content
					};

					foreach (var attachment in attachments)
					{
						var disposition = string.IsNullOrEmpty(attachment.ContentId) ? ContentDisposition.Attachment : ContentDisposition.Inline;
						multipart.Add(new MimePart(attachment.MimeType)
						{
							ContentId = attachment.ContentId,
							Content = new MimeContent(attachment.ContentStream, ContentEncoding.Default),
							ContentDisposition = new ContentDisposition(disposition),
							ContentTransferEncoding = ContentEncoding.Base64,
							FileName = attachment.Name
						});
					}

					message.Body = multipart;

					client.Send(message);

					client.Disconnect(true);
				}

				return new EmailResult(true, DateTime.UtcNow.ToString("u"), string.Empty);
			}
			catch (Exception e)
			{
				while (e.InnerException != null)
				{
					e = e.InnerException;
				}

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
