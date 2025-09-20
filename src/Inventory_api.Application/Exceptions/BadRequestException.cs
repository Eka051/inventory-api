namespace Inventory_api.src.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Error { get; }

        public BadRequestException(string message, string error) : base(message)
        {
            Error = error;
        }

        public BadRequestException(string message) : base(message)
        {
            Error = "Bad Request";
        }
    }
}
