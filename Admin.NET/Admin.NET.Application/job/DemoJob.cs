// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Admin.NET.Core.Service;
using Furion.Schedule;
using Microsoft.Extensions.Logging;
using SqlSugar;


namespace Admin.NET.Core;

/// <summary>
/// 有道云笔记签到作业任务
/// </summary>
// [DailyAt] //每天特定小时开始作业触发器特性
[JobDetail("job_Demo", Description = "Demo定时任务", GroupName = "Demo", Concurrent = false)]
[Daily(TriggerId = "trigger_Demo", Description = "Demo定时任务", RunOnStart = false)]
public class DemoJob(IServiceScopeFactory scopeFactory, ILoggerFactory loggerFactory) : IJob
{
    private readonly ILogger _logger = loggerFactory.CreateLogger(CommonConst.SysLogCategoryName);

    public Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        using var serviceScope = scopeFactory.CreateScope();

        var db = serviceScope.ServiceProvider.GetRequiredService<ISqlSugarClient>().CopyNew();
        var sysConfigService = serviceScope.ServiceProvider.GetRequiredService<SysConfigService>();

        string msg = $"【{DateTime.Now}】签到成功";
        var originColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(msg);
        Console.ForegroundColor = originColor;

        // 自定义日志
        _logger.LogInformation(msg);
        _logger.LogInformation($"【{DateTime.Now}】签到成功");
        return Task.CompletedTask;
    }
}

