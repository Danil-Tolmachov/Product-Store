# Install
FROM node:20.9.0-alpine AS base

WORKDIR /usr/src/app
COPY . /usr/src/app

# Install dependencies
RUN npm install -g @angular/cli
RUN npm install

# Publish app
FROM base AS build
RUN npm run build:prod

# Run app
FROM node:20.9.0-alpine AS final

WORKDIR /usr/app
COPY --from=build /usr/src/app/dist/products-app/ .
EXPOSE 3000 3001

# Install lite server
RUN npm install lite-server

WORKDIR /usr/app/browser
CMD ["npx", "lite-server"]
