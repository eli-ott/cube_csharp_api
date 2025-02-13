namespace MonApi.Shared.Exceptions
{
    public class SoftDeletedException : Exception
    {
        public SoftDeletedException(string message) : base(message) { }
    }
}
