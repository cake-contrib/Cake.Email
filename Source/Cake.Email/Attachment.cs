using System;
using System.IO;

namespace Cake.Email
{
    /// <summary>
    /// Represents an attachment to an e-mail.
    /// </summary>
    public class Attachment : AttachmentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment"/> class
        /// with the specified file name and mime type.
        /// </summary>
        /// <param name="fileName">The file name holding the content for this attachment.</param>
        /// <param name="mimeType">The MIME media type of the content.</param>
        /// <exception cref="ArgumentNullException">fileName is null.</exception>
        public Attachment(string fileName, string mimeType)
            : base(fileName, mimeType)
        {
            this.Name = Path.GetFileName(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment"/> class
        /// with the specified Stream and mime type.
        /// </summary>
        /// <param name="contentStream">A stream containing the content for this attachment.</param>
        /// <param name="mimeType">The MIME media type of the content.</param>
        /// <param name="fileName">The name of the attachment.</param>
        /// <exception cref="ArgumentNullException">contentStream is null.</exception>
        public Attachment(Stream contentStream, string mimeType, string fileName)
            : base(contentStream, mimeType)
        {
            this.Name = Path.GetFileName(fileName);
        }

        /// <summary>
        /// Gets or sets the name of this attachment.
        /// </summary>
        public string Name { get; set; }
    }
}