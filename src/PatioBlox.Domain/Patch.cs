namespace PatioBlox.Domain
{
	using System.Collections.Generic;
	using System.Linq;

	public class Patch
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public List<Section> Sections { get; set; }

		public bool SectionsEqual(Patch other)
		{
			if (Sections.Count != other.Sections.Count) return false;

			// If any sequence fails equality, the whole sequence fails
			return !Sections.Where((t, i) => !t.PatioBlocksEqual(other.Sections[i])).Any();
		}
	}
}