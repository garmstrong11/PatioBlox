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

    public IEnumerable<string> FilterExisting(IEnumerable<string> texts)
    {
      return texts
        .Except(GetAll().Select(d => d.Text));
    }
  }
}