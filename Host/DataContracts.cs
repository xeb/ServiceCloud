using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Kockerbeck.ServiceCloud
{
	[DataContract]
	public class Request
	{
		[DataMember]
		public ServiceCall[] Services { get; set; }

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

	[DataContract]
	public class ServiceCall
	{
		[DataMember]
		public string Address { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public ServiceCall[] Services { get; set; }
	}
}