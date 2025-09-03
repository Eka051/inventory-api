namespace Inventory_api.src.Application.DTOs
{
    public class LoginRequestDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string? accessToken { get; set; }
        public string? refreshToken { get; set; }
    }
}
