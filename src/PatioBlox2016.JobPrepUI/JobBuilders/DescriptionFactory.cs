namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using PatioBlox2016.Concrete;
  using PatioBlox2016.Services.Contracts;

  public class DescriptionFactory : IDescriptionFactory
  {
    private readonly IKeywordRepository _keywordRepo;
    private Dictionary<string, Keyword> _keywordDict; 

    public DescriptionFactory(IKeywordRepository keywordRepo)
    {
      _keywordRepo = keywordRepo;
      _keywordDict = new Dictionary<string, Keyword>();
    }

    public Description CreateDescription(string descriptionText)
    {
      var description = new Description(descriptionText) { InsertDate = DateTime.Now };
      var nameRoot = _keywordDict["NAME"];
      Keyword keyword;

      // If the remainder word is not found in _keywordDict,
      // make a new keyword for it assigned to WordType.Name
      var keywordList = description.WordList
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

    public void UpdateKeywordDict()
    {
      _keywordDict = _keywordRepo.GetKeywordDictionary();
    }

    private static string AssembleTitleCasePhrase(string joiner, List<Keyword> keywords, WordType wordType)
    {
      if (!keywords.Any()) return null;

      //var lowered = keywords.Where(k => k.WordType == wordType).Select(p => p.Expansion.ToLower());
      var lowered = keywords.Select(p => p.Expansion.ToLower());
      var cased = lowered.Select(p => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p));

      var joined = string.Join(joiner, cased);

      if (wordType != WordType.Color) return joined;

      joined = joined.Replace("/Blend", " Blend");
      joined = joined.Replace("/Hill", " Hill");

      return joined;
    }
  }
}