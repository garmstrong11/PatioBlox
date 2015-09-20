namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;
	using System.Linq;

	public class Book
	{
	  private readonly List<Section> _sections; 
    
    public Book(Job job, string bookName)
	  {
      Job = job;
      BookName = bookName;
      _sections = new List<Section>();
	  }

    public Job Job { get; set; }

		public string BookName { get; private set; }

	  public List<Section> Sections
	  {
	    get { return new List<Section>(_sections);}
	  }

	  public void AddSection(Section section)
	  {
	    _sections.Add(section);
	  }

	  public void AddSectionRange(IEnumerable<Section> sections)
	  {
	    _sections.AddRange(sections);
	  }

	  #region Duplicate checking

		public bool HasDuplicateCells
		{
			get { return DuplicateCellGroups.Any(); }
		}

		public IEnumerable<IEnumerable<int>> DuplicateCellGroups
		{
			get
			{
			  var cells = Sections.SelectMany(sec => sec.Pages.SelectMany(c => c.Cells));
        var cellGroups = cells.GroupBy(c => c);

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

#endregion

		public int GetPageCount(int cellsPerPage)
		{
			// Page count must always be an even number!
		  var count = Sections.Sum(sec => sec.PageCount);
			return count % 2 == 0 ? count : count + 1;
		}

	  public override string ToString()
	  {
	    return BookName;
	  }
	}
}