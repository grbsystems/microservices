# Introduction 
Microservice examples based on some articles from Medium

[Medium](https://medium.com/@marcus.eisele/implementing-a-microservice-architecture-with-spring-boot-intro-cdb6ad16806c)



# Oddities
Note that with spring boot 2.0 and above the management endpoints have changed and also they need 
to be exposed by configuration.  See the application.yml file in the counter service as an example,
although that is really a broad brush exposure of the interfaces.

Also the refresh endpoint has moved to actuator, so the curl call for the refresh call is now:

curl -X POST localhost:8080/actuator/refresh -d {} -H "Content-Type: application/json"


# Build and Test
At the root

mvn clean package
docker-compose up

# Key URLS
The counter service 
-   [http://localhost:8080/count](http://localhost:8080/count)

The config service
-   [http://localhost:8888/counterservice/default](http://localhost:8888/counterservice/default)

# Useful Notes during Development

The medium article is for a 1.x.x version of Spring Boot.  With the 2.x.x versions dependencies have changed a bit, as well as some
of the techniques used.  This has been updated accordingly.  I have found the easiest way to get the 
right dependencies in is to go to the spring boot initializer and regenerate the pom.xml file for whatever
spring boot version you want.  Mix and match often causes dependency miskatch errors with ClassNotFound on some
abstract classes.  Score +1 for much better .NET assembly versioning there!

Also, service discovery does not allow you to automagically find the config service.  That needs to reside in 
a bootstrap.yml file.  See counterservoice for an example.

Also see that file for an example of how the discovery service endpoint is set up.



