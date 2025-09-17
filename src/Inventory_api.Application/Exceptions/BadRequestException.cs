namespace Inventory_api.src.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Error { get; }

        // Original two-parameter constructor (second kept for explicit custom error label)
        public BadRequestException(string message, string error) : base(message)
        {
            Error = error;
        }

        // Convenience constructor used across the codebase
        public BadRequestException(string message) : base(message)
        {
            Error = "Bad Request";
        }
    }
}
