namespace AuthorizationServiceProject.Style
{
	/// <summary>
	/// A bunch of style and markup features using 'Spectre.Console' nuget.
	/// <br />
	/// Набор стилей и разметок, использующий нюгет "Spectre.Console".
	/// </summary>
	public static class ConsoleServiceStyle
    {

		public static string GetGreetings()
		{
			return $"\t[yellow on blue]Welcome to Authorizer service. Service online.[/]\n";
		}


		public static string GetCurrentTime()
		{
			return $"[black on white][[{DateTime.Now.ToString("HH:mm")}]][/]";
		}

    }
}
