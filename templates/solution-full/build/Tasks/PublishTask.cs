using System.Linq;

using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Publish;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Cake.Common.Tools.DotNetCore.Pack;

[TaskName("Publish")]
public sealed class PublishTask : FrostingTask<Context>
{
    internal const string SourceDirectory = "src";
    internal const string ArtifactsDirectory = "artifacts";

    public override void Run(Context context)
    {
        var artifacts = context.ConfigFile.PublishArtifacts ?? Enumerable.Empty<Artifact>();

        if (!artifacts.Any())
        {
            context.Log.Warning("No publishing artifacts entered in configuration file");
        }

        foreach (var artifact in artifacts)
        {
            var artifactName = $"{artifact.Name}";

            if (artifact.Nuget)
            {
                artifactName += !string.IsNullOrWhiteSpace(context.VersionSuffix) ? $"_{context.VersionSuffix}" : string.Empty;
                context.Log.Information("Publishing artifact {0} from component {1}", artifactName, artifact.Component);

                context.DotNetCorePack($"{SourceDirectory}/{artifact.Component}/{artifact.Component}.csproj", new DotNetCorePackSettings
                {
                    Configuration = context.BuildConfiguration,
                    OutputDirectory = $"{ArtifactsDirectory}/{artifactName}",
                    VersionSuffix = context.VersionSuffix
                });
            }
            else
            {
                artifactName += $"_{context.Runtime.ToLowerInvariant()}";
                context.Log.Information("Publishing artifact {0} from component {1}", artifactName, artifact.Component);

                context.DotNetCorePublish($"{SourceDirectory}/{artifact.Component}/{artifact.Component}.csproj", new DotNetCorePublishSettings
                {
                    Configuration = context.BuildConfiguration,
                    VersionSuffix = context.VersionSuffix,
                    Runtime = context.Runtime,
                    OutputDirectory = $"{ArtifactsDirectory}/{artifactName}"
                });
            }

            (context.ConfigFile.PublishGlobalExcludes ?? Enumerable.Empty<string>()).ToList().ForEach(pattern =>
            {
                var excludePatternPath = $"{ArtifactsDirectory}/{artifactName}/{pattern}";

                context.Log.Information($"Removing files entered in the global configuration: {excludePatternPath}");
                context.DeleteFiles(excludePatternPath);
            });

            (artifact.Excludes ?? Enumerable.Empty<string>()).ToList().ForEach(pattern =>
            {
                var excludePatternPath = $"{ArtifactsDirectory}/{artifactName}/{pattern}";

                context.Log.Information($"Removing files entered in the artifact configuration: {excludePatternPath}");
                context.DeleteFiles(excludePatternPath);
            });
        }
    }
}
