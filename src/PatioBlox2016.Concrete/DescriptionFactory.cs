namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using Abstract;

  public class DescriptionFactory : IDescriptionFactory
  {
    private readonly Dictionary<string, Keyword> _keywordDict; 

    public DescriptionFactory(IRepository<Keyword> keywordRepo)
    {
      _keywordDict = keywordRepo.GetAll().ToDictionary(k => k.Word);
    }

    public IDescription CreateDescription(string descriptionText)
    {
      var description = new Description(descriptionText) { InsertDate = DateTime.Now };

      var remainderList = description.ExtractRemainder()
        .Split(new[] { " ", "/" }, StringSplitOptions.RemoveEmptyEntries)
        .Select(w => w.ToUpper())
        .ToList();

      description.WordList = new List<string>(remainderList);
      var nameRoot = _keywordDict["NAME"];

      Keyword keyword;

      // If the remainder word is not found in _keywordDict,
      // make a new keyword for it assigned to WordType.Name
      var keywordList = remainderList
        .Select(k => _keywordDict.TryGetValue(k, out keyword) 
          ? keyword 
          : new Keyword(k) {Parent = nameRoot})
        .ToList();

      // Segment the keywords according to their WordType
      description.Color = AssembleTitleCasePhrase("/", keywordList, WordType.Color);
      description.Vendor = AssembleTitleCasePhrase(" ", keywordList, WordType.Vendor);
      description.Name = AssembleTitleCasePhrase(" ", keywordList, WordType.Name);

      return description;
    }

    private static string AssembleTitleCasePhrase(string joiner, List<Keyword> keywords, WordType wordType)
    {
      if (!keywords.Any()) return null;

      var lowered = keywords.Where(k => k.WordType == wordType).Select(p => p.Expansion.ToLower());
      var cased = lowered.Select(p => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p));

      var joined = string.Join(joiner, cased);

      if (wordType == WordType.Color) {
        joined = joined.Replace("/Blend", " Blend");
        joined = joined.Replace("/Hill", " Hill");
      }

      return joined;
    }
  }
}