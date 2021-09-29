using Cake.Frosting;

[TaskName("Dist")]
[IsDependentOn(typeof(DistributeTask))]
public class DistTask : FrostingTask
{
}
