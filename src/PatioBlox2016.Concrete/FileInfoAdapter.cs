namespace PatioBlox2016.Concrete
{
  using System.IO;
  using Abstract;

  public class FileInfoAdapter : IFileInfoAdapter
  {
    private readonly FileInfo _fileInfo;

    public FileInfoAdapter(string path)
    {
      _fileInfo = new FileInfo(path);
    }

    public bool Exists
    {
      get { return _fileInfo.Exists; }
    }

    public string FullName
    {
      get { return _fileInfo.FullName; }
    }

    public string Name
    {
      get { return _fileInfo.Name; }
    }

    public string NameWithoutExtension
    {
      get { return Path.GetFileNameWithoutExtension(_fileInfo.FullName); }
    }

    public IDirectoryInfoAdapter Directory
    {
      get
      {
        return _fileInfo.Directory == null 
          ? null 
          : new DirectoryInfoAdapter(_fileInfo.Directory.FullName);
      }
    }
  }
}