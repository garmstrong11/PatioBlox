namespace PatioBlox.Domain
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class Project
	{
		public Project()
		{
			Patches = new List<Patch>();
		}
		
		[DataMember]
		public IEnumerable<Patch> Patches { get; set; } 
	}
}