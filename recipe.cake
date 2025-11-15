#load nuget:?package=Cake.Recipe&version=4.0.0

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Email",
                            solutionFilePath: "./Source/Cake.Email.slnx",
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
