namespace PatioBlox2016.Concrete
{
  using System;
  using System.Linq;
  using Abstract;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Text.RegularExpressions;

  public class Description
	{
    private static readonly Regex SizeRegex = 
      new Regex(@"(\d+\.?\d*)-?(I-?N|SQ ?FT)?-? ?([Xx])? ?(H(?= ))? ?", 
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private Description() { }

		public Description(string text) : this()
    {
      if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

      Text = text;
    }

    public int Id { get; set; }
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

    public void Resolve(IDictionary<string, Keyword> keywordDict)
    {
      Keyword keyword;
      var nameRoot = keywordDict[Keyword.NameKey];

      // If a remainder word is not found in _keywordDict,
      // make a new keyword for it assigned to WordType.Name
      var wordList = ExtractWordList(Text)
        .Select(k => keywordDict.TryGetValue(k, out keyword) 
          ? keyword 
          : new Keyword(k) {Parent = nameRoot})
        .ToList();

      var colorList = new ColorKeywordList(wordList.Where(w => w.RootWord == Keyword.ColorKey));
      var sizeList = new KeywordList(wordList.Where(w => w.RootWord == Keyword.SizeKey));
      var vendorList = new VendorKeywordList(wordList.Where(w => w.RootWord == Keyword.VendorKey));

      // Combine Name keywords and New keywords There should not be any keywords labeled 
      // 'new' but if there are, combine them with name words so they will not be skipped.
      var nameWords = wordList.Where(w => w.RootWord == Keyword.NameKey || w.RootWord == Keyword.NewKey);
      var nameList = new KeywordList(nameWords);

      // Segment the keywords according to their RootWords:
      Color = colorList.ToTitleCasePhrase();
      Vendor = vendorList.ToTitleCasePhrase();
      Name = nameList.ToTitleCasePhrase();
      Size = sizeList.ToTitleCasePhrase(ExtractSize());
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
    public static string ExtractRemainder(string text)
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

      for (var i = 0; i < matches.Count; i++) {
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
      var idLine = string.Format("'{0}' : {{ 'size' : '{1}', 'color' : '{2}', 'name' : '{3}' }}",
        Id, Size, Color, FullName);

      return idLine.Indent(indentLevel);
    }
  }
}