#!/bin/bash

if [ $1 = "Production" ]; then
    echo "Starting production deployment"
    source deploy/production.vars
else
    echo "Starting nightly deployment"
    source deploy/nightly.vars
fi

id=$2
secret=$3
image_name=$4

docker stop $name || true && docker rm $name || true
docker run -d --name $name -p $port:80 -e VAULT_URL=$vault_url -e VAULT_ID=$id -e VAULT_SECRET=$secret -e ASPNETCORE_ENVIRONMENT=$environment $image_name

echo "Finished deployment"