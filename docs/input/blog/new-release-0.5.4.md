---
Title: New Release - 0.5.4
Published: 2/10/2017
Category: Release
Author: jericho
---

## New features

- [__#30__](https://github.com/cake-contrib/Cake.Email/issues/30) Only send email to recipients with an email address

## Breaking changes

- [__#29__](https://github.com/cake-contrib/Cake.Email/issues/29) Use the new Cake.Email.Common nuget package

## Note

It is recommended to add `using Cake.Email.Common;` near the top of your Cake script in order to easily add atachments to your email like in this example:
```
using Cake.Email.Common;

var attachments = new[]
{
    Attachment.FromLocalFile("C:\\temp\\MyFile.txt")
};
```

Please do not hesitate to reach out in the [Gitter Channel](https://gitter.im/cake-contrib/Lobby) if you have any issues using this addin.