using System;
using System.Linq;

namespace ServiceCloud.Services
{
	public class Decrementer : IDecrementer, ICloudService
	{
		public int Decrement(int i)
		{
			return i-1;
		}

		public void Execute(Request request)
		{
			if (request.Argument == null) return;
			if (request.Argument.GetType() != typeof(int)) return;

			request.Argument = Decrement((int)request.Argument);
		}
	}
}