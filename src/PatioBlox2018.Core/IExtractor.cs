namespace PatioBlox2018.Core
{
  using System.Collections.Generic;

  public interface IExtractor<out T> where T : class
	{
    IEnumerable<T> Extract(string excelFilePath);
	}
}