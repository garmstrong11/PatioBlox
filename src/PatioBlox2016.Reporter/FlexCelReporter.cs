namespace PatioBlox2016.Reporter
{
  using FlexCel.Report;
  using PatioBlox2018.Impl;
  using PatioBlox2018.Impl.AbstractReporter;
  using System;
  using System.Linq;

  public class FlexCelReporter : IReporter
  {
    private static string FlexCelTemplatePath { get; }

    private IJob Job { get; }
    private  FlexCelReport Report { get; }

    static FlexCelReporter()
    {
      FlexCelTemplatePath = ScanbookFileOps.FlexCelReportTemplatePath;
    }

    public FlexCelReporter(IJob job)
    {
      Job = job;
      Report = new FlexCelReport(true);
    }

    public string OutputPath => ScanbookFileOps.FlexCelReportOutputPath;

    public void BuildReport()
    {
      var items = Job.Books.Values.Select(b => new PatchReportDto(b)).AsQueryable();

      if (!items.Any()) {
        throw new InvalidOperationException("Patches were not found for this report");
      }

      Report.AddTable("Items", items.ToList());
      Report.Run(FlexCelTemplatePath, OutputPath);
    }
  }
}