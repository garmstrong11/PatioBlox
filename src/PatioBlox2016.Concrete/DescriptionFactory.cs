namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.Linq;
  using Abstract;

  public class DescriptionFactory : IDescriptionFactory
  {
    private readonly IDictionary<string, string> _expansionDict;
    private readonly IList<string> _colorKeywords;
    private readonly IEnumerable<string> _vendorKeywords;
    private readonly IEnumerable<string> _nameKeywords; 

    public DescriptionFactory(IRepository<Keyword> keywordRepo, IRepository<Expansion> expansionRepo)
    {
      var keywords = keywordRepo.GetAll();

      _colorKeywords = keywords
        .Where(k => k.WordType == WordType.Color)
        .Select(w => w.Word)
        .ToList();

      _vendorKeywords = keywords
        .Where(k => k.WordType == WordType.Vendor)
        .Select(w => w.Word)
        .ToList();

      _nameKeywords = keywords
        .Where(k => k.WordType == WordType.Name)
        .Select(w => w.Word)
        .ToList();

      _expansionDict = expansionRepo.GetAll()
        .ToDictionary(k => k.Word, v => v.Keyword.Word);
    }
    
    public IDescription CreateDescription(string descriptionText)
    {
      var description = new Description(descriptionText) {InsertDate = DateTime.Now};
      var remainderList = description.ExtractRemainder()
        .Split(new[] {" ", "/"}, StringSplitOptions.RemoveEmptyEntries);

      string expansion;
      var remainder = remainderList
        .Select(item => _expansionDict.TryGetValue(item, out expansion) ? expansion : item)
        .ToList();

      description.Color = AssembleTitleCasePhrase("/", remainder.Intersect(_colorKeywords));
      description.Vendor = AssembleTitleCasePhrase(" ", remainder.Intersect(_vendorKeywords));
      description.Name = AssembleTitleCasePhrase(" ", remainder.Intersect(_nameKeywords));

      return description;
    }

    private static string AssembleTitleCasePhrase(string joiner, IEnumerable<string> phrase)
    {
      var lowered = phrase.Select(p => p.ToLower());
      var cased = lowered.Select(p => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p));

      return string.Join(joiner, cased);
    }
  }
}