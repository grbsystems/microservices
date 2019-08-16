
Set-Jdk jdk11

$arguments= "-Dspring.profiles.active=development", "-jar", "discoveryservice/target/discoveryservice-0.0.2-SNAPSHOT.jar"
$global:discovery = Start-Process -FilePath "java.exe" -ArgumentList $arguments -PassThru
$global:discovery

$arguments= "-Dspring.profiles.active=development", "-jar", "configurationservice/target/configurationservice-0.0.2-SNAPSHOT.jar"
$global:configuration = Start-Process -FilePath "java.exe" -ArgumentList $arguments -PassThru
$global:configuration

$arguments= "-Dspring.profiles.active=development", "-jar", "gatewayservice/target/gatewayservice-0.0.1-SNAPSHOT.jar"
$global:gateway = Start-Process -FilePath "java.exe" -ArgumentList $arguments -PassThru
$global:gateway

$arguments= "-Dspring.profiles.active=development", "-jar", "counterservice/target/counterservice-0.0.2-SNAPSHOT.jar"
$global:counter = Start-Process -FilePath "java.exe" -ArgumentList $arguments -PassThru
$global:counter

$env:ASPNETCORE_ENVIRONMENT="Development"
$here = Get-Location
Set-Location SteelToeBoot
dotnet publish .\SteelToeBoot.csproj --configuration Debug --runtime win10-x64 --self-contained --output out
$global:steelboot = Start-Process -FilePath "out/SteelToeBoot.exe" -PassThru
$global:steelboot
Set-Location $here









