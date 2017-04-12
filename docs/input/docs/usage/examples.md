__Sending an email to a single recipient:__

```csharp
#addin Cake.Email

Task("SendEmail")
    .Does(() =>
{
    try
    {
        var result = Email.Send(
                senderName: "Bob Smith", 
                senderAddress: "bob@example.com",
                recipientName: "Jane Doe",
                recipientAddress: "jane@example.com",
                subject: "This is a test",
                content: "<html><body>This is a test</body></html>",
                sendAsHtml: true,
                settings: new EmailSettings 
                {
                    SmtpHost = "smtp.gmail.com",
                    Port = 587,
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
#addin Cake.Email

Task("SendEmail")
    .Does(() =>
{
    try
    {
        var recipients = 
        var result = Email.Send(
                senderName: "Bob Smith", 
                senderAddress: "bob@example.com",
                recipients: new[]
                {
                    new System.Net.Mail.MailAddress("jane@example.com", "Jane Doe"),
                    new System.Net.Mail.MailAddress("bob@example.com", "Bob Smith")
                },
                subject: "This is a test",
                content: "<html><body>This is a test</body></html>",
                sendAsHtml: true,
                settings: new EmailSettings 
                {
                    SmtpHost = "smtp.gmail.com",
                    Port = 587,
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