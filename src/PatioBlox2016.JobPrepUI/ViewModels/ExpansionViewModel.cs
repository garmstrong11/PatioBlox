namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using Caliburn.Micro;

  public class ExpansionViewModel : PropertyChangedBase, ISelectableViewModel
  {
    private string _word;
    private bool _isSelected;

    public string Word
    {
      get { return _word; }
      set
      {
        if (value == _word) return;
        _word = value;
        NotifyOfPropertyChange(() => Word);
      }
    }

    public bool IsSelected
    {
      get { return _isSelected; }
      set
      {
        if (value == _isSelected) return;
        _isSelected = value;
        NotifyOfPropertyChange(() => IsSelected);
      }
    }
  }
}