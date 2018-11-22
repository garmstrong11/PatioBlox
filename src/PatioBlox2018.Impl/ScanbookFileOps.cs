﻿namespace PatioBlox2018.Impl
{
  using System;
  using System.Configuration;
  using System.IO;
  using PatioBlox2018.Core;
  using Serilog;

  public class ScanbookFileOps : IFileOps
  {
    public static string JsxPath { get; }
    public static string PatioBloxExcelFilePath { get; }
    public static string StoreListExcelFilePath { get; }
    public static string FlexCelReportTemplatePath { get; }
    public static string FlexCelReportOutputPath { get; }

    static ScanbookFileOps()
    {
      PatioBloxExcelFilePath =
        VerifyFilePath(ConfigurationManager.AppSettings["BlocksFilename"]);

      StoreListExcelFilePath =
        VerifyFilePath(ConfigurationManager.AppSettings["StoresFilename"]);

      FlexCelReportTemplatePath =
        VerifyFilePath(ConfigurationManager.AppSettings["FlexCelTemplateName"]);

      FlexCelReportOutputPath =
        ConfigurationManager.AppSettings["FlexCelOutputName"];

      JsxPath = ConfigurationManager.AppSettings["JsxBlockDataPath"];
    }

    public void StringToFile(string content, string path)
    {
      File.WriteAllText(path, content);
    }

    private static string VerifyFilePath(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        throw new ArgumentException("Value cannot be null or whitespace.", nameof(path));

      if (!File.Exists(path))
        throw new FileNotFoundException($"Unable to find the file '{path}'.");

      return path;
    }

    public static string GetPrinergyJobRoot()
    {
      var relativeDir = new DirectoryInfo(@"..\..\..");
      return relativeDir.Name;
    }
  }
}