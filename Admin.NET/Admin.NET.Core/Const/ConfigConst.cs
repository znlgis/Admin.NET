// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

namespace Admin.NET.Core;

/// <summary>
/// 配置常量
/// </summary>
public class ConfigConst
{
    /// <summary>
    /// 演示环境
    /// </summary>
    public const string SysDemoEnv = "SYS_DEMO";

    /// <summary>
    /// 默认密码
    /// </summary>
    public const string SysPassword = "SYS_PASSWORD";

    /// <summary>
    /// 密码最大错误次数
    /// </summary>
    public const string SysPasswordMaxErrorTimes = "SYS_PASSWORD_MAX_ERROR_TIMES";

    /// <summary>
    /// 日志保留天数
    /// </summary>
    public const string SysLogRetentionDays = "SYS_LOG_RETENTION_DAYS";

    /// <summary>
    /// 记录操作日志
    /// </summary>
    public const string SysOpLog = "SYS_OPLOG";

    /// <summary>
    /// 单设备登录
    /// </summary>
    public const string SysSingleLogin = "SYS_SINGLE_LOGIN";

    /// <summary>
    /// 登入登出提醒
    /// </summary>
    public const string SysLoginOutReminder = "SYS_LOGIN_OUT_REMINDER";

    /// <summary>
    /// 登陆时隐藏租户
    /// </summary>
    public const string SysHideTenantLogin = "SYS_HIDE_TENANT_LOGIN";

    /// <summary>
    /// 登录二次验证
    /// </summary>
    public const string SysSecondVer = "SYS_SECOND_VER";

    /// <summary>
    /// 图形验证码
    /// </summary>
    public const string SysCaptcha = "SYS_CAPTCHA";

    /// <summary>
    /// Token过期时间
    /// </summary>
    public const string SysTokenExpire = "SYS_TOKEN_EXPIRE";

    /// <summary>
    /// RefreshToken过期时间
    /// </summary>
    public const string SysRefreshTokenExpire = "SYS_REFRESH_TOKEN_EXPIRE";

    /// <summary>
    /// 发送异常日志邮件
    /// </summary>
    public const string SysErrorMail = "SYS_ERROR_MAIL";

    /// <summary>
    /// 域登录验证
    /// </summary>
    public const string SysDomainLogin = "SYS_DOMAIN_LOGIN";

    // /// <summary>
    // /// 租户域名隔离登录验证
    // /// </summary>
    // public const string SysTenantHostLogin = "SYS_TENANT_HOST_LOGIN";

    /// <summary>
    /// 数据校验日志
    /// </summary>
    public const string SysValidationLog = "SYS_VALIDATION_LOG";

    /// <summary>
    /// 行政区域同步层级 1-省级,2-市级,3-区县级,4-街道级,5-村级
    /// </summary>
    public const string SysRegionSyncLevel = "SYS_REGION_SYNC_LEVEL";

    /// <summary>
    /// Default 分组
    /// </summary>
    public const string SysDefaultGroup = "DEFAULT";

    /// <summary>
    /// 支付宝授权页面地址
    /// </summary>
    public const string AlipayAuthPageUrl = "ALIPAY_AUTH_PAGE_URL_";

    // /// <summary>
    // /// 系统图标
    // /// </summary>
    // public const string SysWebLogo = "SYS_WEB_LOGO";
    //
    // /// <summary>
    // /// 系统主标题
    // /// </summary>
    // public const string SysWebTitle = "SYS_WEB_TITLE";
    //
    // /// <summary>
    // /// 系统副标题
    // /// </summary>
    // public const string SysWebViceTitle = "SYS_WEB_VICETITLE";
    //
    // /// <summary>
    // /// 系统描述
    // /// </summary>
    // public const string SysWebViceDesc = "SYS_WEB_VICEDESC";
    //
    // /// <summary>
    // /// 水印内容
    // /// </summary>
    // public const string SysWebWatermark = "SYS_WEB_WATERMARK";
    //
    // /// <summary>
    // /// 版权说明
    // /// </summary>
    // public const string SysWebCopyright = "SYS_WEB_COPYRIGHT";
    //
    // /// <summary>
    // /// ICP备案号
    // /// </summary>
    // public const string SysWebIcp = "SYS_WEB_ICP";
    //
    // /// <summary>
    // /// ICP地址
    // /// </summary>
    // public const string SysWebIcpUrl = "SYS_WEB_ICPURL";
}