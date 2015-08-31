namespace PatioBlox2016.JobPrepUI.Views
{
  using System;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;

  public class KeywordViewModel : PropertyChangedBase
  {
    private string _word;
    private string _wordType;
    private BindableCollection<string> _wordTypes;

    public KeywordViewModel(string word)
    {
      Word = word;
      WordTypes = new BindableCollection<string>(Enum.GetNames(typeof(WordType)));
      SelectedWordType = Enum.GetName(typeof (WordType), 0);
    }

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

    public BindableCollection<string> WordTypes
    {
      get { return _wordTypes; }
      set
      {
        if (Equals(value, _wordTypes)) return;
        _wordTypes = value;
        NotifyOfPropertyChange(() => WordTypes);
      }
    }

    public string SelectedWordType
    {
      get { return _wordType; }
      set
      {
        if (value == _wordType) return;
        _wordType = value;
        NotifyOfPropertyChange(() => SelectedWordType);
      }
    }
  }
}