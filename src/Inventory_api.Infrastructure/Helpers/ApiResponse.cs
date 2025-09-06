namespace Inventory_api.Infrastructure.Helpers
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }
    }

    public class ApiErrorResponse
    {
        public bool success { get; set; } = false;
        public string? message { get; set; }
        public object? error { get; set; }
    }
}
