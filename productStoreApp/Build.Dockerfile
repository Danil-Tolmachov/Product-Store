# Install
FROM node:20.9.0-alpine as base

WORKDIR /usr/src/app
COPY . /usr/src/app

# Install dependencies
RUN npm install -g @angular/cli
RUN npm install

# Install lite server
RUN npm install lite-server

# Publish app
FROM base as buid
RUN npm run build-prod

# Run app
FROM buid as final
EXPOSE 3001

WORKDIR /usr/src/app/dist/products-app/browser
CMD ["npx", "lite-server"]
