# Cake.Email

[![License](http://img.shields.io/:license-mit-blue.svg)](http://cake-contrib.mit-license.org)

Cake.Email is an Addin for [Cake](http://cakebuild.net/) which allows sending of email from your build scripts.

## Usage

First, include a reference to this addin in your script like this:

```csharp
#addin nuget:?package=Cake.Email&version=0.8.0&loaddependencies=true
```

Second, we highly recommend that you add the following 'using' statement in your script. Technically, this is not necessary, but it simplifies dealing with attachements:

```csharp
using Cake.Email.Common;
```

Also, this addin is designed to take advantage of some of the new features released in CakeBuild version `0.33.0` therefore your `tools\package.config` should look like this:

```xml
<packages>
    <package id="Cake" version="0.33.0" />
</packages>
```

## Information

| |Stable|Pre-release|
|:--:|:--:|:--:|
|GitHub Release|-|[![GitHub release](https://img.shields.io/github/release/cake-contrib/Cake.Email.svg)](https://github.com/cake-contrib/Cake.Email/releases/latest)|
|Package|[![NuGet](https://img.shields.io/nuget/v/Cake.Email.svg)](https://www.nuget.org/packages/Cake.Email)|[![MyGet](https://img.shields.io/myget/cake-contrib/vpre/Cake.Email.svg)](http://myget.org/feed/cake-contrib/package/nuget/Cake.Email)|

## Build Status

|Develop|Master|
|:--:|:--:|
|[![Build status](https://ci.appveyor.com/api/projects/status/y8b1429u4dpbxlf2/branch/develop?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-email/branch/develop)|[![Build status](https://ci.appveyor.com/api/projects/status/y8b1429u4dpbxlf2/branch/develop?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-email/branch/master)|

## Code Coverage

[![Coverage Status](https://coveralls.io/repos/github/cake-contrib/Cake.Email/badge.svg)](https://coveralls.io/github/cake-contrib/Cake.Email)

## Quick Links

- [Documentation](https://cake-contrib.github.io/Cake.Email/)

## Chat Room

Please do not hesitate to reach out in the [GitHub discussions](https://github.com/cake-build/cake/discussions/categories/extension-q-a) if you have any issues using this addin.