namespace PatioBlox2018.Core.ScanbookEntities {
  using System.Collections.Generic;

  public interface IPage
  {
    ISection Section { get; }
    IEnumerable<IPatioBlok> PatioBlox { get; }
    string Header { get; }
    int BlockCount { get; }
    int SourceRowIndex { get; }
    void AddPatioBlok(IPatioBlok patioBlok);
  }
}