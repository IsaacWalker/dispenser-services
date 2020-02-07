$service = "Scheduler"
$serviceName = $service + "Service"
$publishProject = ".\" + $serviceName + "\Web." + $serviceName  + ".csproj"
$servicePoolName = $serviceName + "Pool"
$siteName = $serviceName + "Site"
$hostName = "www." + $service.ToLower() + ".hostname.com"
$websitePath = "$(get-location)\Website\" + $serviceName

Remove-Website -Name $siteName 
Remove-WebAppPool $servicePoolName
Remove-Item $websitePath -ErrorAction Ignore

dotnet publish $publishProject -o Website\$serviceName
New-WebAppPool $servicePoolName
New-Website -Name $siteName -Port 80 -HostHeader $hostName -PhysicalPath $websitePath -ApplicationPool $servicePoolName

