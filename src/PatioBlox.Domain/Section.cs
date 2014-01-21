namespace PatioBlox.Domain
{
	using System.Collections.Generic;
	using System.Linq;

	public class Section
	{
		public Section()
		{
			PatioBlocks = new List<PatioBlock>();
		}

		public int Id { get; set; }
		public string Name { get; set; }
		public int Index { get; set; }
		public int RowNumber { get; set; }
		public List<PatioBlock> PatioBlocks { get; set; }

		public Patch Patch { get; set; }
		public int PatchId { get; set; }

		public bool PatioBlocksEqual(Section other)
		{
			return PatioBlocks.SequenceEqual(other.PatioBlocks);
		}
	}
}