namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;

	public class Page
  {
		private Page()
		{
			Cells = new List<Cell>();
		}

		public Page(Section section, int index) : this()
		{
			Id = -1;
			Index = index;
			Section = section;
		}

		public int Id { get; private set; }
	  public int Index { get; set; }

	  public Section Section { get; set; }
		public int SectionId { get; set; }

	  public ICollection<Cell> Cells { get; set; }
  }
}