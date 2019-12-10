using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("\n\nType a number:");
				Convertor.Convert(Console.ReadLine());

				Console.Write("\nContinue? [Esc] to quit, [Enter] to continue: ");
				if (Console.ReadKey().Key == ConsoleKey.Escape) break;
			}
		}
	}
}
