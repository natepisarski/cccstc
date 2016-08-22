using System;
using System.Collections.Generic;

using Mono.Data.Sqlite;

namespace cccstc
{
	public static class STCFactory
	{
		/// <summary>
		/// Gets all of the workers from the database.
		/// <param name="database">The path to the database file</param>
		/// </summary>
		public static IEnumerable<STC> Workers(string database)
		{
			var reader = ExecuteSQL ("SELECT * FROM workers;", database);

			while (reader.Read ())
				yield return new STC (reader.GetString (1), reader.GetString (0));

			yield break;
		}

		/// <summary>
		/// Returns the worker from the database
		/// </summary>
		/// <returns>The worker</returns>
		/// <param name="name">The full name of the worker to retrieve</param>
		/// <param name="database">The path to the database</param>
		public static STC GetWorker(string email, string database)
		{
			string name = ExecuteSQL ("SELECT name FROM workers WHERE email = " + email + ";", database).GetString(1);

			return new STC (name, email);
		}

		/// <summary>
		/// Gets the sessions of the worker found in between a beginning and ending date.
		/// </summary>
		/// <returns>The sessions reported between the beginning and ending dates</returns>
		/// <param name="email">The email of the student worker to return the sessions of</param>
		/// <param name="beginning">The beginning of time to search for sessions</param>
		/// <param name="end">The end of time to search for sessions in</param>
		/// <param name="database">The path to the database file</param>
		public static IEnumerable<Session> SessionsOf(string email, DateTime beginning, DateTime end, string database)
		{
			List<Session> sessions = new List<Session> ();

			SqliteDataReader reader = ExecuteSQL ("SELECT * FROM sessions;", database);

			while (reader.Read ())
				sessions.Add (new Session (reader.GetString (0), reader.GetString (1), reader.GetString (2)));

			// Filter the results based on beginning and end
			foreach (Session session in sessions)
				if (session.Start.CompareTo (beginning) >= 0 && session.Start.CompareTo (end) <= 0)
					yield return session;

			yield break;
		}

		/// <summary>
		/// Returns all of the sessions logged in the database
		/// <param name="database">The database file</param>
		/// </summary>
		public static IEnumerable<Session> Sessions(string database)
		{
			var reader = ExecuteSQL ("SELECT * FROM sessions;", database);

			while (reader.Read ())
				yield return new Session (reader.GetString (0), reader.GetString (1), reader.GetString (2));

			yield break;
		}

		/// <summary>
		/// Executes an SQL command on the database
		/// </summary>
		/// <returns>The reader that is returned by running this SQL command</returns>
		/// <param name="sql">The SQL to run on the database</param>
		/// <param name="database">The path to the databse</param>
		public static SqliteDataReader ExecuteSQL(string sql, string database)
		{
			SqliteConnection db = new SqliteConnection ("Data Source="+database+";Version=3;");
			db.Open ();

			SqliteDataReader reader = new SqliteCommand (sql, db).ExecuteReader ();

			return reader;	
		}


	}
}

