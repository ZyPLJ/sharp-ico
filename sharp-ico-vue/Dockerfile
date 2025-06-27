# 生产阶段
FROM node:20-alpine as build-stage
WORKDIR /app
COPY . .
RUN npm install && npm run build  # 自动打包

# 复制构建结果到 Nginx
FROM nginx:stable-alpine
COPY --from=build-stage /app/dist /usr/share/nginx/html
COPY default.conf /etc/nginx/conf.d/default.conf

# 暴露端口
EXPOSE 5173

# 启动 Nginx
CMD ["nginx", "-g", "daemon off;"]
