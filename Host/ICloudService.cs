using System;
using System.Linq;

namespace ServiceCloud
{
	public interface ICloudService
	{
		void Execute(Request request);
	}
}
