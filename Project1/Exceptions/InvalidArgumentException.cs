using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
	public class InvalidArgumentException : Exception
	{
		public InvalidArgumentException(string message) : base(message) { }
	}
}