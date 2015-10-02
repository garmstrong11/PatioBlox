namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Caliburn.Micro;
  using Concrete;

  public class DescriptionViewModel : PropertyChangedBase
  {
    private readonly Description _description;
    private readonly IDictionary<string, Keyword> _keywordDict;

    public DescriptionViewModel(Description description, IDictionary<string, Keyword> keywordDict)
    {
      _description = description;
      _keywordDict = keywordDict;
    }

    public int Id
    {
      get { return _description.Id; }
    }

    public string Text
    {
      get { return _description.Text; }
    }

    public string Vendor
    {
      get { return _description.Vendor; }
      set
      {
        if (value == _description.Vendor) return;
        _description.Vendor = value;
        NotifyOfPropertyChange(() => Vendor);
      }
    }

    public Description Description
    {
      get { return _description; }
    }

    public string Size
    {
      get { return _description.Size; }
      set
      {
        if (value == _description.Size) return;
        _description.Size = value;
        NotifyOfPropertyChange(() => Size);
      }
    }

    public string Color
    {
      get { return _description.Color; }
      set
      {
        if (value == _description.Color) return;
        _description.Color = value;
        NotifyOfPropertyChange(() => Color);
      }
    }

    public string Name
    {
      get { return _description.Name; }
      set
      {
        if (value == _description.Name) return;
        _description.Name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

    public DateTime InsertDate
    {
      get { return _description.InsertDate; }
    }

    public void Resolve()
    {
      Keyword keyword;
      var nameRoot = _keywordDict[Keyword.NameKey];

      // If a remainder word is not found in _keywordDict,
      // make a new keyword for it assigned to WordType.Name
      var wordLookup = Description.ExtractWordList(Text)
        .Select(k => _keywordDict.TryGetValue(k, out keyword)
          ? keyword
          : new Keyword(k) {Parent = nameRoot})
        .ToLookup(k => k.RootWord);

      var colorList = new ColorKeywordList(wordLookup[Keyword.ColorKey]);
      var sizeList = new KeywordList(wordLookup[Keyword.SizeKey]);
      var vendorList = new VendorKeywordList(wordLookup[Keyword.VendorKey]);

      // Combine Name keywords and New keywords. There should not be any keywords labeled 
      // 'new' but if there are, combine them with name words so they will not be skipped.
      var nameWords = wordLookup[Keyword.NameKey].Concat(wordLookup[Keyword.NewKey]);
      var nameList = new KeywordList(nameWords);

      // Segment the keywords according to their RootWords:
      Color = colorList.ToTitleCasePhrase();
      Vendor = vendorList.ToTitleCasePhrase();
      Name = nameList.ToTitleCasePhrase();
      Size = sizeList.ToTitleCasePhrase(Description.ExtractSize(Text));
    }
  }
}