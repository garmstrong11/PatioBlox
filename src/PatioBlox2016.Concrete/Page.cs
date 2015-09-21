namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using System.Text;

  public class Page
  {
    private readonly List<Cell> _cells;
    private static readonly TextInfo TextInfo = new CultureInfo("en-US", false).TextInfo;

    public Page(Section section, IEnumerable<Cell> cells)
    {
      Section = section;
      _cells = new List<Cell>(cells);
    }
    
    public Section Section { get; private set; }

    public IReadOnlyList<Cell> Cells { 
      get { return _cells.AsReadOnly(); }
    }

    public void AddCellRange(IEnumerable<Cell> cells)
    {
      _cells.AddRange(cells);
    }

    public void AddCell(Cell cell)
    {
      _cells.Add(cell);
    }

    public string Header
    {
      get
      {
        var lowered = TextInfo.ToLower(Section.SectionName);
        return TextInfo.ToTitleCase(lowered);
      }
    }

    protected bool Equals(Page other)
    {
      return Section.Equals(other.Section) && Cells.SequenceEqual(other.Cells);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Page) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (Section.GetHashCode() * 397) ^ Cells.GetHashCode();
      }
    }

    public string ToJsxString(int indentLevel)
    {
      var sb = new StringBuilder();
      var contentLevel = indentLevel + 1;
      var cellLevel = indentLevel + 2;

      sb.AppendLine("{".Indent(indentLevel));
      var header = string.Format("'header' : '{0}',", Header).Indent(contentLevel);
      sb.AppendLine(header);
      sb.AppendLine("'blocks' : [".Indent(contentLevel));

      var cellStrings = Cells.Select(c => c.ToJsxString(cellLevel));
      var joinedCells = string.Join(",\n", cellStrings);
      sb.Append(joinedCells);
      sb.AppendLine();
      sb.AppendLine("]".Indent(contentLevel));
      sb.Append("}".Indent(indentLevel));

      return sb.ToString();
    }
  }
}