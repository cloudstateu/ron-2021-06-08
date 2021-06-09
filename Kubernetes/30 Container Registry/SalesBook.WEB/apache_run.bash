docker build . -t apache-docker

#https://github.com/docker-library/php/blob/8203d502a18ecfe79ac011f85843754fb524b899/7.3/stretch/apache/Dockerfile

docker run --name apache2container -d -p 8080:80 -t apache-docker:latest 