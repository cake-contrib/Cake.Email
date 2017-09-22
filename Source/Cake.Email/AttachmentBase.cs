using System;
using System.IO;

namespace Cake.Email
{
    /// <summary>
    /// Base class that represents an email attachment.
    /// </summary>
    public abstract class AttachmentBase : IDisposable
    {
        private bool _isDisposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentBase"/> class
        /// with the specified file name and mime type.
        /// </summary>
        /// <param name="fileName">The file name holding the content for this attachment.</param>
        /// <param name="mimeType">The MIME media type of the content.</param>
        /// <exception cref="ArgumentNullException">fileName is null.</exception>
        protected AttachmentBase(string fileName, string mimeType)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (fileName == string.Empty)
            {
                throw new ArgumentException("filename is empty", nameof(fileName));
            }

            this.ContentStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            this.MimeType = !string.IsNullOrEmpty(mimeType) ? mimeType : throw new ArgumentException("mimeType is null or empty", nameof(mimeType));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentBase"/> class
        /// with the specified Stream and mime type.
        /// </summary>
        /// <param name="contentStream">A stream containing the content for this attachment.</param>
        /// <param name="mimeType">The MIME media type of the content.</param>
        /// <exception cref="ArgumentNullException">contentStream is null.</exception>
        protected AttachmentBase(Stream contentStream, string mimeType)
        {
            this.ContentStream = contentStream ?? throw new ArgumentNullException(nameof(contentStream));
            this.MimeType = !string.IsNullOrEmpty(mimeType) ? mimeType : throw new ArgumentException("mimeType is empty", nameof(mimeType));
        }

        /// <summary>
        /// Gets or sets the content stream of this attachment.
        /// </summary>
        public Stream ContentStream { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of this attachment.
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Releases the resources used by the System.Net.Mail.AttachmentBase.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Releases the unmanaged resources used by the System.Net.Mail.AttachmentBase and
        /// optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_isDisposed)
            {
                _isDisposed = true;
                ContentStream.Dispose();
            }
        }
    }
}