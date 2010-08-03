using System;
using System.Linq;

namespace ServiceCloud.Services
{
	public class Incrementer : IIncrementer, ICloudService
	{
		public void Execute(Request request)
		{
			if (request.Argument == null) return;
			if (request.Argument.GetType() != typeof(int)) return;

			request.Argument = Increment((int)request.Argument);
		}

		public int Increment(int i)
		{
			return i + 1;
		}
	}
}
