using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatioBlox2018.Blox
{
  using System.IO;
  using PatioBlox2016.Extractor;
  using PatioBlox2018.Core;
  using SimpleInjector;

  class Program
  {
    public static void Main(string[] args)
    {
      var container = ConfigureContainer();
      var cwd = new DirectoryInfo(Environment.CurrentDirectory);
      var xlFiles = cwd.EnumerateFiles("*.xlsx");
      var extractor = container.GetInstance<IExtractor<IPatchRow>>();
      var rows = extractor.Extract(xlFiles.Select(f => f.FullName)).Take(100).ToList();

      rows.ForEach(r => Console.WriteLine(r.ToString()));

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
