# Introduction 
Microservice examples based on some articles from Medium

[Medium](https://medium.com/@marcus.eisele/implementing-a-microservice-architecture-with-spring-boot-intro-cdb6ad16806c)



# Oddities
Note that with spring boot 2.0 and above the management endpoints have changed and also they need 
to be exposed by configuration.  See the application.yml file in the counter service as an example,
although that is really a broad brush exposure of the interfaces.

Also the refresh endpoint has moved to actuator, so the curl call for the refresh call is now:

curl -X POST localhost:8000/actuator/refresh -d {} -H "Content-Type: application/json"


# Build and Test
At the root

mvn clean package

docker-compose up

# Key URLS
The counter service 
-   [http://localhost:8000/count](http://localhost:8000/count)

The config service
-   [http://localhost:9000/counterservice/default](http://localhost:9000/counterservice/default)

# Useful Notes during Development

The medium article is for a 1.x.x version of Spring Boot.  With the 2.x.x versions dependencies have changed a bit, as well as some
of the techniques used.  This has been updated accordingly.  I have found the easiest way to get the 
right dependencies in is to go to the spring boot initializer and regenerate the pom.xml file for whatever
spring boot version you want.  Mix and match often causes dependency mismatch errors with ClassNotFound on some
abstract classes.  Score +1 for much better .NET assembly versioning there!

Also, service discovery does not allow you to automagically find the config service.  That needs to reside in 
a bootstrap.yml file.  See counterservice for an example.

Also see that file for an example of how the discovery service endpoint is set up.

# Starting Services

The set of 3 services can be started from docker-compose and the counterservice will wait until the 
configuration service is available and running.

# Accessing services via the gateway

The url for the counter service via the gateway is 
[http://localhost:8080/gateway/counterservice/count](http://localhost:8080/gateway/counterservice/count)

You can override the service names in the zuul config file.  You probably don't want to go this, but it can be useful 
when you have name conflicts.

To only allow access to the counter service using a cs prefix. 

    zuul.ignored-services=*
    zuul.routes.counter.path=/cs/**
    zuul.routes.counter.serviceId=counterservice

Which overrides the defaults for the service name.  The gateway prefix comes from the line:

    zuul.prefix=/gateway
    
# .NET Core/Standard service

In the original article there is no part seven.  I wanted to add a .NET core service using steeltoe.  The result is 
steeltoeboot, which is an amalgam of some sample code.  It follows broadly the same pattern as the 
java code, but with a couple of gotchas.  

It seems steeltoe does not do as good a job of automagically picking up the hostname and port.  This can lead to some funky route filtering
issues in zuul.  The instance settings need to be called out in more detail in the appsettings.json file. There may be 
other, better, ways to do this, but at least this works for a quick sample app.

The service returns the 2 values in the configuration file on the 
[http://localhost:8080/gateway/steeltoeboot/api/values](http://localhost:8080/gateway/steeltoeboot/api/values) endpoint,
or on the [http://localhost:8080/gateway/steeltoeboot/api/values](http://localhost:8080/gateway/steeltoeboot/api/values) 
endpoint if you are bypassing the gateway and just running locally.   

To build the dotnet core as an executable [Win or linux as appropriate]

```
dotnet publish .\SteelToeBoot.csproj --configuration Debug --runtime win10-x64 --self-contained --output out
dotnet publish .\SteelToeBoot.csproj --configuration Debug --runtime linux-x64 --self-contained --output outlinux
```


# Running in Docker considerations

There are a few tweaks to setting and config when running in docker that are related to effective use of the docker 
network.  Not that you can avoid using the docker network and just connect to the local network, but that isn't how
containers typically get deployed.

These generally all related to service discovery and communication. 





