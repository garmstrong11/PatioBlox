namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  public class ColorKeywordList : KeywordList
  {
    private static readonly StringBuilder Sb = new StringBuilder();

    public ColorKeywordList(IEnumerable<Keyword> keywords) : base(keywords) {}

    public override string ToTitleCasePhrase(string startPhrase = "")
    {
      if (!Keywords.Any()) return null;

      Sb.Clear();
      Sb.Append(startPhrase);
      var count = Keywords.Count;

      for (var i = 0; i < count; i++) {
        var keyword = Keywords[i];
        switch (Keywords[i]) {
          case "Blend" :
          case "Hill" :
            if (i == 0) { Sb.Append(keyword); }
            else { Sb.AppendFormat(" {0}", keyword); }
            break;
          default :
            if (i == 0) { Sb.Append(keyword); }
            else { Sb.AppendFormat("/{0}", keyword); }
            break;
        }
      }

      return Sb.ToString();
    }
  }
}