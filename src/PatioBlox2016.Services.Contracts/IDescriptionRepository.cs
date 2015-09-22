namespace PatioBlox2016.Services.Contracts
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public interface IDescriptionRepository : IRepository<Description>
  {
    Dictionary<string, Description> GetDescriptionDictionary();

    IEnumerable<string> FilterExisting(IEnumerable<string> texts);

    IEnumerable<Description> GetDescriptionsForJob(IEnumerable<string> descriptionStrings);

    Dictionary<string, int> GetTextToIdDict();
  }
}