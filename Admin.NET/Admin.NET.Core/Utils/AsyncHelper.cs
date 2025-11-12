// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！

using Nito.AsyncEx;

namespace Admin.NET.Core.Utils;
/// <summary>
/// Provides some helper methods to work with async methods.
/// </summary>
public static class AsyncHelper
{
    /// <summary>
    /// Checks if given method is an async method.
    /// </summary>
    /// <param name="method">A method to check</param>
    public static bool IsAsync(this MethodInfo method)
    {
        return method.ReturnType.IsTaskOrTaskOfT();
    }

    public static bool IsTaskOrTaskOfT(this Type type)
    {
        return type == typeof(Task) || (IntrospectionExtensions.GetTypeInfo(type).IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>));
    }

    public static bool IsTaskOfT(this Type type)
    {
        return IntrospectionExtensions.GetTypeInfo(type).IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>);
    }

    /// <summary>
    /// Returns void if given type is Task.
    /// Return T, if given type is Task{T}.
    /// Returns given type otherwise.
    /// </summary>
    public static Type UnwrapTask(Type type)
    {
        if (type == typeof(Task))
        {
            return typeof(void);
        }

        if (type.IsTaskOfT())
        {
            return type.GenericTypeArguments[0];
        }

        return type;
    }

    /// <summary>
    /// Runs a async method synchronously.
    /// </summary>
    /// <param name="func">A function that returns a result</param>
    /// <typeparam name="TResult">Result type</typeparam>
    /// <returns>Result of the async operation</returns>
    public static TResult RunSync<TResult>(Func<Task<TResult>> func)
    {
        return AsyncContext.Run(func);
    }

    /// <summary>
    /// Runs a async method synchronously.
    /// </summary>
    /// <param name="action">An async action</param>
    public static void RunSync(Func<Task> action)
    {
        AsyncContext.Run(action);
    }
}

