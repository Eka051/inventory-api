namespace API_Manajemen_Barang.Helpers
{
    public class PasswordHasherHelper
    {
        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
