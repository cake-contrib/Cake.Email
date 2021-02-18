$ErrorActionPreference = 'Stop'

function Run([string[]]$arguments) {
    $cmd = @("& dotnet")
    $cmd += $arguments
    $cmdLine = $cmd -join " "
    Write-Verbose "> $cmdLine"
    Invoke-Expression $cmdLine

    if ($LASTEXITCODE -ne 0) {
        Write-Host "Non-Zero exit code ($($LASTEXITCODE)), exiting..."
        exit $LASTEXITCODE
	}
}

Run tool, restore

Run cake, recipe.cake, --bootstrap

$arguments = @("cake"; "recipe.cake")
$arguments += @($args)

Run $arguments