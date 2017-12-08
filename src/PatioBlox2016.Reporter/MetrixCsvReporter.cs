namespace PatioBlox2016.Reporter
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using PatioBlox2018.Impl;
  using PatioBlox2018.Impl.AbstractReporter;

  public class MetrixCsvReporter : IReporter
  {
    private IJob Job { get; }

    public MetrixCsvReporter(IJob job)
    {
      Job = job;

      var prinergyFolderName = ScanbookFileOps.GetPrinergyJobRoot();
      OutputPath = $@".\out_reports\{prinergyFolderName}_MetrixProducts.csv";
    }

    public void BuildReport()
    {
      var rowList = new List<string> { GetMetrixHeader() };
      rowList.AddRange(GetMetrixRows(Job.Books.Values));

      File.WriteAllText(OutputPath, string.Join(Environment.NewLine, rowList));
    }

    public string OutputPath { get; }

    public List<string> GetMetrixRows(IEnumerable<ScanbookBook> books)
    {
      var rows = 
        from book in books
        from name in book.GetSheetNames()
        select $"{name},1,{book.SetsForPatch},8.5,5.5,,,{name}.pdf,,,,,,,,";

      return rows.ToList();
    }

    private static string GetMetrixHeader()
    {
      var sb = new StringBuilder()
      .Append("Item #,")
      .Append("Versions,")
      .Append("Quantity,")
      .Append("Trim Width,")
      .Append("Trim Height,")
      .Append("PageColor Name Side 1,")
      .Append("PageColor Name Side 2,")
      .Append("Content File,")
      .Append("Product Group Name,")
      .Append("Company Name,")
      .Append("Company Contact First Name,")
      .Append("Company Contact Family Name,")
      .Append("Description,")
      .Append("Notes,")
      .Append("Due Date,")
      .Append("Grain Direction,")
      .Append("Offcut Top,")
      .Append("Offcut Left,")
      .Append("Offcut Bottom,")
      .Append("Offcut Right,")
      .Append("Priority");

      return sb.ToString();
    }
  }
}