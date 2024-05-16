namespace Domain.Modules.Auth
{
    /// <summary>
    /// Response from an authentication operation
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// User authenticated
        /// </summary>
        public UserAuthDto User { get; set; }

        /// <summary>
        /// JWT generated for the user
        /// </summary>
        public string Token { get; set; }

        public AuthResponse()
        {
        }

        public AuthResponse(UserAuthDto user, string token)
        {
            User = user;
            Token = token;
        }
    }
}