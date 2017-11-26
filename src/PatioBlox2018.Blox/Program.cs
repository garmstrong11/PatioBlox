namespace PatioBlox2018.Blox
{
  using PatioBlox2016.Extractor;
  using PatioBlox2018.Core;
  using PatioBlox2018.Impl;
  using SimpleInjector;
  using System;

  internal class Program
  {
    public static void Main(string[] args)
    {
      var container = ConfigureContainer();

      try {
        var fileOps = container.GetInstance<IFileOps>();
        var patchRowExtractor = container.GetInstance<IExtractor<IPatchRow>>();
        var storeExtractor = container.GetInstance<IExtractor<IAdvertisingPatch>>();

        var job = new ScanbookJob(patchRowExtractor);
        job.AddDataSource(fileOps.PatioBloxExcelFilePath);

        job.BuildBooks();
        var proj = job.GetJson();
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

      return container;
    }
  }
}
