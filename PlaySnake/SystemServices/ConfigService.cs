using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Text.Json;

namespace PlaySnake.SystemServices;

internal class ConfigService
{
    private const string _configFilePath = "/../../../config.json";
    private static readonly IConfigurationRoot _configRoot;

    static ConfigService()
    {
        _configRoot = new ConfigurationBuilder()
                            .SetBasePath(AppContext.BaseDirectory)
                            .AddJsonFile(Directory.GetCurrentDirectory() + _configFilePath, optional: false, reloadOnChange: true)
                            .Build();
    }

    internal static void SetConstants()
    {
        var appName = ReadFromConfigFile(ConfigConsts.AppName);
        var title = ReadFromConfigFile(ConfigConsts.Window.Title) ?? appName ?? "PlaySnake";
        var windowWidth = int.Parse(ReadFromConfigFile(ConfigConsts.Window.Width) ?? "0");
        var windowHeight = int.Parse(ReadFromConfigFile(ConfigConsts.Window.Height) ?? "0");
        //var fullscreen = bool.Parse(ReadFromConfigFile(ConfigConsts.Window.Fullscreen) ?? "0");
        //var resizable = bool.Parse(ReadFromConfigFile(ConfigConsts.Window.Resizable) ?? "0");

        try
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                Console.SetBufferSize(windowWidth + 100, windowHeight + 100);
            }
            Console.SetWindowSize(windowWidth, windowHeight);

            Console.Title = title;
            Console.WriteLine($"Console size set to {windowWidth} columns by {windowHeight} rows.");
            Console.WriteLine("Press any key to exit...");
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("\nCould not set the window size. It may be larger than the maximum allowed by your screen resolution and console font.");
        }
        catch (IOException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("\nCould not set the window size. Output might be redirected.");
        }

        Console.ReadKey();
    }

    private static string? ReadFromConfigFile(string section)
    {
        return _configRoot.GetSection(section).Value;
    }
}

internal static class ConfigConsts
{
    internal const string AppName = "AppName";
    internal const string Version = "Version";
    internal const string Author = "Author";

    internal static class Window
    {
        internal const string SectionName = "Window";

        internal const string Title = SectionName + ":Title";
        internal const string Width = SectionName + ":Width";
        internal const string Height = SectionName + ":Height";
        internal const string Resizable = SectionName + ":Resizable";
        internal const string Fullscreen = SectionName + ":Fullscreen";
    }
}