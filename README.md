# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
1.	Follow the build steps
2.	Configure the name resolution and certificates
3.	After finishing step 1 & 2 you can use the app [here](https://eventhub.ch) 

# Build
Run `docker-compose -p eventhub-api up -d --scale eventhub.api=2 --build` in the root directory of this repository to build all the necessary images and run the containers. 

# Configure (Linux)
1. Edit your hosts file under `/etc/hosts` and add following lines
```
127.0.0.1 api.eventhub.ch 
127.0.0.1 eventhub.ch
```
2. Install the self signed certificates which are located under `/nginx/api.eventhub.ch.crt` and `/nginx/eventhub.ch.crt` by copying them to `/usr/local/share/ca-certificates` and afterwards running `sudo update-ca-certficates`
