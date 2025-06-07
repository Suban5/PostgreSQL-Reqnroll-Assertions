using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ReqnrollDbTest.Support;

public class ConfigHelper
{
    private static readonly IConfigurationRoot Configuration;

    static ConfigHelper()
    {
        Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // or AppContext.BaseDirectory
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

    }

    public static string PostgreSQLConnectionString => Configuration.GetConnectionString("PostgreSQL");
}
