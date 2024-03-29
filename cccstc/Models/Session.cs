﻿using System;
using HumDrum.Collections;

namespace cccstc
{
	public class Session
	{

		/// <summary>
		/// The email of the worker who completed this sessions
		/// </summary>
		/// <value>The email of the worker</value>
		public string Email {get; set;}

		/// <summary>
		/// When the session started
		/// </summary>
		/// <value>The start of the session</value>
		public DateTime Start { get; set;}

		/// <summary>
		/// When the session ended
		/// </summary>
		/// <value>The end of the session</value>
		public DateTime End { get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="cccstc.Session"/> class.
		/// This uses two strings to initialize the Session, since that's how the information is stored in the database.
		/// </summary>
		/// <param name="email"> The email of this student worker</param>
		/// <param name="dateStarted">The datetime string of when a session started</param>
		/// <param name="dateEnded">The datetime string of when a session ended</param>
		public Session (string email, string dateStarted, string dateEnded)
		{
			Email = email;
			Start = Convert.ToDateTime (dateStarted);

			// Sessions must always have a starting time; however, the end time can be null if the user is still working
			End = Convert.ToDateTime(dateEnded);
		}

		/// <summary>
		/// Finds out the timespan between the start and end of the session
		/// </summary>
		/// <returns>The timespan between the start and end of this session</returns>
		public TimeSpan ElapsedTime()
		{
			return End.Subtract (Start);
		}

		/// <summary>
		/// Returns the total amount of money earned from this session
		/// </summary>
		/// <param name="wage">The amount of hourly money made</param>
		public double Earned(double wage)
		{
			return ElapsedTime ().TotalHours * wage;
		}

		/// <summary>
		/// Calculate the amount of money made for any given timespan
		/// </summary>
		/// <param name="timespan">How long the shift was</param>
		/// <param name="wage">How much money the worker makes in an hour</param>
		public static double Earned(TimeSpan timespan, double wage)
		{
			return timespan.TotalHours * wage;
		}

		/// <summary>
		/// Return the string month of a given integer month, starting with one
		/// </summary>
		/// <returns>The name of the integer month</returns>
		/// <param name="month">The integer value of the month (September = 1, December = 12)</param>
		public static string MonthOf(int month)
		{
			return Transformations.Zip (
				Transformations.Make (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12),
				Transformations.Make (
					"January",
					"February",
					"March",
					"April",
					"May",
					"June",
					"July",
					"August",
					"September",
					"October",
					"November", // This is gauraunteed to only return one item
					"December")).When (x => x.Item1.Equals (month)).Get (0).Item2;
		}
	}
}

