<img src="../../../img/logo.png" alt="Chmurowisko logo" width="200" align="right">
<br><br>
<br><br>
<br><br>

# Working with Dockerfiles

## Lab Overeview

#### In this lab you will learn how to work with Dockerfiles and how to build your own docker images and containers

## Task 1: Create container that will run apache2 server on ubuntu

1. Create new file and name it "Dockerfile"
2. Copy content of files/Dockerfile and past it into your Dockerfile
3. Run: `docker build -t myapache2 .`. Wait for the build to finish.
4. Run `docker images` and verify that your image is on a list.
5. Execute `docker run -p 8081:80 -d myapache2`
6. Open web browser and verify that your container is running `http://localhost:8081`
7. Stop the container

## Task 2: Run apache2 server with your custom page added

1. Modify existing Dockerfile so it executes following command during build: `mkdir /var/www/html/mypage`
2. Add this command to Dockerfile (after creating "mypage" directory): `COPY index.html /var/www/html/mypage`
3. Build image: `docker build -t myapache2-mypage .`
4. Run container: `docker run -p 8081:80 -d myapache2-mypage`
5. Check your web page: `http://localhost:8081/mypage/`

## Task3: Run Node.js webapp in a container

Your task is to write a Dockerfile that will create a container with node.js webapp. The webapp should be accessible via `http://localhost:8080`. A few hints for you:
- all application files are located in "app" directory from this lab
- for base image use node from dockerhub: [https://hub.docker.com/node/](https://hub.docker.com/_/node/) (version 10)
- `package.json` is a file that defines all dependencies for Node.js app. In order to install them run `npm install` in a directory where `package.json` is located
- in order to start the app run `node server.js`
