# Admin.NET Mini (最小化实现)

基于 NuGet 包的 Admin.NET 最小化后端实现，演示如何快速搭建一个最简单的 Admin.NET 应用。

## 项目结构

```
demos/backend/mini/
├── Admin.NET.Mini.sln                          # 解决方案文件
├── Admin.NET.Mini.Application/                 # 业务应用层（引用 NuGet 包）
│   ├── Configuration/                          # 配置文件
│   │   └── App.json                           # 应用配置
│   ├── GlobalUsings.cs                         # 全局引用
│   ├── Startup.cs                              # 启动配置
│   ├── HelloApi.cs                             # 示例接口
│   └── Admin.NET.Mini.Application.csproj
└── Admin.NET.Mini.Web.Entry/                   # Web 入口项目
    ├── wwwroot/                                # 静态资源目录（必须）
    ├── Program.cs                              # 程序入口
    ├── appsettings.json                        # 应用配置
    └── Admin.NET.Mini.Web.Entry.csproj
```

## 特点

- **最小化配置**：只包含运行所需的最基本配置
- **简单易懂**：代码结构简洁，适合快速学习和上手
- **基于 NuGet**：使用 `Fs.Admin.NET.Core` 和 `Fs.Admin.NET.Web.Core` NuGet 包
- **示例接口**：包含简单的 Hello World 接口演示

## 前提条件

- .NET 8.0 SDK 或 .NET 10.0 SDK
- NuGet 包源中已发布 `Fs.Admin.NET.Core` 和 `Fs.Admin.NET.Web.Core` (版本 2.0.2)

## 快速开始

### 1. 进入项目目录

```bash
cd demos/backend/mini
```

### 2. 还原依赖

```bash
dotnet restore
```

### 3. 构建项目

```bash
dotnet build
```

### 4. 运行项目

```bash
dotnet run --project Admin.NET.Mini.Web.Entry
```

### 5. 访问应用

启动后，访问以下地址：

- 应用地址: `http://localhost:5006`
- Swagger UI: `http://localhost:5006/index.html`
- Hello 接口: `http://localhost:5006/api/hello/hello`
- Time 接口: `http://localhost:5006/api/hello/time`

## 示例接口

项目包含一个简单的 `HelloApi` 控制器，提供两个接口：

1. **GET /api/hello/hello** - 返回 Hello World 消息
2. **GET /api/hello/time** - 返回服务器时间和时间戳

这些接口允许匿名访问（`[AllowAnonymous]`），可以直接在浏览器中测试。

## 与完整版的区别

| 项目 | 完整版 (full) | 最小版 (mini) |
|------|--------------|--------------|
| 配置文件 | 多个配置文件（20+） | 只有 App.json |
| 示例代码 | 完整的业务示例 | 最简单的 Hello API |
| 数据库 | 包含数据库配置和文件 | 不包含 |
| 静态资源 | 包含 IP 数据库等资源 | 只有空的 wwwroot |
| 功能模块 | Entity、EventBus、Const 等 | 只有 HelloApi |

## 扩展开发

在此基础上，您可以：

1. 添加更多的 API 控制器到 `Application` 项目
2. 添加实体类和数据库配置
3. 配置更多的 Furion 功能（如缓存、日志等）
4. 参考完整版示例添加更多配置文件

## 使用本地 NuGet 包

如果需要使用本地构建的 NuGet 包：

```bash
# 1. 打包核心项目
cd ../../Admin.NET
dotnet pack Admin.NET.Core/Admin.NET.Core.csproj -c Release -o ../nupkgs /p:PackageId=Fs.Admin.NET.Core /p:IsPackable=true
dotnet pack Admin.NET.Web.Core/Admin.NET.Web.Core.csproj -c Release -o ../nupkgs /p:PackageId=Fs.Admin.NET.Web.Core /p:IsPackable=true

# 2. 添加本地包源
cd ../demos/backend/mini
dotnet nuget add source $(pwd)/../../../nupkgs --name local

# 3. 还原和构建
dotnet restore
dotnet build
```

## 常见问题

### 安全说明

⚠️ **重要**: 本示例中的配置文件包含示例密钥和凭据，仅用于演示目的。在生产环境中使用时，请务必：
- 更改所有密钥、密码和令牌
- 使用环境变量或安全配置提供程序存储敏感信息
- 不要将实际的生产凭据提交到版本控制系统

### 1. 启动失败 "provider root path cannot be determined"

确保 `wwwroot` 目录存在。这是 Furion 使用静态文件服务所必需的。

### 2. NuGet 包未找到

确保：
- 已发布 `Fs.Admin.NET.Core` 和 `Fs.Admin.NET.Web.Core` 到 NuGet.org
- 或已正确配置本地包源

### 3. 程序集加载失败

确保 `Configuration/App.json` 中的 `ExternalAssemblies` 包含：
```json
"ExternalAssemblies": [ "Admin.NET.Core.dll", "Admin.NET.Web.Core.dll" ]
```

## 参考

- [完整版示例](../full/) - 包含更多功能和配置的完整示例
- [Admin.NET 官方文档](https://gitee.com/zuohuaijun/Admin.NET)
- [Furion 文档](https://furion.baiqian.ltd/)