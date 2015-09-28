namespace PatioBlox2016.Concrete
{
  using System;
  using System.Linq;
  using Abstract;
  using System.Collections.Generic;
  using System.Text.RegularExpressions;

  public class Description : IDescription
	{
    private static readonly Regex SizeRegex = 
      new Regex(@"(\d+\.?\d*)-?(I-?N|SQ ?FT)?-? ?([Xx])? ?(H(?= ))? ?", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private Description() { }

		public Description(string text) : this()
    {
      if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

		  Id = 0;
      Text = text;
		  Size = ExtractSize();
    }

    public int Id { get; private set; }
    public string Text { get; private set; }
    public string Vendor { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }
    public DateTime InsertDate { get; set; }

    public List<string> WordList
    {
      get { return ExtractWordList(Text); }
    }

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

    /// <summary>
    /// Removes the Size component from the argument and returns the remainder.
    /// </summary>
    /// <param name="text">The input string from which to remove size data.</param>
    /// <returns></returns>
    private static string ExtractRemainder(string text)
    {
      return SizeRegex.Replace(text, string.Empty);
    }

    public static List<string> ExtractWordList(string descText)
    {
      return ExtractRemainder(descText)
        .Split(new[] { " ", "/" }, StringSplitOptions.RemoveEmptyEntries)
        .Select(w => w.ToUpper())
        .ToList();
    }

    private string ExtractSize()
    {
      var matchList = new List<string>();
      var matches = SizeRegex.Matches(Text);
      var square = Text.Contains("SQUARE") ? " Square" : "";

      for (var i = 0; i < matches.Count; i++) {
        matchList.Add(matches[i].Value.ToUpper().Trim(' ', 'X'));
      }

      return string.Format("{0}{1}", string.Join(" x ", matchList), square);
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
      var idLine = string.Format("'{0}' : {{ 'size' : '{1}', 'color' : '{2}', 'name' : '{3}' }}",
        Id, Size, Color, FullName);

      return idLine.Indent(indentLevel);
    }
  }
}