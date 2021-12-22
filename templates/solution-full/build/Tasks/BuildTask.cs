using System;

using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Frosting;

[TaskName("Build")]
public sealed class BuildTask : FrostingTask<Context>
{
    public override void Run(Context context)
    {
        context.DotNetCoreBuild(context.ConfigFile.SolutionFile, new DotNetCoreBuildSettings
        {
            Configuration = context.BuildConfiguration,
            MSBuildSettings = new DotNetCoreMSBuildSettings
            {
                NoLogo = true,
                MaxCpuCount = Environment.ProcessorCount
            }
        });
    }
}
