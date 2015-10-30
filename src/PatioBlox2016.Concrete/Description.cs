namespace PatioBlox2016.Concrete
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using System.Text.RegularExpressions;
  using PatioBlox2016.Abstract;

  public class Description : IDescription
	{
    private static readonly Regex SizeRegex = 
      new Regex(@"(\d+\.?\d*)-?(I-?N|SQ ?FT)?-? ?([Xx])? ?(H(?= ))? ?", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private Description() { }

		public Description(string text) : this()
    {
      if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

      Text = text;
      JobSources = new List<JobSource>();
    }

    public int Id { get; set; }
    public string Text { get; private set; }
    public string Vendor { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }
    public DateTime InsertDate { get; set; }
    public ICollection<JobSource> JobSources { get; set; }

    public bool IsUnresolved
    {
      get { return Vendor == null && Size == null && Color == null && Name == null; }
    }

    public string FullName
    {
      get
      {
        return string.IsNullOrWhiteSpace(Vendor)
          ? Name
          : string.Format("{0}|{1}", Vendor, Name);
      }
    }

    public static List<string> ExtractWordList(string descText)
    {
      return SizeRegex.Replace(descText, string.Empty)
        .Split(new[] { " ", "/" }, StringSplitOptions.RemoveEmptyEntries)
        .Select(w => w.ToUpper())
        .ToList();
    }

    public static string ExtractSize(string descriptionText)
    {
      var matchList = new List<string>();
      var matches = SizeRegex.Matches(descriptionText);

      for (var i = 0; i < matches.Count; i++)
      {
        matchList.Add(matches[i].Value.ToUpper().Trim(' ', 'X'));
      }

      return string.Join(" x ", matchList);
    }

    protected bool Equals(Description other)
    {
      return string.Equals(Text, other.Text);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Description) obj);
    }

    public override int GetHashCode()
    {
      return Text.GetHashCode();
    }

    public override string ToString()
    {
      return Text;
    }

    public string ToJsxString(int indentLevel)
    {
      var idLine = string.Format("{0} : {{ 'size' : '{1}', 'color' : '{2}', 'name' : '{3}' }}",
        Id, Size, Color, FullName);

      return idLine.Indent(indentLevel);
    }
  }
}