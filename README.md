# A demonstration of relevent 

## Introduction 
A Microservice example based on Many articles from many places.  "Lots of different places."  

Througout the document obtuse references are made to a movie. Read to the end to see if you catch it.

## Prerequisites

Java 11 - Open JDK 11 is acceptable
maven
dotnet core 2.2.402
postman or a similar REST debug tool
a running mongodb instance to manage the JWT tokens and users
docker desktop (if you want to play with the docker containers)

Lots of patience

## Building the suite of microservices
At the root

```
mvn clean package
.\Start-Development.ps1
```

You will need JAVA_HOME correctly defined.

This will start up the 5 microservices.  They can take a while to come up.

You can also start them individually, but you do need to start them with the development profile active otherwise they will assume they are running in docker and failure will result.

## Getting ready to run - setting up mongo

This isn't going to be a mongo tutorial.  

Connect to your running mongo instance and inport the sample user into a database called "test" with a collection of "user".  The user.json file is in the gateway directory.

```
mongoimport --db test --collection user --file user.json
```
You are now done.  You can import more users if you want, but in reality there can be only one.


## Once it is running

### Key URLS

The the discovery service 
-   [http://localhost:8761](http://localhost:8761)

This usually comes up quite quickly and the UI will show what services have come up and registered with Eureka.  When all 4 services are registered (Eureka does not register with itself) everything is up and running.

### Gateway configuration for those interested

You can override the service names in the zuul config file.  You probably don't want to go this, but it can be useful when you have name conflicts.

To only allow access to the counter service using a cs prefix. 

```
ignored-services: '*'
  routes:
    counter:
      path: /cs/**
      serviceId: counterservice
      steeltoe:
        path: /st/**
        serviceId: steeltoeboot
```

Which overrides the defaults for the service name.  The gateway prefix comes from the line:

```
zuul:
  prefix: /gateway  
```

you can find all of this in the gateway.yml file.

### Accessing services via the gateway

The service is protected by a security mechanism based on JWT.  Currently this is only used between the gateway and the client. Additional work needs to happen to carry that down to the microservice itself.

To get past the gateway you need to get a JWT token from the gateway.  Currently there is no UI for this, so you talk funny using postman instead.

Create a new POST message to http://localhost:8080/api/signin with the body type set to RAW and a text type of JSON

Include the following snippet in the body.

```
{
    "username": "user@domain.com",
    "password": "password"
}
```

Now post it.  You should get an access token.  You'll need that in a minute.

Create a second GET request in postman for http://localhost:8080/gateway/counter/count.

Add a header with the name Authorization and a value of the access token you just generated.  Send the message and you should get back a text string in reply.  Yay - it worked.  If it didn't work you did something wrong, or there is an error in this document.

The experimenters will notice that the system is so secure that is does not actually validate the password.  It is left to the reader to figure out why that is and where that is injected.  It is actually instructive into how much of spring configuration works by dependency injection.

### The gateway and discovery services

The discovery service is pretty much out-of-the-box, so I'm not going to add anything of note.  The detail is easily discoverable with a web search.

The gateway has it;s own readme on the JWT security piece.  If it is isn't there then I haven't got round to writing it yet - sorry. Check back in a week.

### The silly simple Java counter service

This reads its configuration for the message from the configuration service.  The code is so simple not much more explanination is needed.

The meat that makes it work with the config server is:

```
@Configuration
@EnableAutoConfiguration
@RefreshScope
public class CounterController {
```

It is all done via dependency injection.

The config values are picked up here:

```
@Value(value = "${counter.prefixMessage}")
    private String prefixMessage;
```

### .NET Core/Standard service

I wanted to add a .NET core service using steeltoe.  The result is steeltoeboot, which is an amalgam of sample code from "lots of different places".  It follows broadly the same pattern as the java code, but with a couple of gotchas.  

It seems steeltoe does not do as good a job of automagically picking up the hostname and port.  This can lead to some funky route filtering issues in zuul.  The instance settings need to be called out in more detail in the appsettings.json file. There may be other, better, ways to do this, but at least this works for a quick sample app.

The service returns the 2 values in the configuration file on the 
[http://localhost:8080/gateway/steeltoeboot/api/values](http://localhost:8080/gateway/steeltoeboot/api/values) endpoint.

To build the dotnet core as an executable [Win or linux as appropriate]

```
dotnet publish .\SteelToeBoot.csproj --configuration Debug --runtime win10-x64 --self-contained --output out
dotnet publish .\SteelToeBoot.csproj --configuration Debug --runtime linux-x64 --self-contained --output outlinux
```

## Running in Docker considerations

There are a few tweaks to setting and config when running in docker that are related to effective use of the docker network.  Note that you can avoid using the docker network and just connect to the local network, but that isn't how
containers typically get deployed.

These generally all related to service discovery and communication. 

This is handled by having development release profiles with matching changes to appliction setting to configure the service for the environment it is running in. 

There is a standard pattern for this:

```
.../resources/application.yml
.../resources/application-development.yml
```

The same applies to the bootstrap.yml file.  Bootstrap.yml is the highest priority settings file and is used to configure the system before the application properties get read.

If you have docker desktop installed (or you are running this on linux which will probably work as well)  there is a docker-compose file to bring the system up.  The command:

``` 
docker-compose up 
```

will build the containers and bring them online.  It is lft to the reader to explore the other areas of docker.

Note that there is an issue with steeltoeboot where it cannot find the configuration service when run in a container.  I have no idea why,  but if you do fix it please fork the repository and send me a pull request.

## Oddities
Note that with spring boot 2.0 and above the management endpoints have changed and also they need to be exposed by configuration.  See the application.yml file in the counter service as an example, although that is really a broad brush exposure of all of the interfaces.

Also the refresh endpoint has moved to actuator, so the curl call for the refresh call is now:

curl -X POST localhost:8000/actuator/refresh -d {} -H "Content-Type: application/json"

Note that this goes VFR direct to the service and bypasses the gateway.

## The Movie Reference

### Highlander

Connor MacLeod: There can be only one!

Garfield: You talk funny Nash. Where you from?  
Nash: Lots of different places.

Ok, I confess that was weak. If you want to do better, fork the repo, do some clever stuff and send me a pull request.

