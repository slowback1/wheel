cd $(git rev-parse --show-toplevel)/frontend || exit 1

cp ./static/config/config.example.json ./static/config/config.json

npm install