# Set-PSDebug -strict
$Name = "A4B91446.18967FDE5EFB2"
$Executable = "WPF_SilentUpdate\WPF_SilentUpdate.exe"

Get-AppxPackage -Name $Name
$Version = Get-AppxPackage -Name $Name | Select-Object -ExpandProperty Version
$InstallLocation = Get-AppxPackage -Name $Name | Select-Object -ExpandProperty InstallLocation
$FilePath = "${InstallLocation}\${Executable}"

Write-Host "---------------"
Write-Host "Name = ${Name}, Version = ${Version}"
Write-Host "FilePath = ${FilePath}"
Write-Host "---------------"

$proc = Start-Process -FilePath $FilePath -Wait -PassThru
Write-Host "ExitCode = "$proc.ExitCode

pause
