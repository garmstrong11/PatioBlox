namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;

	public class Book
	{
		protected Book()
		{
			Sections = new HashSet<Section>();
		}

		public Book(int jobId, string bookName)
		{
			Id = -1;
			JobId = jobId;
			BookName = bookName;
		}

		public int Id { get; private set; }

		public Job Job { get; set; }
		public int JobId { get; private set; }

		public string BookName { get; private set; }

		public ICollection<Section> Sections { get; set; }
	}
}