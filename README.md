# PROJECT OVERVIEW
#### This project is about building an online agency website for selling or renting properties

#### I’m Dominique Kiboko. I’m a Full Stack Developer / .NET developer. I find C# a great language to use and it’s also backed by a great ecosystem, I love solving  problems.

# RUNNING

#### POSTGRES

### Docker - Postgres
#### Install docker
https://docs.docker.com/docker-for-windows/install/

#### Get Postgres Image
https://hub.docker.com/_/postgres

#### Run locally
docker run -d postgres -e POSTGRES_USER=kibokouser -e POSTGRES_PASSWORD=KibokoHouse1502@ --volume kibokopropertymamanger-volume:/var/lib/postgresql/data -p 5432:5432  --detach postgres --name kibokohousepostgres

docker run --name kibokopropertymamanger-postgres -e POSTGRES_PASSWORD=KibokoHouse1502@ -d postgres --volume kibokopropertymamanger-volume:/var/lib/postgresql/data

docker run --name some-postgres -e POSTGRES_PASSWORD=KibokoHouse1502@ -d postgres


#### postgres tools
https://www.pgadmin.org/

### In API folder Run this conmmand to build the image

  ##### Build client
  ng build
 
  ##### Build backend
  dotnet build
  docker build -t dominichdocker/kibokopropertymamanger .

 #### run docker

  docker run --rm -it -p 8080:80 dominichdocker/kibokopropertymamanger:latest

 #### push to docker so that we can access from the cloud flateform we are going to be using

 docker push dominichdocker/kibokopropertymamanger:latest

 #### pull on server

docker-compose pull

docker-compose up --force-recreate --build -d

docker image prune -af

  docker-compose up -d
docker-compose down --volumes
  docker pull dominichdocker/kibokopropertymamanger:latest


 ### INSTALL DOCKER COMPOSE ON VPS Linux

#### Download Docker Compose:
 sudo curl -L "https://github.com/docker/compose/releases/latest/download/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose

#### Apply executable permissions:
 sudo chmod +x /usr/local/bin/docker-compose

#### Create a symbolic link:
sudo ln -s /usr/local/bin/docker-compose /usr/bin/docker-compose

Verify the installation:
docker-compose --version

## DELETING Docker images 
### Here is short and quick solution I used
#### Docker provides a single command that will clean up any resources — images, containers, volumes, and networks — that are dangling (not associated with a container):
docker system prune

#### To additionally remove any stopped containers and all unused images (not just dangling images), add the -a flag to the command:
docker system prune -a


# ADD SSL CERTIFICATE lET'S ENCRUPT
sudo systemctl stop nginx
sudo apt update
sudo apt install certbot
sudo apt install python3-certbot-nginx
sudo certbot certonly --nginx -d kibokopropertymamanger.co.za -d www.kibokopropertymamanger.co.za -d test.kibokopropertymamanger.co.za


# DEPLOYMENT TO PRODUCTION

## Local preparation
cd client
ng build
cd API
dotnet build
docker build -t dominichdocker/kibokopropertymamanger .
docker push dominichdocker/kibokopropertymamanger:latest
## Production deployment
docker stop (pid API)
docker-compose pull

##### if needed you can run docker image prune -af



# HOW TO DROP A POSTGRES DATABASE USING COMMAND
psql -h localhost -p 5432 -U your_username -d your_database
### list db
\l
### switch to db
\c databse_name;
### close sessions
#### get pid
SELECT * FROM pg_stat_activity WHERE datname = 'your_database'; 
#### kill pid
SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = 'your_database';
### drop db
DROP DATABASE database_name;




## CHANGE DB TO MySQL
#### Get MySQL Image
https://hub.docker.com/_/mariadb

#### Install Nuget 
Pomelo.EntityFrameworkCore.MySql 

#### change use of connection string in Program.cs to
var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseMySql(connString, serverVersion);
});

#### Run locally

docker run --detach --name local-mariadb --env MARIADB_ROOT_PASSWORD=Kibokotransfer1502@  -p 3306:3306 mariadb:latest 



# Update Angular from 14 to 16
### get a report for angular 15
ng update
### update
ng update @angular/cdk@15 @angular/cli@15 @angular/core@15 ngx-toastr --force
### get a report for angular 16
ng update
### update
ng update @angular/cdk@16 @angular/cli@16 @angular/core@16 --force

### update npm package to lastest
npm outdated
npm i -g npm-check-updates && ncu -u && npm i -f
### update ngx-bootstrap to 11
npm install ngx-bootstrap@11 -g -f

# change ngx gallery
use https://ngx-gallery.netlify.app/


D:\repos\kibokohouseapp\platforms\android\app\build\outputs\apk\debug