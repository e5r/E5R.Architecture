namespace E5R.Architecture.Business.Test.Mocks
{
    public class EmptyBusinessObject : BusinessObject<EmptyBusinessObject, EmptyDataModule>
    {
        public EmptyDataModule ExposeModule => Module;

        public EmptyBusinessObject(object origin) : base(origin)
        {
        }
    }
}
