#!/bin/bash

echo "Starting migration"

migration_image_name=$1
secret=$2
assembly="/app/Din.Infrastructure.Migrations.dll"

docker run --rm $migration_image_name dotnet fm migrate -p mysql -c $secret -a $assembly

echo "Finished migration"