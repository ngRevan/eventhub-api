# EventHub API 
This project contains the ASP .NET backend as well as the docker-compose components needed to setup all services.

## Getting Started
1.  On Windows be sure to clone this repository with the following parameter due to different line endings: `git clone <<url>> --config core.autocrlf=input`
2.  Follow the build steps
3.	Configure the name resolution and certificates
4.	After finishing step 1 & 2 you can navigate to [https://eventhub.ch](https://eventhub.ch)  or  [https://api.eventhub.ch](https://api.eventhub.ch)  to see the live version. 

## Build
Run `docker-compose -p eventhub-api up -d --scale eventhub.api=2 --build` in the root directory of this repository to build all the necessary images and to run the containers. 

## Configure (Linux)
1. Edit your hosts file under `/etc/hosts` and add following lines
```
127.0.0.1 api.eventhub.ch 
127.0.0.1 eventhub.ch
```
2. Install the self signed certificates which are located under `/nginx/api.eventhub.ch.crt` and `/nginx/eventhub.ch.crt` by copying them to `/usr/local/share/ca-certificates` and afterwards running `sudo update-ca-certficates`
