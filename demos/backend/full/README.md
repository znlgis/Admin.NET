# Admin.NET Demo (NuGet-based)

基于 NuGet 包的 Admin.NET 示例程序，借鉴官方 `Admin.NET.Application` 项目结构。

## 项目结构

```
demo/
├── Admin.NET.Demo.sln                          # 解决方案文件
├── Admin.NET.Demo.Application/                 # 业务应用层（引用 NuGet 包）
│   ├── Configuration/                          # 配置文件
│   ├── Const/                                  # 常量定义
│   ├── Entity/                                 # 实体定义
│   ├── EventBus/                               # 事件总线订阅
│   ├── OpenApi/                                # 开放接口
│   ├── GlobalUsings.cs                         # 全局引用
│   └── Startup.cs                              # 启动配置
└── Admin.NET.Demo.Web.Entry/                   # Web 入口项目
    ├── Properties/launchSettings.json          # 启动配置
    ├── Program.cs                              # 程序入口
    ├── appsettings.json                        # 应用配置
    └── appsettings.Development.json            # 开发环境配置
```

## 与官方项目的区别

| 项目 | 官方 Admin.NET.Application | Demo |
|------|---------------------------|------|
| 依赖方式 | `ProjectReference` (源码引用) | `PackageReference` (NuGet 包引用) |
| Admin.NET.Core | 项目引用 | NuGet 包 `Fs.Admin.NET.Core` |
| Admin.NET.Web.Core | 项目引用 | NuGet 包 `Fs.Admin.NET.Web.Core` |

## 快速开始

### 前提条件

- .NET 8.0 SDK 或 .NET 10.0 SDK
- NuGet 包源中已发布 `Fs.Admin.NET.Core` 和 `Fs.Admin.NET.Web.Core`

### 构建和运行

```bash
# 进入 demo 目录
cd demo

# 还原依赖
dotnet restore

# 构建
dotnet build

# 运行
dotnet run --project Admin.NET.Demo.Web.Entry
```

访问 `http://localhost:5005` 即可。

## 使用本地 NuGet 包

如果 NuGet 包尚未发布到 NuGet.org，可以使用本地包源：

```bash
# 在项目根目录打包
cd Admin.NET
dotnet pack Admin.NET.Core/Admin.NET.Core.csproj -c Release -o ../nupkgs
dotnet pack Admin.NET.Web.Core/Admin.NET.Web.Core.csproj -c Release -o ../nupkgs

# 在 demo 目录添加本地包源并还原
cd ../demo
dotnet nuget add source ../nupkgs --name local
dotnet restore
```
