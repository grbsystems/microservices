FROM openjdk:11

VOLUME /tmp
ADD target/counterservice-0.0.2-SNAPSHOT.jar app.jar
ENV JAVA_OPTS="-Xms16m -Xmx64m"
ENTRYPOINT [ "sh", "-c", "java $JAVA_OPTS -Djava.security.egd=file:/dev/./urandom -jar /app.jar"]