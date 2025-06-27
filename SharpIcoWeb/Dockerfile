FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# 复制解决方案文件
COPY *.sln .
# 复制项目文件
COPY SharpIcoWeb/*.csproj ./SharpIcoWeb/
# 复制项目依赖项
COPY SharpIco/*.csproj ./SharpIco/
# 还原依赖项
RUN dotnet restore
# 编译项目
COPY . .
WORKDIR /src/SharpIcoWeb
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
ENTRYPOINT ["dotnet", "SharpIcoWeb.dll"]