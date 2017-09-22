using System;

namespace Cake.Email
{
    /// <summary>
    /// Represents an e-mail address.
    /// </summary>
    public class MailAddress
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailAddress"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <exception cref="ArgumentNullException">fileName is null.</exception>
        public MailAddress(string address)
            : this(address, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MailAddress"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="address">The address.</param>
        /// <exception cref="ArgumentNullException">fileName is null.</exception>
        public MailAddress(string address, string name)
        {
            this.Address = address;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}