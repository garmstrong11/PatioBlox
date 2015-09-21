namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text.RegularExpressions;
  using Abstract;
  using Concrete;

  public class SectionFactory : ISectionFactory
  { 
    private readonly IPageFactory _pageFactory;

    public SectionFactory(IPageFactory pageFactory)
    {
      _pageFactory = pageFactory;
    }
    
    public IEnumerable<Section> CreateSections(IEnumerable<IPatchRowExtract> patchRows, Book book)
    {
      var sections = new HashSet<Section>(new SectionNameEqualityComparer());
      var extracts = patchRows.ToList();

      var sectionExtracts = extracts
        .Where(pr => !string.IsNullOrWhiteSpace(pr.Section))
        .Where(pr => !Regex.IsMatch(pr.Section, @"^Page ?\d+$"));

      // Add to HashSet to eliminate duplicates by section name...
      foreach (var sectionExtract in sectionExtracts) {
        sections.Add(new Section(book, sectionExtract.Section, sectionExtract.RowIndex));
      }

      var sectionDict = GetExtractsForSections(sections.AsEnumerable(), extracts);

      foreach (var sectionKey in sectionDict.Keys) {
        var pages = _pageFactory.CreatePages(sectionKey, sectionDict[sectionKey]);
        sectionKey.AddPageRange(pages);
      }

      return sectionDict.Keys
        .OrderBy(sk => sk.SourceRowIndex)
        .AsEnumerable();
    }

    private static IDictionary<Section, List<IPatchRowExtract>> GetExtractsForSections(IEnumerable<Section> sections, 
      IEnumerable<IPatchRowExtract> patchRows)
    {
      var sects = sections.ToList();
      var extractDict = sects.ToDictionary(section => section, section => new List<IPatchRowExtract>());
      var extracts = patchRows.Where(pr => pr.Sku >= 0);

      foreach (var extract in extracts) {
        var section = sects.LastOrDefault(s => s.SourceRowIndex <= extract.RowIndex);

        if (section == null){
          throw new InvalidOperationException("Can't find a section for cell.");
        }

        var list = extractDict[section];
        list.Add(extract);
      }

      return extractDict; 
    } 
  }
}