namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using Abstract;

  public class DirectoryInfoAdapter : IDirectoryInfoAdapter
  {
    private readonly DirectoryInfo _directoryInfo;

    public DirectoryInfoAdapter(string path)
    {
      _directoryInfo = new DirectoryInfo(path);
    }

    public bool Exists
    {
      get { return _directoryInfo.Exists; }
    }
    public IDirectoryInfoAdapter CreateSubdirectory(string name)
    {
      var subD = _directoryInfo.CreateSubdirectory(name);
      return new DirectoryInfoAdapter(subD.FullName);
    }

    public string FullName
    {
      get { return _directoryInfo.FullName; }
    }

    public IDirectoryInfoAdapter Parent
    {
      get
      {
        return _directoryInfo.Parent == null
          ? null
          : new DirectoryInfoAdapter(_directoryInfo.Parent.FullName);
      }
    }

    public string Name
    {
      get { return _directoryInfo.Name; }
    }

    public IEnumerable<IDirectoryInfoAdapter> GetDirectories(string searchPattern)
    {
      return _directoryInfo.GetDirectories(searchPattern)
        .Select(d => new DirectoryInfoAdapter(d.FullName));
    }

    public IEnumerable<IDirectoryInfoAdapter> GetDirectories(string searchPattern, SearchOption searchOption)
    {
      return _directoryInfo.GetDirectories(searchPattern, searchOption)
        .Select(d => new DirectoryInfoAdapter(d.FullName));
    }

    public IEnumerable<IFileInfoAdapter> GetFiles(string searchPattern, SearchOption searchOption)
    {
      return _directoryInfo.GetFiles(searchPattern, searchOption)
        .Select(f => new FileInfoAdapter(f.FullName));
    }

    public IEnumerable<IFileInfoAdapter> GetFiles(string searchPattern)
    {
      return _directoryInfo.GetFiles(searchPattern)
        .Select(f => new FileInfoAdapter(f.FullName));
    }
  }
}