FROM node:14

WORKDIR /app

COPY package*.json ./

COPY public/ ./public/

COPY src/ ./src/

RUN npm install

COPY . .

EXPOSE 5173

ENV REACT_APP_BACKEND_URL=https://localhost:5001/api

CMD ["npm", "run", "dev"]
