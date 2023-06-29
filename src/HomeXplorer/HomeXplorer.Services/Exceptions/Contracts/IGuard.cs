namespace HomeXplorer.Services.Exceptions.Contracts
{
    public interface IGuard
    {
        void AgainstNull<T>(T value, string? errorMessage = null);
    }
}
