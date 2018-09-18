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
