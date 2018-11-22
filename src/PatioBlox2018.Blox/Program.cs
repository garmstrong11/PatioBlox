namespace PatioBlox2018.Blox
{
  using PatioBlox2016.Extractor;
  using PatioBlox2018.Core;
  using PatioBlox2018.Impl;
  using SimpleInjector;
  using System;
  using PatioBlox2016.Reporter;
  using PatioBlox2018.Impl.AbstractReporter;
  using PatioBlox2018.Impl.Barcodes;
  using Serilog;

  internal class Program
  {
    public static void Main(string[] args)
    {
      ConfigureLogger();
      Log.Information("Working directory: {WorkDir}", Environment.CurrentDirectory);
      var container = ConfigureContainer();

      try {
        var fileOps = container.GetInstance<IFileOps>();
        var blockExtractor = container.GetInstance<IExtractor<IPatchRow>>();
        var storeExtractor = container.GetInstance<IExtractor<IAdvertisingPatch>>();
        var barcodeFactory = container.GetInstance<IBarcodeFactory>();

        var job = new ScanbookJob(blockExtractor, storeExtractor, fileOps);

        job.BuildBooks(
          ScanbookFileOps.PatioBloxExcelFilePath, 
          ScanbookFileOps.StoreListExcelFilePath,
          barcodeFactory);

        var proj = job.GetJsxBlocks();

        fileOps.StringToFile(proj, ScanbookFileOps.JsxPath);
        IReporter reporter = new FlexCelReporter(job);
        reporter.BuildReport();

        reporter = new MetrixCsvReporter(job);
        reporter.BuildReport();

        Log.Information("Working directory: {WorkDir}", Environment.CurrentDirectory);
        Console.WriteLine("Finished successfully. Press any key to exit.");
      }
      catch (Exception exc) {
        Console.WriteLine(exc.Message);
      }

      Console.ReadKey();
    }

    private static Container ConfigureContainer()
    {
      var container = new Container();

      container.RegisterSingleton<IFileOps, ScanbookFileOps>();
      container.RegisterSingleton<IExtractor<IPatchRow>, PatchExtractor>();
      container.RegisterSingleton<IExtractor<IAdvertisingPatch>, AdvertisingPatchExtractor>();
      container.RegisterSingleton<IDataSourceAdapter, FlexCelDataSourceAdapter>();
      container.RegisterSingleton<IColumnIndexService, FlexcelColumnIndexService>();
      container.RegisterSingleton<IBarcodeFactory, DefaultBarcodeFactory>();

      container.Verify();

      return container;
    }

    public static void ConfigureLogger() =>
      Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .Enrich.FromLogContext()
        .CreateLogger();
  }
}
