__Sending an email to a single recipient:__

```csharp
#addin nuget:?package=Cake.Email&version=0.8.0&loaddependencies=true

using Cake.Email.Common;

Task("SendEmail")
    .Does(() =>
{
    try
    {
        var attachments = new[]
        {
                Attachment.FromLocalFile("C:\\temp\\MyFile.txt"),
                Attachment.FromLocalFile("C:\\temp\\MySpreadsheet.xls"),
                Attachment.FromLocalFile("C:\\temp\\MyFile.pdf"),
        };
        var result = Email.SendEmail(
                senderName: "Bob Smith", 
                senderAddress: "bob@example.com",
                recipientName: "Jane Doe",
                recipientAddress: "jane@example.com",
                subject: "This is a test",
                htmlContent: "<html><body>This is a test</body></html>",
                textContent: "This is a test"
                attachments: attachments,
                settings: new EmailSettings 
                {
                    SmtpHost = "smtp.gmail.com",
                    Port = 465,
                    EnableSsl = true,
                    Username = "my_gmail_address@gmail.com",
                    Password = "my_password"
                }
        );

        if (result.Ok)
        {
            Information("Email succcessfully sent");
        }
        else
        {
            Error("Failed to send email: {0}", result.Error);
        }
    }
    catch(Exception ex)
    {
        Error("{0}", ex);
    }
});
```

__Sending an email to multiple recipients:__

```csharp
#addin nuget:?package=Cake.Email&version=0.8.0&loaddependencies=true

using Cake.Email.Common;

Task("SendEmail")
    .Does(() =>
{
    try
    {
        var attachments = new[]
        {
                Attachment.FromLocalFile("C:\\temp\\MyFile.txt"),
                Attachment.FromLocalFile("C:\\temp\\MySpreadsheet.xls"),
                Attachment.FromLocalFile("C:\\temp\\MyFile.pdf"),
        };
        var result = Email.SendEmail(
                senderName: "Bob Smith", 
                senderAddress: "bob@example.com",
                recipients: new[]
                {
                    new Cake.Email.MailAddress("jane@example.com", "Jane Doe"),
                    new Cake.Email.MailAddress("bob@example.com", "Bob Smith")
                },
                subject: "This is a test",
                htmlContent: "<html><body>This is a test</body></html>",
                textContent: "This is a test"
                attachments: attachments,
                settings: new EmailSettings 
                {
                    SmtpHost = "smtp.gmail.com",
                    Port = 465,
                    EnableSsl = true,
                    Username = "my_gmail_address@gmail.com",
                    Password = "my_password"
                }
        );

        if (result.Ok)
        {
            Information("Email succcessfully sent");
        }
        else
        {
            Error("Failed to send email: {0}", result.Error);
        }
    }
    catch(Exception ex)
    {
        Error("{0}", ex);
    }
});
```
