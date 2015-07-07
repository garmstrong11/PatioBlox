namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;

	public class Page
  {
		private Page() { }

		public Page(int index)
		{
			Index = index;
		}

		public int Id { get; set; }
	  public int Index { get; set; }

	  public Section Section { get; set; }
		public int SectionId { get; set; }

	  public ICollection<Cell> Cells { get; set; }
  }
}