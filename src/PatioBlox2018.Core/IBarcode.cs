namespace PatioBlox2018.Core
{
  using System.Collections.Generic;

  public interface IBarcode
  {
    string Value { get; }
    int Length { get; }
    IEnumerable<string> Errors { get; }
  }
}