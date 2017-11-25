using System;
using System.Linq;

namespace PatioBlox2018.Blox
{
  using PatioBlox2016.Extractor;
  using PatioBlox2018.Core;
  using PatioBlox2018.Impl;
  using SimpleInjector;
  using System.IO;

  internal class Program
  {
    public static void Main(string[] args)
    {
      var container = ConfigureContainer();
      var cwd = new DirectoryInfo(Environment.CurrentDirectory);
      var xlFiles = cwd.EnumerateFiles("*.xlsx").ToList();
      var extractor = container.GetInstance<IExtractor<IPatchRow>>();

      var job = new ScanbookJob(extractor);
      xlFiles.ForEach(x => job.AddDataSource(x.FullName));

      job.BuildBooks();

      Console.ReadKey();
    }

    private static Container ConfigureContainer()
    {
      var container = new Container();

      container.RegisterSingleton<IExtractor<IPatchRow>, PatchExtractor>();
      container.RegisterSingleton<IDataSourceAdapter, FlexCelDataSourceAdapter>();
      container.RegisterSingleton<IColumnIndexService, FlexcelColumnIndexService>();

      return container;
    }
  }
}
