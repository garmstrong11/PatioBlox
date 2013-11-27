namespace PatioBlox.Domain
{
	using System.Collections.Generic;
	using System.Linq;

	public class Patch
	{
		private const int BlocksPerPage = 4;

		public Patch()
		{
			Sections = new List<Section>();
		}
		
		public int Id { get; set; }
		public string Name { get; set; }
		public int StoreCount { get; set; }

		public List<Section> Sections { get; set; }

		public int PageCount
		{
			get
			{
				var count = 0;

				foreach (var section in Sections) {
					var blockCount = section.PatioBlocks.Count;
					var pgCount = blockCount / BlocksPerPage;
					pgCount += blockCount % BlocksPerPage != 0 ? 1 : 0;
					count += pgCount;
				}
				
				return count % 2 == 0 ? count : count + 1;
			}
		}

		public bool SectionsEqual(Patch other)
		{
			if (Sections.Count != other.Sections.Count) return false;

			// If any sequence fails equality, the whole sequence fails
			return !Sections.Where((t, i) => !t.PatioBlocksEqual(other.Sections[i])).Any();
		}

		public override bool Equals(object obj)
		{
			var other = obj as Patch;
			if (other == null) return false;

			return SectionsEqual(other) && Id == other.Id && Name == other.Name;
		}
	}
}