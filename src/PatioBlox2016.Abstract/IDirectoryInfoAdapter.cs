namespace PatioBlox2016.Abstract
{
  using System.Collections.Generic;
  using System.IO;

  public interface IDirectoryInfoAdapter
  {
    bool Exists { get; }
    IDirectoryInfoAdapter CreateSubdirectory(string name);
    string FullName { get; }
    IDirectoryInfoAdapter Parent { get; }
    string Name { get; }
    IEnumerable<IDirectoryInfoAdapter> GetDirectories(string searchPattern);
    IEnumerable<IDirectoryInfoAdapter> GetDirectories(string searchPattern, SearchOption searchOption);

    IEnumerable<IFileInfoAdapter> GetFiles(string searchPattern);
    IEnumerable<IFileInfoAdapter> GetFiles(string searchPattern, SearchOption searchOption);
  }
}