using Cake.Core.Annotations;
using System.Text;

namespace Cake.Email
{
	/// <summary>
	/// The result of EmailProvider.
	/// </summary>
	[CakeAliasCategory("Email")]
	public sealed class EmailResult
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EmailResult"/> class.
		/// </summary>
		/// <param name="ok">Indicating success or failure.</param>
		/// <param name="timeStamp">Timestamp of the message.</param>
		/// <param name="error">Error message on failure.</param>
		public EmailResult(bool ok, string timeStamp, string error)
		{
			Ok = ok;
			TimeStamp = timeStamp;
			Error = error;
		}

		/// <summary>
		/// Gets a value indicating whether success or failure, <see cref="Error"/> for info on failure.
		/// </summary>
		public bool Ok { get; private set; }

		/// <summary>
		/// Gets the Timestamp of the message.
		/// </summary>
		public string TimeStamp { get; private set; }

		/// <summary>
		/// Gets the Error message on failure.
		/// </summary>
		public string Error { get; private set; }

		/// <summary>
		/// Convert this instance of value to a string representation.
		/// </summary>
		/// <returns>The complete string representation of the EmailResult.</returns>
		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.Append("{ Ok = ");
			builder.Append(Ok);
			builder.Append(", TimeStamp = ");
			builder.Append(TimeStamp);
			builder.Append(", Error = ");
			builder.Append(Error);
			builder.Append(" }");
			return builder.ToString();
		}
	}
}
