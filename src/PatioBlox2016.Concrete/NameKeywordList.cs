namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;
  using System.Linq;

  public class NameKeywordList : KeywordList
  {
    public NameKeywordList(IEnumerable<Keyword> keywords) : base(keywords) {}

    public override string ToTitleCasePhrase(string startPhrase = "")
    {
      if (!Keywords.Any()) return null;

      if (!string.IsNullOrWhiteSpace(startPhrase))
      {
        Keywords.Insert(0, startPhrase);
      }

      var replDict = new Dictionary<string, string>
      {
        {"Countrymanor", "Country Manor"}
      };

      string word;
      var newList = Keywords.Select(k => replDict.TryGetValue(k, out word) ? word : k);

      return string.Join(" ", newList);
    }
  }
}