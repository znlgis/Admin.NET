using Furion;
using System.Reflection;
using System.Runtime.Loader;

namespace Admin.NET.Demo.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        Console.WriteLine(">>> SingleFilePublish.IncludeAssemblies() called");
        var baseDir = AppContext.BaseDirectory;
        var assemblies = new List<Assembly>();
        var names = new[] { "Admin.NET.Demo.Application", "Admin.NET.Core", "Admin.NET.Web.Core" };
        foreach (var name in names)
        {
            var path = Path.Combine(baseDir, $"{name}.dll");
            Console.WriteLine($">>>   Loading {name}: exists={File.Exists(path)}");
            if (File.Exists(path))
            {
                try
                {
                    var asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                    assemblies.Add(asm);
                    Console.WriteLine($">>>   Loaded {asm.GetName().Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($">>>   Failed: {ex.Message}");
                }
            }
        }
        return assemblies.ToArray();
    }

    public string[] IncludeAssemblyNames()
    {
        Console.WriteLine(">>> SingleFilePublish.IncludeAssemblyNames() called");
        return new[]
        {
            "Admin.NET.Demo.Application",
            "Admin.NET.Core",
            "Admin.NET.Web.Core",
        };
    }
}
