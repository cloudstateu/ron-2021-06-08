<img src="../../../img/logo.png" alt="Chmurowisko logo" width="200" align="right">
<br><br>
<br><br>
<br><br>

# Runninc basic Docker containers

## LAB Overview

#### In this lab you will be introduced to basics of Docker CLI; you will also run basic Docker containers

## Task 1: Run sample container from image downloaded from DockerHub
1. Open terminal
2. Check that Docker is installed properly: `docker --version`
3. Run hello-world image: `docker run hello-world`. This command will try to find hello-world image locally. If it will not be present, docker will download image from DockerHub repository.
4. Run SuperMario game: `docker run -d -p 8600:8080 pengbai/docker-supermario`. This command will run SuperMario game in container: 
   - `-p`: expose container's port 8080 on your local port 8600
   - `-d`: run in detached mode (in background)
5. Open web browser and type: `http://localhost:8600`
6. Play the game!

## Task 2: Run sample web page on ngnix, collect logs.
1. Type: `docker run -p 8080:80 nginxdemos/hello`
2. Open web browser and type `localhost:8080/somepath`
3. Go back to terminal, you can see all the nginx logs. In fact you see everything that goes to standard output on container.
4. Stop the container: `Ctrl + C`
5. Run container again but in detached mode: `docker run -p 8080:80 -d nginxdemos/hello`. This command will output id of a container. Copy it.
6. Go to web browser and make some requests.
7. See logs from the container: `docker logs -f CONTAINER_ID` (paste container id from point 5)
   - `-f`: follow logs from container
8. Make some more requests and see how logs are collected

## Task 3: Execute command on running container
1. `docker exec` lets you execut command on running container
2. Type: `docker exec CONTAINER_ID whoami`. This will execute `whoami` command. (Get container id from previous task)
3. You can also run shell: `docker exec CONTAINER_ID sh`. However it won't work because we also need to attach to containers terminal.
4. Run `docker exec -it CONTAINER_ID sh`
5. Now you entered on a container and you can execute any shell command on it (eg: `ls`, `whoami`, `pwd`)

## Task 4: Run ubuntu in container and install apache server
1. Type `docker run -it -p 8081:80 ubuntu bash`. This commands will:
   - run ubuntu image
   - expose container's port 80 to local 8081
   - execute `bash` command on container
   - open psedut TTY (terminal)
2. We exposed container's port 80, but there is no server running. You can check `http://localhost:8081` that no page is displayed
3. Go to termimal and inside container execute:
   - `apt update`
   - `apt install -y apache2`
   - `service apache2 start` 
4. Check again web browser: `http://localhost:8081`
5. Type `exit`. This command will exit and stop the container. The apache2 web page will be no longer available. If you want to get it back you will need to run ubuntu image and install apache2 manually again. In next lab about **Dockerfiles** you will learn how to automate such tasks.
   
## Task 5: Stop and start container
1. Type `docker ps`. This will show you all running containers.
2. Stop a container by running `docker stop CONTAINER_ID`
3. Type `docker ps -a`. This will show you all containers: running and stopped.
4. You can start a container by running `docker start CONTAINER_ID`
5. Type `docker ps` again and verify that your container is running again

## Task 6: Clean-up resources
1. Stop all running containers.
2. You can remove stopped containers by running `docker rm CONTAINER_ID`
3. Remove all stopped containers. You can also remove multiple containers in one line: `docker rm CONTAINER_1_ID`, `CONTAINER_2_ID` etc.
4. Type `docker images`. It will show you all images stored on your local machine. Each image consumes some disk space (SIZE column)
5. You can remove unnecessary images by running: `docker rmi IMAGE_ID`. Note that containers that run from this images must be stopped and removed already.
