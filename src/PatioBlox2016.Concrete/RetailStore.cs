namespace PatioBlox2016.Concrete
{
  using PatioBlox2016.Abstract;

  public class RetailStore : IRetailStore
  {
    public RetailStore(string name, int id)
    {
      Name = name;
      Id = id;
    }
    
    #region Implementation of IRetailStore

    public string Name { get; private set; }
    public int Id { get; private set; }
    public IAdvertisingPatch AdertisingPatch { get; set; }

    #endregion

    #region Equality members

    protected bool Equals(RetailStore other)
    {
      return Id == other.Id;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((RetailStore) obj);
    }

    public override int GetHashCode()
    {
      return Id;
    }

    #endregion
  }
}