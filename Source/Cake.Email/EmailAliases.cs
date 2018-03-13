using Cake.Core;
using Cake.Core.Annotations;
using System;

namespace Cake.Email
{
	/// <summary>
	/// <para>Contains aliases related to emails.</para>
	/// <para>
	/// In order to use the commands for this addin, you will need to include the following in your build.cake file to download and
	/// reference from NuGet.org:
	/// <code>
	/// #addin Cake.Email
	/// </code>
	/// </para>
	/// </summary>
	[CakeAliasCategory("Email")]
	public static class EmailAliases
	{
		/// <summary>
		/// Gets a <see cref="EmailProvider"/> instance that can be used for sending emails.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>A <see cref="EmailProvider"/> instance.</returns>
		[CakePropertyAlias(Cache = true)]
		[CakeNamespaceImport("Cake.Email")]
		public static EmailProvider Email(this ICakeContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			return new EmailProvider(context);
		}
	}
}
