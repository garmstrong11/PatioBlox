namespace PatioBlox.Domain
{
	using System.Collections.Generic;

	public class Patch
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public List<Section> Sections { get; set; }
	}
}