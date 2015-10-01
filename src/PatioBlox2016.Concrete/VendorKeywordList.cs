namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;
  using System.Linq;
  using PatioBlox2016.Abstract;

  public class VendorKeywordList : KeywordList
  {   
    public VendorKeywordList(IEnumerable<Keyword> keywords) : base(keywords) {}

    public override string ToTitleCasePhrase(string startPhrase = "")
    {
      if (!Keywords.Any()) return null;

      if (!string.IsNullOrWhiteSpace(startPhrase)) {
        Keywords.Insert(0, startPhrase);
      }

      var replDict = new Dictionary<string, string>
      {
        {"Countrystone", "Country Stone"},
        {"Pacificclay", "Pacific Clay"}
      };

      string word;
      var newList = Keywords.Select(k => replDict.TryGetValue(k, out word) ? word : k);

      return string.Join(" ", newList);
    }
  }
}