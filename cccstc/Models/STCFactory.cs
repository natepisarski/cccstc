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
		/// Writes the given session to the database
		/// </summary>
		/// <param name="session">The session you would like to write</param>
		/// <param name="database">The path to the database file</param>
		public static void WriteSession(Session session, string database)
		{
			ExecuteSQL(String.Format("INSERT INTO sessions VALUES('{0}', '{1}', '{2}');",
				session.Email,
				session.Start.ToString(),
				session.End.ToString()
			), database);
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
		/// Returns the sessions which are open, i.e, those with no ending field
		/// </summary>
		/// <returns>The sessions that have not ended yet</returns>
		/// <param name="database">The path to the database file</param>
		public static IEnumerable<Session> OpenSessions(string database)
		{
			var reader = ExecuteSQL ("SELECT * FROM sessions WHERE end = NULL;", database);

			while (reader.Read ())
				yield return new Session (reader.GetString (0), reader.GetString (1), "");

			yield break;
		}

		/// <summary>
		/// Gets the sessions that have ended
		/// </summary>
		/// <returns>The sessions which have an end field in the database</returns>
		/// <param name="database">The database file to point to</param>
		public static IEnumerable<Session> ClosedSessions(string database)
		{
			var reader = ExecuteSQL ("SELECT * FROM sessions WHERE end != NULL;", database);

			while (reader.Read ())
				yield return new Session (reader.GetString (0), reader.GetString (1), reader.GetString (2));

			yield break;
		}

		/// <summary>
		/// This will provide some information
		/// </summary>
		/// <param name="database">The path to the database file</param>
		public static IEnumerable<Info> Information(string database)
		{
			var reader = ExecuteSQL ("SELECT * FROM information;", database);

			while (reader.Read ())
				yield return new Info (reader.GetString (0), reader.GetString (1));

			yield break;
		}

		/// <summary>
		/// This will write some information to the database
		/// </summary>
		/// <param name="title">The title of the informational post</param>
		/// <param name="text">The text of the informational post</param>
		/// <param name="database">The path to the database file</param>
		public static void WriteInformation(string title, string text, int sticky, string database)
		{
			ExecuteSQL(String.Format("INSERT INTO information VALUES('{0}', '{1}', '{2}');", title, text, sticky), database);
		}

		/// <summary>
		/// Writes this information object to the database
		/// </summary>
		/// <param name="info">The information object to add</param>
		public static void WriteInformation(Info info, string database)
		{
			WriteInformation (info.Title, info.Text, info.Sticky ? 1 : 0, database);
		}

		/// <summary>
		/// Writes a given user to the database
		/// </summary>
		/// <param name="name">The first and last name of the worker</param>
		/// <param name="email">The email of the user</param>
		public static void WriteWorker(string name, string email, string database)
		{
			ExecuteSQL(String.Format("INSERT INTO workers VALUES('{0}', '{1}');",  email, name), database);
		}

		/// <summary>
		/// Writes the worker to the database
		/// </summary>
		/// <param name="worker">The worker to add</param>
		public static void WriteWorker(STC worker, string database)
		{
			WriteWorker (worker.Name, worker.Email, database);
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

