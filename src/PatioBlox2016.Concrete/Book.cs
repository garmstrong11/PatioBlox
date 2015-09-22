namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

  public class Book
	{
	  private static readonly StringBuilder Sb = new StringBuilder();
    private readonly List<Section> _sections; 
    
    public Book(Job job, string bookName)
	  {
      Job = job;
      BookName = bookName;
      _sections = new List<Section>();
	  }

    public Job Job { get; set; }

		public string BookName { get; private set; }

	  public IReadOnlyCollection<Section> Sections
	  {
	    get { return _sections.AsReadOnly();}
	  }

	  public void AddSection(Section section)
	  {
	    _sections.Add(section);
	  }

	  public void AddSectionRange(IEnumerable<Section> sections)
	  {
	    _sections.AddRange(sections);
	  }

	  public void RemoveSection(Section section)
	  {
	    _sections.Remove(section);
	  }

    protected bool Equals(Book other)
    {
      return string.Equals(BookName, other.BookName) 
        && Sections.SequenceEqual(other.Sections);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Book) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (BookName.GetHashCode() * 397) ^ Sections.GetHashCode();
      }
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
			  var cells = Sections.SelectMany(s => s.Cells);
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

		public int PageCount
		{
		  get
		  {
        // Page count must always be an even number!
        var count = Sections.Sum(sec => sec.PageCount);
        return count % 2 == 0 ? count : count + 1;
		  }
		}

	  public override string ToString()
	  {
	    return BookName;
	  }

	  public string ToJsxString(int indentLevel)
	  {
	    var contentLevel = indentLevel + 1;
	    var childLevel = indentLevel + 2;
	    Sb.Clear();

	    var bookName = string.Format("'{0}' : {{", BookName).Indent(indentLevel);
	    Sb.AppendLine(bookName);

	    var nameProp = string.Format("'name' : '{0}',", BookName).Indent(contentLevel);
	    Sb.AppendLine(nameProp);

	    var pagesLine = "'pages' : [".Indent(contentLevel);
	    Sb.AppendLine(pagesLine);

	    var pageStrings = Sections
        .SelectMany(s => s.Pages)
        .Select(p => p.ToJsxString(childLevel));

	    var joinedPages = string.Join(",\n", pageStrings);
	    Sb.AppendLine(joinedPages);

	    Sb.AppendLine("]".Indent(contentLevel));
	    Sb.Append("}".Indent(indentLevel));

	    return Sb.ToString();
	  }
	}
}