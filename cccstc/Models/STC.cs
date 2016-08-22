using System;

namespace cccstc
{
	public class STC
	{
		/// <summary>
		/// The preferred name of this worker
		/// </summary>
		/// <value>The name of the worker</value>
		public string Name { get; set; }

		/// <summary>
		/// The email of this worker
		/// </summary>
		/// <value>The email of the given worker</value>
		public string Email {get; set;}

		public STC (string name, string email)
		{
			Name = name;
			Email = email;
		}
	}
}

