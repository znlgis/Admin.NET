# Admin.NET Mini Single (单项目最小化实现)

基于 NuGet 包的 Admin.NET 最小化后端实现。与 `mini` 不同，`mini-single` 只包含一个项目（一个 `.csproj`）。

## 项目结构

```
demos/backend/mini-single/
├── Admin.NET.Mini.Single.csproj                 # 单项目（Web + Application）
├── Program.cs                                   # 程序入口
├── Startup.cs                                   # 启动配置
├── HelloApi.cs                                  # 示例接口
├── GlobalUsings.cs                              # 全局引用
├── appsettings.json                             # 配置入口
├── Configuration/
│   └── App.json                                 # 应用配置
└── wwwroot/                                     # 静态资源目录（必须）
```

## 快速开始

```bash
cd demos/backend/mini-single
dotnet restore
dotnet build
dotnet run
```

启动后访问：

- 应用地址: `http://localhost:5006`
- Swagger UI: `http://localhost:5006/index.html`
- Hello 接口: `http://localhost:5006/api/hello/hello`
- Time 接口: `http://localhost:5006/api/hello/time`
