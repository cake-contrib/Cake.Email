---
Title: New Release - 0.6.0
Published: 13/03/2018
Category: Release
Author: jericho
---

## Breaking changes

- [__#33__](https://github.com/cake-contrib/Cake.Email/issues/33) Upgrade to Cake 0.26.0 and target netstandard2.0

## Note

First, include a reference to this addin in your script like this:
```
#addin nuget:?package=Cake.Email&version=0.6.0&loaddependencies=true
```

Second, we highly recommend that you add the following 'using' statement in your script. Technically, this is not necesary, but it simplifies dealing with attachements: 
```
using Cake.Email.Common;
```

Also, this addin is designed to take advantage of some of the new features released in CakeBuild version `0.26.0` therefore your `tools\package.config` should look like this:
```
<packages>
    <package id="Cake" version="0.26.0" />
</packages>
```

Please do not hesitate to reach out in the [Gitter Channel](https://gitter.im/cake-contrib/Lobby) if you have any issues using this addin.
