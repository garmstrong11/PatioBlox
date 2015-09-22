namespace PatioBlox2016.Services.EfImpl
{
  using System.Collections.Generic;
  using System.Linq;
  using Concrete;
  using Contracts;
  using DataAccess;

  public class DescriptionRepository : RepositoryBase<Description>, IDescriptionRepository
  {
    public DescriptionRepository(PatioBloxContext context) : base(context)
    {
    }

    public Dictionary<string, Description> GetDescriptionDictionary()
    {
      return GetAll().ToDictionary(k => k.Text);
    }

    public Dictionary<string, int> GetTextToIdDict()
    {
      return Context.Descriptions
        .Select(d => new {d.Text, d.Id})
        .ToDictionary(k => k.Text, v => v.Id);
    }

    public IEnumerable<string> FilterExisting(IEnumerable<string> texts)
    {
      return texts
        .Except(GetAll().Select(d => d.Text));
    }

    public IEnumerable<Description> GetDescriptionsForJob(IEnumerable<string> descriptionStrings)
    {
      return Context.Descriptions.Where(d => descriptionStrings.Contains(d.Text));
    }
  }
}