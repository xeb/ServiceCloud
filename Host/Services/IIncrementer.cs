using System;
using System.Linq;
using System.ServiceModel;

namespace ServiceCloud.Services
{
	[ServiceContract]
	public interface IIncrementer
	{
		[OperationContract]
		int Increment(int i);
	}
}
