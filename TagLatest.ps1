
param(
    [Parameter(Mandatory=$false)] 
    [switch] $Push,

    [Parameter(Mandatory=$false)] 
    [switch] $PushOnly
)

#
# Tags the images as latest
#"
$images="counterservice", "gatewayservice", "configurationservice", "discoveryservice"

foreach($image in $images)
{
    $existing = $env:REGISTRY + $image + ":" + $env:TAG
    $target = $env:REGISTRY + $image + ":" + "latest"

    if(!$PushOnly)
    {
        Write-Host "Tagging $existing to $target"

        $args = 'tag', $existing, $target
        & 'docker' $args
    }    
    
    if($push -Or $PushOnly)
    {
        Write-Host "Pushing $existing"
        $args = 'push', $existing
        & 'docker' $args
        
        Write-Host "Pushing $target"
        $args = 'push', $target
        & 'docker' $args
    }
}