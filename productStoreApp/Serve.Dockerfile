FROM node:alpine

WORKDIR /usr/src/app
COPY . /usr/src/app

# Install dependencies
RUN npm install -g @angular/cli
RUN npm install

# Install lite server
RUN npm install lite-server

EXPOSE 4200

CMD ["ng", "serve", "--host", "0.0.0.0"]
