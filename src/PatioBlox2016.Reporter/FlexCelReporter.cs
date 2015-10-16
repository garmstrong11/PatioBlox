namespace PatioBlox2016.Reporter
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using Abstract;
  using FlexCel.Report;

  public class FlexCelReporter : IReporter
  {
    private readonly FlexCelReport _report;
    private readonly IJob _job;
    private readonly IAdvertisingPatchExtractor _adPatchExtractor;
    private readonly ISettingsService _settings;
    private readonly List<IPatchReportDto> _items;

    public FlexCelReporter(IJob job, IAdvertisingPatchExtractor adPatchExtractor, ISettingsService settings)
    {
      if (job == null) throw new ArgumentNullException("job");
      if (adPatchExtractor == null) throw new ArgumentNullException("adPatchExtractor");
      if (settings == null) throw new ArgumentNullException("settings");

      _job = job;
      _adPatchExtractor = adPatchExtractor;
      _settings = settings;
      _items = new List<IPatchReportDto>();

      _report = new FlexCelReport(true);
      IsInitialized = false;
    }

    public void Initialize(string storeListPath)
    {
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

      var dtos = from book in _job.Books
        let name = book.BookName
        select new PatchReportDto(name, book.PageCount, patches[name], _settings.CopiesPerStore);

      _items.AddRange(dtos);
      IsInitialized = true;
    }

    public async Task BuildPatchReport(string templatePath, string outputPath)
    {
      if (!File.Exists(templatePath)) {
        throw new FileNotFoundException("Report template not found", templatePath);
      }

      if (!_items.Any()) {
        throw new InvalidOperationException("Patches were not found for this report");
      }

      _report.AddTable("Items", _items);
      await Task.Run(() => _report.Run(templatePath, outputPath));
    }

    public async Task BuildMetrixCsv(string outputPath)
    {
      // Validate that outputPath is a legal Windows path;
      // Path.GetFullPath will throw if outputPath is not valid.
      var fullPath = Path.GetFullPath(outputPath);

      var outFile = new FileInfo(fullPath);

      // throw if no items exist (reporter is not initialized)
      if (!_items.Any()) {
        throw new InvalidOperationException("Patches were not found for this report");
      }

      // If there are items, then we have passed initialization, 
      // so the dictionary is safe to use against _job.Books.
      var storeCountDict = _items.ToDictionary(k => k.Name, v => v.StoreCount);

      const string artWidth = "8.5";
      const string artHeight = "5.5";
      const string format = "{0},1,{1},{2},{3},,,{0}.pdf,,,,,,,,";
      var result = new List<string> {GetMetrixHeader()};

      foreach (var book in _job.Books) {
        var copyCount = _settings.CopiesPerStore*storeCountDict[book.BookName];
        result.AddRange(book.PdfFileNames.Select(b => string.Format(format, b, copyCount, artWidth, artHeight)));
      }

      using (var stream = outFile.CreateText()) {
        await stream.WriteAsync(string.Join("\n", result));
      }
    }

    public bool IsInitialized { get; private set; }

    private static string GetMetrixHeader()
    {
      var sb = new StringBuilder();
      sb.Append("Item #,");
      sb.Append("Versions,");
      sb.Append("Quantity,");
      sb.Append("Trim Width,");
      sb.Append("Trim Height,");
      sb.Append("PageColor Name Side 1,");
      sb.Append("PageColor Name Side 2,");
      sb.Append("Content File,");
      sb.Append("Product Group Name,");
      sb.Append("Company Name,");
      sb.Append("Company Contact First Name,");
      sb.Append("Company Contact Family Name,");
      sb.Append("Description,");
      sb.Append("Notes,");
      sb.Append("Due Date,");
      sb.Append("Grain Direction,");
      sb.Append("Offcut Top,");
      sb.Append("Offcut Left,");
      sb.Append("Offcut Bottom,");
      sb.Append("Offcut Right,");
      sb.Append("Priority");

      return sb.ToString();
    }
  }
}