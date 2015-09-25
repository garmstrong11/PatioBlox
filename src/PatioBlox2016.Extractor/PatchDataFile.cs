namespace PatioBlox2016.Extractor
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using Abstract;

  public class PatchDataFile
  {
    private readonly IJobFolders _jobFolders;
    private readonly HashSet<string> _sheetNames;
    private string _path;

    public PatchDataFile(IJobFolders jobFolders)
    {
      _jobFolders = jobFolders;
      _sheetNames = new HashSet<string>();
    }

    public string Path
    {
      get { return _path; }
      set
      {
        if (_path == value) return;

        if (!File.Exists(value)) {
          throw new FileNotFoundException(value);
        }

        _path = value;
      }
    }

    public void AddSheetName(string sheetName)
    {
      if (string.IsNullOrWhiteSpace(sheetName)) {
        throw new ArgumentNullException("sheetName");
      }

      var added = _sheetNames.Add(sheetName);

      if (!added) {
        throw new InvalidOperationException(
          string.Format("A patch with the name {0} already exists.", sheetName));
      }
    }

    public List<string> SheetNames
    {
      get { return _sheetNames.ToList(); }
    }

    protected bool Equals(PatchDataFile other)
    {
      return SheetNames.Equals(other.SheetNames) 
        && string.Equals(Path, other.Path);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((PatchDataFile) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (SheetNames.GetHashCode() * 397) ^ Path.GetHashCode();
      }
    }

    public override string ToString()
    {
      return Path;
    }
  }
}