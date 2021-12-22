using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Test;
using Cake.Core.Diagnostics;
using Cake.Frosting;

[TaskName("Test")]
[Dependency(typeof(BuildTask))]
public sealed class TestTask : FrostingTask<Context>
{
    internal const string TestDirectory = "test";

    public override void Run(Context context)
    {
        var testArtifacts = context.ConfigFile.TestArtifacts ?? Enumerable.Empty<Artifact>();

        if (!testArtifacts.Any())
        {
            context.Log.Warning("No test artifacts entered in configuration file");
        }

        int magic = 0;

        Parallel.ForEach(testArtifacts, () => 0, (testArtifact, loopState, localSum) =>
        {
            context.Log.Information("Testing component {0}", testArtifact.Component);

            context.DotNetCoreTest($"{TestDirectory}/{testArtifact.Component}/{testArtifact.Component}.csproj", new DotNetCoreTestSettings
            {
                Configuration = context.BuildConfiguration,
                NoBuild = true,
                Collectors = new[] { "XPlat Code Coverage" }
            });

            return ++localSum;
        }, r => Interlocked.Add(ref magic, r));

        if (magic != testArtifacts.Count())
        {
            context.Log.Warning("Processing of parallel tasks does not seem to have been successful");
        }
    }
}
