using System;

namespace cccstc
{
	/// <summary>
	/// Represents information left by a student worker regarding the lab.
	/// </summary>
	public class Info
	{
		/// <summary>
		/// Represents the title of the informational post
		/// </summary>
		/// <value>The title of the post</value>
		public string Title {get; set;}

		/// <summary>
		/// Represents the text of this informational post
		/// </summary>
		/// <value>The text of the post</value>
		public string Text { get; set;}

		/// <summary>
		/// Whether or not this post will appear at the top of the informational page
		/// </summary>
		/// <value><c>true</c> if sticky; otherwise, <c>false</c>.</value>
		public bool Sticky { get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="cccstc.Info"/> class.
		/// </summary>
		/// <param name="title">The title of the post</param>
		/// <param name="text">The text of the post</param>
		public Info (string title, string text)
		{
			Title = title;
			Text = text;
			Sticky = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="cccstc.Info"/> class.
		/// This constructor allows you to set the stickiness of the post to true
		/// </summary>
		/// <param name="title">The title of the post</param>
		/// <param name="text">The body of the post</param>
		/// <param name="sticky">If set to <c>true</c> sticky.</param>
		public Info(string title, string text, bool sticky) : this(title, text) {
			Sticky = sticky;
		}
	}
}

