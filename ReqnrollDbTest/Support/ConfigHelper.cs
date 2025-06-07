using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ReqnrollDbTest.Support.Configuration;

public class ConfigHelper
{
    private static readonly IConfigurationRoot _configuration;

    static ConfigHelper()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // or AppContext.BaseDirectory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        _configuration = builder.Build();
    }

    public static string GetConnectionString(string name = "PostgresDb")
    {
        return _configuration.GetConnectionString(name)!;
    }
}
