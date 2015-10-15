namespace PatioBlox2016.Reporter
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Threading.Tasks;
  using Abstract;
  using FlexCel.Report;

  public class FlexCelReporter : IReporter
  {
    private readonly FlexCelReport _report;
    private readonly IJob _job;
    private readonly IAdvertisingPatchExtractor _adPatchExtractor;
    private readonly List<IPatchReportDto> _items;

    public FlexCelReporter(IJob job, IAdvertisingPatchExtractor adPatchExtractor)
    {
      if (job == null) throw new ArgumentNullException("job");
      if (adPatchExtractor == null) throw new ArgumentNullException("adPatchExtractor");

      _job = job;
      _adPatchExtractor = adPatchExtractor;

      _report = new FlexCelReport(true);
      _items = new List<IPatchReportDto>();
    }

    public string TemplatePath { get; set; }

    public string OutputPath { get; set; }

    public virtual async Task RunAsync()
    {
      if (_items == null) {
        throw new InvalidDataException("Blox should not be null");
      }

      if (_items.Count == 0) return;

      await Task.Run(() =>
                     {
                       _report.AddTable("Blox", _items);
                       _report.Run(TemplatePath, OutputPath);
                     });
    }

    public void BuildPatchReport()
    {
      {
        if (!File.Exists(TemplatePath)) {
          throw new FileNotFoundException("Report template not found", TemplatePath);
        }

        if (_items.Count == 0) return;

        _report.AddTable("Items", _items);
        _report.Run(TemplatePath, OutputPath);
      }
    }

    public void BuildMetrixCsv()
    {
      throw new NotImplementedException();
    }

    public void AddItems(IEnumerable<IPatchReportDto> items)
    {
      _items.AddRange(items);
    }

    public void BuildDtoList(string storeListPath)
    {
      if (string.IsNullOrWhiteSpace(storeListPath)) throw new ArgumentNullException("storeListPath");
      if (!File.Exists(storeListPath)) throw new FileNotFoundException("Store list file not found");

      _adPatchExtractor.Initialize(storeListPath);
      var patches = _adPatchExtractor.Extract()
        .ToDictionary(k => k.Name, v => v.StoreCount);

      // Check if there are books with no patch in the patch list:
      var missingPatches = _job.Books.Select(b => b.BookName)
        .Except(patches.Keys)
        .ToList();

      // Throw if there are missing patches:
      if (missingPatches.Any()) {
        var patchList = string.Join(", ", missingPatches);
        throw new InvalidOperationException(
          string.Format("The store list is missing data for patch {0}", patchList));
      }

      var dtos =  from book in _job.Books
                  let name = book.BookName
                  select new PatchReportDto(name, book.PageCount, patches[name]);

      _items.AddRange(dtos);
    }
  }
}