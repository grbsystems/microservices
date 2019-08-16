FROM ubuntu:cosmic
RUN apt-get install libssl1.0.0:amd64

COPY ./outlinux ./SteelToeBoot

EXPOSE 80/tcp

ENTRYPOINT ["/bin/bash"]