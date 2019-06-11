FROM openjdk:8-jdk-alpine

RUN addgroup -S appgroup
RUN adduser -S appuser -G appgroup
USER appuser

VOLUME /tmp
ADD target/discoveryservice-0.0.2-SNAPSHOT.jar app.jar
ENV JAVA_OPTS="-Xms16m -Xmx64m"
ENTRYPOINT [ "sh", "-c", "java $JAVA_OPTS -Djava.security.egd=file:/dev/./urandom -jar /app.jar"]