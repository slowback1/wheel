﻿# Usage
# Env Variables:
# REGISTRY: the docker registry to push the image to. ex: hub.docker:5000
# BUILD_NUMBER: the unique to this build number.  ex: 1
# PROJECT_NAME: The name of the project.  Defaults to "api". ex: my-cool-project-name

cd $(git rev-parse --show-toplevel)/api || exit 1

BUILD_NUMBER=latest
REGISTRY="hub.docker.com:5000"
PROJECT_NAME="slowback1/wheel-of-slowback-discord-bot"

if [ -z ${REGISTRY} ]; then
  echo "REGISTRY is unset, please set it before running this script";
  exit 1
fi

if [ -z ${BUILD_NUMBER} ]; then
  echo "BUILD_NUMBER is unset, please set it before running this script";
  exit 1
fi

if [ -z ${PROJECT_NAME} ]; then
  echo "PROJECT_NAME is unset, setting to 'frontend'";
  PROJECT_NAME="api"
fi

TAG="${PROJECT_NAME}:${BUILD_NUMBER}"

docker build -t=${TAG} -f DiscordBot/Dockerfile --target=final .
docker image tag ${TAG}

docker image push ${TAG}