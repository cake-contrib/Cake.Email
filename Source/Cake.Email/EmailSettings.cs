using Cake.Core.Annotations;

namespace Cake.Email
{
    /// <summary>
    /// Class that lets you override default API settings
    /// </summary>
    [CakeAliasCategory("Email")]
    public sealed class EmailSettings
    {
        /// <summary>
        /// Gets or sets the SMTP host
        /// </summary>
        public string SmtpHost { get; set; }

        /// <summary>
        /// Gets or sets the port of the SMTP host
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the connection should be encrypted using Ssl
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Optional flag for if should throw exception on failure
        /// </summary>
        public bool? ThrowOnFail { get; set; }
    }
}