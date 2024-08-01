namespace WebApp.Utility
{
    public class SD
    {
        public static string CouponApiBaseUrl { get; set; }
        public static string AuthApiBaseUrl { get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JwtToken";
        public enum ApiType
        {
            Get,
            Post,
            Delete,
            Put
        }
    }
}
