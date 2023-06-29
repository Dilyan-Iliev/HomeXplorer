namespace HomeXplorer.Services.Exceptions
{
    using HomeXplorer.Services.Exceptions.Contracts;

    public class Guard
        : IGuard
    {
        public void AgainstNull<T>(T value, string? errorMessage = null)
        {
            if (value == null)
            {
                var exception = errorMessage == null ?
                    new HomeXplorerException()
                    : new HomeXplorerException(errorMessage);

                throw exception;
            }
        }
    }
}
