namespace PatioBlox2016.Services.EfImpl
{
  using PatioBlox2016.Concrete;
  using PatioBlox2016.DataAccess;

  public class ExpansionRepository : RepositoryBase<Expansion>
  {
    public ExpansionRepository(PatioBloxContext context) : base(context) {}
  }
}