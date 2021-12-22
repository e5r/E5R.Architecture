using Cake.Frosting;

[TaskName("Default")]
[IsDependentOn(typeof(TestTask))]
public class DefaultTask : FrostingTask
{
}
