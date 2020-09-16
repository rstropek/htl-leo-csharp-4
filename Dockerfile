FROM node:alpine AS builder

WORKDIR /app
COPY ./ ./
RUN npm run build

FROM node:alpine
EXPOSE 80
WORKDIR /app
COPY package.json ./
COPY --from=builder /app/slides/9999_full.md ./
RUN npm install --only=prod

CMD [ "npm", "start", "--", "9999_full.md", "--port", "80" ]
