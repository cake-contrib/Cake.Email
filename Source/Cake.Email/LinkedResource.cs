using System;
using System.IO;

namespace Cake.Email
{
    /// <summary>
    ///  Represents an embedded external resource in an email attachment, such as an image
    ///  in an HTML attachment.
    /// </summary>
    public class LinkedResource : AttachmentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedResource"/> class
        /// with the specified file name, mime type and content ID.
        /// </summary>
        /// <param name="fileName">The file name holding the content for this attachment.</param>
        /// <param name="mimeType">The MIME media type of the content.</param>
        /// <param name="contentId">The ID of the content.</param>
        /// <exception cref="ArgumentNullException">fileName is null.</exception>
        public LinkedResource(string fileName, string mimeType, string contentId)
            : base(fileName, mimeType)
        {
            this.ContentId = contentId;
            this.Name = Path.GetFileName(fileName);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedResource"/> class
        /// with the specified Stream, mime type and content ID.
        /// </summary>
        /// <param name="contentStream">A stream containing the content for this attachment.</param>
        /// <param name="fileName">The name of the attachment.</param>
        /// <param name="mimeType">The MIME media type of the content.</param>
        /// <param name="contentId">The ID of the content.</param>
        /// <exception cref="ArgumentNullException">contentStream is null.</exception>
        public LinkedResource(Stream contentStream, string fileName, string mimeType, string contentId)
            : base(contentStream, mimeType)
        {
            this.ContentId = contentId;
            this.Name = Path.GetFileName(fileName);
        }

        /// <summary>
        /// Gets or sets the MIME content ID for this attachment.
        /// </summary>
        public string ContentId { get; set; }

        /// <summary>
        /// Gets or sets the name of this attachment.
        /// </summary>
        public string Name { get; set; }
    }
}