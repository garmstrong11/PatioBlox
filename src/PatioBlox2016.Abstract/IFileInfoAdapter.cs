namespace PatioBlox2016.Abstract
{
  public interface IFileInfoAdapter
  {
    bool Exists { get; }
    string FullName { get; }
    string Name { get; }
    string NameWithoutExtension { get; }
    IDirectoryInfoAdapter Directory { get; }
  }
}