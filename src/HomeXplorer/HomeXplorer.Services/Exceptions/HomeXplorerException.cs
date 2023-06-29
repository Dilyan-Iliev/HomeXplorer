namespace HomeXplorer.Services.Exceptions
{
    public class HomeXplorerException
        : Exception
    {
        public HomeXplorerException()
        {
        }

        public HomeXplorerException(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
