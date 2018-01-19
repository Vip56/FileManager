function Exec  		
{		
    [CmdletBinding()]		
    param(		
        [Parameter(Position=0,Mandatory=1)][scriptblock]$cmd,		
        [Parameter(Position=1,Mandatory=0)][string]$errorMessage = ($msgs.error_bad_command -f $cmd)		
    )		
    & $cmd		
    if ($lastexitcode -ne 0) {		
        throw ("Exec: " + $errorMessage)		
    }		
}

if(Test-Path .\artifacts) { Remove-Item .\artifacts -Force -Recurse }

exec { & dotnet restore }

$revision = @{ $true = $env:APPVEYOR_BUILD_NUMBER; $false = 1 }[$env:APPVEYOR_BUILD_NUMBER -ne $NULL];
$revision = [convert]::ToInt32($revision, 10)

#exec { & dotnet test .\test\Sino.FileManager.Tests -c Release }

exec { & dotnet pack .\src\Sino.FileManager -c Release -o .\artifacts --version-suffix=$revision }
