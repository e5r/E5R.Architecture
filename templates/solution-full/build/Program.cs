using Cake.Core;
using Cake.Frosting;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseWorkingDirectory("..")
            .UseContext<Context>()
            .Run(args);
    }
}
