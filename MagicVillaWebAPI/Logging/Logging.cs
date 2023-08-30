﻿namespace MagicVillaWebAPI.Logging
{
	public class Logging : Ilogging
	{
		public void Log(string message, string type)
		{
			if (type == "error")
			{
                Console.WriteLine("ERROR" + message);
            }
			else
			{
				Console.WriteLine(message);
			}
		}
	}
}
