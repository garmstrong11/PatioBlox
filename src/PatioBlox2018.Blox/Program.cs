namespace PatioBlox2018.Blox
{
  using PatioBlox2016.Extractor;
  using PatioBlox2018.Core;
  using PatioBlox2018.Impl;
  using SimpleInjector;
  using System;
  using System.Configuration;

  internal class Program
  {
    public static void Main(string[] args)
    {
      var container = ConfigureContainer();
      var blockPath = ConfigurationManager.AppSettings["BlocksFilename"];
      var storePath = ConfigurationManager.AppSettings["StoresFilename"];

      try {
        var fileOps = container.GetInstance<IFileOps>();
        var blockExtractor = container.GetInstance<IExtractor<IPatchRow>>();
        var storeExtractor = container.GetInstance<IExtractor<IAdvertisingPatch>>();

        var job = new ScanbookJob(blockExtractor, storeExtractor, fileOps);

        job.BuildBooks(blockPath, storePath);
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
