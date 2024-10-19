# Client 
ng build
# API
dotnet build
docker build -t dominichdocker/kibokopropertymamanger .
docker push dominichdocker/kibokopropertymamanger:latest

# SERVER

docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' kibokopropertymamanger

cd /etc/nginx/sites-available
nano docker-proxy

sudo systemctl restart nginx

create docker-compose.yml file
docker container prune
docker-compose pull
docker-compose up --force-recreate --build -d
