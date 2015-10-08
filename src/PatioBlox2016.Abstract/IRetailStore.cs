namespace PatioBlox2016.Abstract
{
  public interface IRetailStore
  {
    string Name { get; }
    int Id { get; }
    IAdvertisingPatch AdertisingPatch { get; set; }
  }
}