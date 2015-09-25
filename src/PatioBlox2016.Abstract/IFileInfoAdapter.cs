namespace PatioBlox2016.Abstract
{
  public interface IFileInfoAdapter
  {
    bool Exists { get; }
    string FullName { get; }
    IDirectoryInfoAdapter Directory { get; }
  }
}