namespace PatioBlox.Domain
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract(IsReference = true)]
	public class Page
	{
		[DataMember]
		public Section Section { get; set; }

		[DataMember]
		public IEnumerable<Product> Products { get; set; }
	}
}