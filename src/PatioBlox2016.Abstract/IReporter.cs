namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;

  public interface IReporter
  {
    string TemplatePath { get; set; }
    string OutputPath { get; set; }

    void BuildDtoList(string storeListPath);
    void BuildPatchReport();
    void BuildMetrixCsv();

    void AddItems(IEnumerable<IPatchReportDto> items);
  }
}