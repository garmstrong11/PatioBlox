namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;

	public class Section
  {
		private Section()
		{
			Pages = new List<Page>();
		}

	  public Section(int bookId) : this()
	  {
		  Id = -1;
			BookId = bookId;
	  }

		public int Id { get; private set; }
		public string Name { get; set; }

		public Book Book { get; set; }
		public int BookId { get; private set; }

		public ICollection<Page> Pages { get; set; } 
  }
}