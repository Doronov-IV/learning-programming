namespace AuthorizationServiceProject.Style
{
	/// <summary>
	/// A bunch of style and markup features using 'Spectre.Console' nuget.
	/// <br />
	/// Набор стилей и разметок, использующий нюгет "Spectre.Console".
	/// </summary>
	public static class ConsoleServiceStyle
    {


		/// <summary>
		/// Get the spectre markup string containing greeting.
		/// <br />
		/// Получить строку типа Spectre.Markup, содержащую приветствие.
		/// </summary>
		public static string GetGreetings()
		{
			return $"\t[yellow on blue]Welcome to Authorizer service. Service online.[/]\n";
		}



		/// <summary>
		/// Get the spectre markup string containing current time in format of 'HH:mm:ss'.
		/// <br />
		/// Получить строку типа Spectre.Markup, содержащую текущее время в формате "HH:mm:ss".
		/// </summary>
		public static string GetCurrentTime()
		{
			return $"[black on white][[{DateTime.Now.ToString("HH:mm:ss")}]][/]";
		}



		/// <summary>
		/// Get the spectre markup string containing user connection message.
		/// <br />
		/// Получить строку типа Spectre.Markup, содержащую сообщение о подключении пользователя.
		/// </summary>
		public static string GetUserConnection(string userLogin)
		{
			return $"{ConsoleServiceStyle.GetCurrentTime()} user has [underline]connected[/] with login [green]\"{userLogin}\"[/].\n";
        }



        /// <summary>
        /// Get the spectre markup string containing user registration and connection message.
        /// <br />
        /// Получить строку типа Spectre.Markup, содержащую сообщение о регистрации и подключении пользователя.
        /// </summary>
        public static string GetUserRegistration(string userLogin)
        {
            return $"{ConsoleServiceStyle.GetCurrentTime()} user has [underline]registrated and connected[/] with login [green]\"{userLogin}\"[/].\n";
        }



        /// <summary>
        /// Get the spectre markup string containing user disconnection message.
        /// <br />
        /// Получить строку типа Spectre.Markup, содержащую сообщение об дисконнекте пользователя.
        /// </summary>
        public static string GetUserDisconnection(string userLogin)
		{
			return $"{ConsoleServiceStyle.GetCurrentTime()} user [green]\"{userLogin}\"[/] has [underline]disconnected[/].\n";
		}


    }
}
