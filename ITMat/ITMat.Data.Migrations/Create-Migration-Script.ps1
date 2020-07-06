param (
    [Parameter(Mandatory = $true)] [string] $ScriptName
)

$scriptsDir = "Scripts";
$finalName = "$(Get-Date -u +%Y%m%d%H%M%S)_$ScriptName.sql";
New-Item "$scriptsDir/$finalName";