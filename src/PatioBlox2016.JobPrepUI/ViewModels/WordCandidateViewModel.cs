namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using Caliburn.Micro;

  public class WordCandidateViewModel : PropertyChangedBase, ISelectableViewModel
  {
    private bool _isSelected;
    private List<string> _usages;

    public WordCandidateViewModel()
    {
      Usages = new List<string>();
    }

    public WordCandidateViewModel(string word, IEnumerable<string> usages) 
      : this()
    {
      Word = word;
      Usages.AddRange(usages);
    }

    public WordCandidateViewModel(KeywordViewModel kwvm) : this()
    {
      Word = kwvm.Word;
      Usages.AddRange(kwvm.Usages);
    }

    public string Word { get; private set; }

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

    public List<string> Usages
    {
      get { return _usages; }
      set
      {
        if (Equals(value, _usages)) return;
        _usages = value;
        NotifyOfPropertyChange(() => Usages);
      }
    }
  }
}