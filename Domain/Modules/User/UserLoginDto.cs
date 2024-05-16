namespace Domain.Modules.User
{
    /// <summary>
    /// User data to authenticate
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// User username
        /// </summary>
        /// <example>test@gmail.com</example>
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        /// <example>P@$$w0rd</example>
        public string Password { get; set; }
    }
}