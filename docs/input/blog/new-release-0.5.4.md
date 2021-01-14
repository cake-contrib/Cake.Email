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

First, include a reference to this addin in your script like this:
```
#addin nuget:?package=Cake.Email
```

Second, we highly recommend that you add the following 'using' statement in your script. Technically, this is not necesary, but it simplifies dealing with attachements: 
```
using Cake.Email.Common;
```

Also, this addin is designed to take advantage of some of the new features released in CakeBuild version `0.22.0`. Having said that, a [bug](https://github.com/cake-build/cake/issues/1838) was discovered in `0.22.0` and fixed in `0.23.0` therefore you need to ensure to ensure that version (or more recent).
Your `tools\package.config` should be:
```
<packages>
    <package id="Cake" version="0.23.0" />
</packages>
```

Finally, and this is critical, you need to "opt-in" the new feature in CakeBuild that this addin depends on. If you are using the standard bootstrapper, you opt-in these feature like so:
```
.\build.ps1 --nuget_useinprocessclient=true --nuget_loaddependencies=true
```

Please do not hesitate to reach out in the [GitHub discussions](https://github.com/cake-build/cake/discussions/categories/extension-q-a) if you have any issues using this addin.