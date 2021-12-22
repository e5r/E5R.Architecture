using System;

public class ConfigFile
{
    private static ConfigFile _defaultInstance;

    public string SolutionFile { get; set; }
    public string[] PublishGlobalExcludes { get; set; }
    public Artifact[] PublishArtifacts { get; set; }
    public Artifact[] TestArtifacts { get; set; }

    public void Ensure()
    {
        if (string.IsNullOrWhiteSpace(SolutionFile))
        {
            throw new Exception("Solution file not detected. Check your build.json file.");
        }
    }

    public static ConfigFile Default => _defaultInstance ?? (_defaultInstance = new ConfigFile());
}
