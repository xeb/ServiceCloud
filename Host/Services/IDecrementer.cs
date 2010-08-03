using System;
using System.Linq;
using System.ServiceModel;

namespace ServiceCloud.Services
{
	[ServiceContract]
	public interface IDecrementer
	{
		[OperationContract]
		int Decrement(int i);
	}
}