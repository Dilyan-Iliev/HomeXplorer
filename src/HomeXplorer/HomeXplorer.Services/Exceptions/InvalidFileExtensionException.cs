namespace HomeXplorer.Services.Exceptions
{
    public class InvalidFileExtensionException
        : Exception
    {
        public InvalidFileExtensionException()
        {
        }

        public InvalidFileExtensionException(string errorMessage)
            : base(errorMessage)
        {
        }
    }
}
