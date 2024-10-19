# Preparing the project
## Download Nodejs
- go to https://nodejs.org/en/download/prebuilt-installer click on 'Download Node.js'
- install it

## Download and install docker
- go to https://www.docker.com/products/docker-desktop/
- click on download
- install it

## Download and install dotnet sdk version 7
- go to https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.302-windows-x64-installer
- download and install
- go to https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-7.0.20-windows-x64-installer?cid=getdotnetcore
- download and install 

## download the repository
- go to https://github.com/kitwa/itcareeradvisor click on button 'Code' then click on 'Download ZIP' as shown on picture
- unzip the folder
- open the folder in visual studio code

## Run the backend
 - cd API
 - dotnet dev-certs https --trust                         (#### this command is only once)
 - docker run --detach --name troisiemeadam --env MARIADB_ROOT_PASSWORD=TroisiemeAdam1502@  -p 3306:3306 mariadb:latest 	(#### this command is only once)
 - dotnet tool install --global dotnet-ef
 - dotnet ef migrations add InitialCreate -o Data/Migrations
 - dotnet ef database drop (run this if existing database)
 - dotnet ef database update
 - dotnet build
 - dotnet run


## Run the Frontend
- npm install
- ng build 
- ng serve


Visit http://localhost:4200/ to access the application


# Publish

## Docker
- create ripo on docker called troisiemeadam.kibokohouse.com
### Client 
ng build
### API
- dotnet build
- docker build -t dominichdocker/troisiemeadam.kibokohouse.com .
- docker login
- docker push dominichdocker/troisiemeadam.kibokohouse.com:latest

### SERVER

docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' kibokohouse
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' za.kibokohouse
docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' troisiemeadam.kibokohouse

cd /etc/nginx/sites-available
nano docker-proxy

sudo systemctl restart nginx

create docker-compose.yml file
docker container prune
docker-compose pull
docker-compose up --force-recreate --build -d


db first

sudo apt update
sudo apt install default-mysql-client

mysqldump -u root --port=3306 -p troisiemeadam > db.sql

scp root@kibokohouse.com:/root/db.sql C:\Users\dom\Desktop