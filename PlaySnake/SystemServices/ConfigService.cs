using Microsoft.Extensions.Configuration;

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
        if (!OperatingSystem.IsWindows())
            return;

        int envMaxCols = Console.LargestWindowWidth;
        int envMaxRows = Console.LargestWindowHeight;

        var appName = ReadFromConfigFile(ConfigConsts.AppName);
        var title = ReadFromConfigFile(ConfigConsts.Window.Title) ?? appName ?? "PlaySnake";
        int minCols = int.Parse(ReadFromConfigFile(ConfigConsts.Window.MinColumns) ?? "80");
        int minRows = int.Parse(ReadFromConfigFile(ConfigConsts.Window.MinRows) ?? "24");
        int maxCols = int.Parse(ReadFromConfigFile(ConfigConsts.Window.MaxColumns) ?? "160");
        int maxRows = int.Parse(ReadFromConfigFile(ConfigConsts.Window.MaxRows) ?? "50");
        double scale = double.Parse(ReadFromConfigFile(ConfigConsts.Window.MaxRows) ?? "0.9");

        int targetCols = (int)(envMaxCols * scale);
        int targetRows = (int)(envMaxRows * scale);
        targetCols = Math.Clamp(targetCols, minCols, maxCols);
        targetRows = Math.Clamp(targetRows, minRows, maxRows);
        targetCols = Math.Min(targetCols, envMaxCols);
        targetRows = Math.Min(targetRows, envMaxRows);

        try
        {
            Console.Title = title;

            Console.SetBufferSize(
                Math.Max(Console.BufferWidth, targetCols),
                Math.Max(Console.BufferHeight, targetRows)
            );
            Console.SetWindowSize(targetCols, targetRows);

            ConsoleLayout.WriteHorizontallyCenteredBlock("Default configurations applied...");
            Thread.Sleep(1000);
            Console.Clear();
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
        internal const string MinColumns = SectionName + ":MinColumns";
        internal const string MinRows = SectionName + ":MinRows";
        internal const string MaxColumns = SectionName + ":MaxColumns";
        internal const string MaxRows = SectionName + ":MaxRows";
        internal const string Scale = SectionName + ":Scale";
    }
}