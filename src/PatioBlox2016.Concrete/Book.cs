namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;
	using System.Linq;

	public class Book
	{
		private Book()
		{
			Sections = new HashSet<Section>();
		}

		public Book(JobFile jobFile, string bookName) : this()
		{
			Id = -1;
			JobFile = jobFile;
			BookName = bookName;
		}

		public int Id { get; private set; }

		//public Job Job { get; set; }
		//public int JobId { get; private set; }

		public JobFile JobFile { get; set; }
		public int JobFileId { get; private set; }

		public string BookName { get; private set; }

		public ICollection<Section> Sections { get; set; }

		public bool HasDuplicateCells
		{
			get { return DuplicateCellGroups.Any(); }
		}

		public IEnumerable<IEnumerable<int>> DuplicateCellGroups
		{
			get
			{
				var cellGroups = (from section in Sections
					from page in section.Pages
					from cell in page.Cells
					select cell)
					.GroupBy(c => c, k => k, new CellDuplicateComparer());

				return cellGroups.Where(cg => cg.Count() > 1)
					.Select(doop => doop.Select(d => d.SourceRowIndex));
			}
		}

		public IEnumerable<string> DuplicateCellReports
		{
			get
			{
				// Flatten to IEnumerable<string>:
				var doopIndices = DuplicateCellGroups
					.Select(doop => string.Join(" and ", doop.Select(d => d.ToString())));

				// Format the strings for report output:
				return doopIndices
					.Select(doopIndex => string.Format("Book {0} has duplicates in rows {1}", BookName, doopIndex));
			}
		}

		public int PageCount
		{
			get
			{
				// Page count must always be an even number:
				var count = Sections.SelectMany(s => s.Pages).Count();
				return count % 2 == 0 ? count : count + 1;
			}
		}
	}
}