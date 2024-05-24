FROM node:alpine

WORKDIR /usr/src/app
COPY . /usr/src/app

# Install dependencies
RUN npm install -g @angular/cli
RUN npm install

# Install lite server
RUN npm install lite-server

# Build app
RUN npm run build-prod

EXPOSE 3000

WORKDIR /usr/src/app/dist/products-app/browser
CMD ["npx", "lite-server"]
