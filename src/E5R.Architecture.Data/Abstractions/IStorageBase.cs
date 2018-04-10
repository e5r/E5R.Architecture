namespace E5R.Architecture.Data.Abstractions
{
    public interface IStorageBase
    {
        void ConfigureSession(UnderlyingSession session);
    }
}
