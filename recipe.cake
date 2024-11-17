#load nuget:https://pkgs.dev.azure.com/cake-contrib/Home/_packaging/addins/nuget/v3/index.json?package=Cake.Recipe&version=4.0.0-alpha0126

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Email",
                            masterBranchName: "main",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Email",
                            shouldRunDotNetCorePack: true,
                            shouldRunInspectCode: false,
                            shouldRunCodecov: false,
                            appVeyorAccountName: "cakecontrib",
                            shouldCalculateVersion: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context);

Build.RunDotNetCore();
