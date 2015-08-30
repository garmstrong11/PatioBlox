namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.IO;
  using Caliburn.Micro;

  public class PatchFileViewModel : PropertyChangedBase
  {
    private string _name;
    private string _filePath;

    public PatchFileViewModel(string filePath)
    {
      FilePath = filePath;
      Name = Path.GetFileName(FilePath);
    }

    public string Name
    {
      get { return _name; }
      set
      {
        if (value == _name) return;
        _name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

    public string FilePath
    {
      get { return _filePath; }
      set
      {
        if (value == _filePath) return;
        _filePath = value;
        NotifyOfPropertyChange(() => FilePath);
      }
    }
  }
}