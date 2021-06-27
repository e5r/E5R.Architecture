namespace E5R.Architecture.Infrastructure.AspNetCore.Workers
{
    public class WorkManagerOptions
    {
        public int DelayMinimum { get; set; }
        public int DelayMaximum { get; set; }
        public int DelayIncrement { get; set; }
    }
}
