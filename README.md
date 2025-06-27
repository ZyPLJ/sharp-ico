# 基于SharpIco开发图片转ICO工具网站

# SharpIcoWeb

[![License](https://camo.githubusercontent.com/bb4e5c0036a6a8cdbc59b38d44f09ad8f6dc722751dad34d3df5bf0ac61913c1/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f6c6963656e73652d4d49542d626c7565)](https://camo.githubusercontent.com/bb4e5c0036a6a8cdbc59b38d44f09ad8f6dc722751dad34d3df5bf0ac61913c1/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f6c6963656e73652d4d49542d626c7565)

![.NET](https://camo.githubusercontent.com/7732c145abc7fb05a8373d4d161318970723f355ddd1d080a3fbef3c6941cd0f/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f2e4e45542d392e302d707572706c65)

## 📝项目介绍

SharpIcoWeb是基于[SharpIco](https://github.com/star-plan/sharp-ico)开发的图片转ICO工具网站，支持上传png、jpg等图片转换为多尺寸的Ico图片文件。采用前后端分离技术。

使用 `.NET Minimal API`开发，够轻量。

## 🎯 应用场景

* 网站Favicon 🌐
* 软件图标 🖥️
* 个性化文件夹标识 📂

```html
<link rel="icon" type="image/ico+xml" href="/logo.ico" />
```

## ✨核心技术

| **⚡** <br />**Vite+Vue+Element-Plus**<br /> **极速的开发服务器和高效的生产构建** |          **🗂️ → ❌** <br />**纯文件操作（无需SQLite/MySQL）**          |
| :-------------------------------------------------------------------------------------------------: | :-----------------------------------------------------------------------------------: |
|       🏗️<br />**.NET 9 MiniAPI** <br />**轻量级API开发，处理图像转换业务逻辑**       | <br />**🖼️** <br />**后端使用的强大图像处理库，<br />实现PNG/JPG转ICO** |
|                        **🐳** <br />**可容器化（Docker 支持）**                        |              **📱 + 💻** <br />**响应式设计（适配移动端）**              |

## ✅后续更新

* [ ] 不同尺寸分别生成ICO文件。
* [ ] 前端显示ICO文件图标数量数据、大小、偏移等数据。
* [ ] 批量转换功能。

## 🚀快速开始

### Docker部署

#### Docker CLI

```dockerfile
docker-compose up --build -d
```

#### Docker Compose

```yaml
version: '3.8'

services:
  frontend:
    build:
      context: ./sharp-ico-vue   # 指向前端目录
      dockerfile: Dockerfile
    ports:
      - "5173:5173"               # 前端映射到宿主机的5173端口
    depends_on:
      - backend

  backend:
    build:
      context: .    # 指向后端目录
      dockerfile: Dockerfile
    ports:
      - "5235:5235"            # 后端端口
```

### 手动部署

```bash
git clone https://github.com/ZyPLJ/SharpIcoWeb.git

cd SharpIcoWeb

dotnet build -c Release

dotnet run

cd ..

cd sharp-ico-vue

npm install

npm run dev
```

## 👀如何使用

前后端项目运行或部署后，打开运行后网址。

选择需要生成的ICO图表尺寸，可多选

![image.png](https://raw.githubusercontent.com/ZyPLJ/note-gen-image-sync/main/2025-06/82586b88-2c72-47db-8c42-4b90d7b43235.png)

上传图片文件，点击转换。

![image.png](https://raw.githubusercontent.com/ZyPLJ/note-gen-image-sync/main/2025-06/77b11313-ff66-4d3f-924d-f31c0c16b349.png)

## 🛠 开发指南

### 项目结构

```
sharp-ico/
├── SharpIco/               # 图标转换类库  
│   ├── SharpIco.csproj
├── SharpIcoWeb/            # 后端Api项目
│   ├── SharpIcoWeb.csproj
├── sharp-ico-vue           # 前端项目
```

### 开发环境

* .Net 9
* Node.js 20.19+
* Vue3

### 运行项目

#### 后端

```bash
dotnet build -c Release

dotnet run
```

#### 前端

```bash
npm install

npm run dev
```

## 相关链接

* [SharpIco](https://github.com/star-plan/sharp-ico "SharpIco是一个纯 C# AOT 实现的轻量级图标生成工具")