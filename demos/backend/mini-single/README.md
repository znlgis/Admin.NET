# Admin.NET Mini Single（单项目最小化实现）

基于 NuGet 包的 Admin.NET 最小化后端实现。
与 `mini` 示例不同，`mini-single` 采用**单项目解决方案**（仅一个 Web 项目）。

## 项目结构

```
demos/backend/mini-single/
├── Admin.NET.Mini.sln                          # 单项目解决方案
└── Admin.NET.Mini.Application/                 # 单个 Web 项目（NuGet）
    ├── Configuration/                          # 配置文件
    ├── wwwroot/                                # 静态资源目录（必须）
    ├── Program.cs                              # 程序入口
    ├── Startup.cs                              # 启动配置
    ├── HelloApi.cs                             # 示例接口
    ├── GlobalUsings.cs                         # 全局引用
    ├── appsettings.json                        # 应用配置
    └── Admin.NET.Mini.Application.csproj
```

## 依赖包

- `Fs.Admin.NET.Core` 2.0.2
- `Fs.Admin.NET.Web.Core` 2.0.2

## 快速开始

```bash
cd demos/backend/mini-single
dotnet restore
dotnet build
dotnet run --project Admin.NET.Mini.Application
```

启动后默认访问地址：

- `http://localhost:5006`
- `http://localhost:5006/index.html`
- `http://localhost:5006/api/hello/hello`
- `http://localhost:5006/api/hello/time`
