using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ServiceCloud
{
	[ServiceContract]
	public interface IGateway
	{
		[OperationContract]
		Response Execute(Request request);
	}

	[DataContract]
	public class Request
	{
		[DataMember]
		public string[] Services { get; set; }

		[DataMember]
		public object Argument { get; set; }
	}

	[DataContract]
	public class Response
	{
		[DataMember]
		public object ReturnObject { get; set; }

		[DataMember]
		public string[] ServicesRan { get; set; }
	}
}