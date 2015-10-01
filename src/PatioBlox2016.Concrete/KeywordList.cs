namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using PatioBlox2016.Abstract;

  public class KeywordList
  {
    protected readonly List<string> Keywords;

    public KeywordList(IEnumerable<Keyword> keywords)
    {
      var lowered = keywords.Select(p => p.Expansion.ToLower());
      Keywords = lowered.Select(p => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p)).ToList();
    }

    public virtual string ToTitleCasePhrase(string startPhrase = "")
    {
      if (!Keywords.Any() && string.IsNullOrWhiteSpace(startPhrase)) {
        return null;
      }

      if (!string.IsNullOrWhiteSpace(startPhrase)) {
        Keywords.Insert(0, startPhrase);
      }

      return string.Join(" ", Keywords);
    }
  }
}