using System.IO;
using System;
using System.Linq;

using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using System.Text.Json;
using Cake.Common;
using Microsoft.DotNet.PlatformAbstractions;

public class Context : FrostingContext
{
    private const string BuildConfigFile = "build.json";

    public string BasePath { get; set; }
    public string BuildConfiguration { get; set; }
    public string Runtime { get; set; }
    public string VersionSuffix { get; set; }
    public DistributeFormat DistributeFormat { get; set; }
    public ConfigFile ConfigFile { get; set; }

    public Context(ICakeContext context)
        : base(context)
    {
        BuildConfiguration = context.Argument("configuration", "Release");
        Runtime = context.Argument("runtime", RuntimeEnvironment.GetRuntimeIdentifier());
        VersionSuffix = context.Argument("version-suffix", string.Empty);

        var distFormat = context.Argument("dist-format", "zip").ToLowerInvariant();

        switch (distFormat)
        {
            case "bz":
            case "bz2":
            case "bzip":
            case "bzip2":
                DistributeFormat = DistributeFormat.BZip2;
                break;

            case "gz":
            case "gzip":
                DistributeFormat = DistributeFormat.GZip;
                break;

            // "z"
            // "zip"
            default:
                if (!string.IsNullOrEmpty(distFormat) && !new[] { "z", "zip" }.Contains(distFormat))
                {
                    context.Log.Warning($"Invalid \"dist-format\" parameter. Assuming the default value \"{DistributeFormat.Zip}\"");
                }
                DistributeFormat = DistributeFormat.Zip;
                break;
        }

        var path = SearchBuildConfigFile();

        ConfigFile = !string.IsNullOrWhiteSpace(path)
            ? LoadBuildConfigFromFile(path)
            : ConfigFile.Default;

        ConfigFile.Ensure();

        BasePath = BasePath ?? Directory.GetCurrentDirectory();
    }

    private string SearchBuildConfigFile()
    {
        var pathBase = AppDomain.CurrentDomain.BaseDirectory;

        while (pathBase != null)
        {
            var filePath = Path.Combine(pathBase, BuildConfigFile);

            if (File.Exists(filePath))
            {
                BasePath = Path.GetDirectoryName(filePath);

                return filePath;
            }

            pathBase = Path.GetDirectoryName(pathBase);
        }

        BasePath = null;

        return null;
    }

    private ConfigFile LoadBuildConfigFromFile(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Build config file not found!", filePath);
            }

            return JsonSerializer.Deserialize<ConfigFile>(File.ReadAllText(filePath), new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
        catch (Exception ex)
        {
            Log.Error("Failed to load build settings: ", ex.Message);
        }

        Log.Warning("Using default build configuration");

        return ConfigFile.Default;
    }
}
