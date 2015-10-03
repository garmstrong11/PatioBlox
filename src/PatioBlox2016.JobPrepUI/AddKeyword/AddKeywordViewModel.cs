namespace PatioBlox2016.JobPrepUI.AddKeyword
{
  using System.Linq;
  using Caliburn.Micro;
  using Concrete;
  using FluentValidation;
  using Infra;
  using Services.Contracts;

  public class AddKeywordViewModel : ValidatingScreen
  {
    private readonly IExtractionResultValidationUow _uow;
    private string _word;
    private BindableCollection<Keyword> _parents;
    private Keyword _selectedParent;
    private bool _canSave;

    public AddKeywordViewModel(IValidator validator, IExtractionResultValidationUow uow) 
      : base(validator)
    {
      _uow = uow;
      Parents = new BindableCollection<Keyword>();
    }

    protected override void OnActivate()
    {
      Parents.AddRange(_uow.GetRootKeywords().Where(k => k.Word != Keyword.NewKey));
      SelectedParent = Parents.First(k => k.Word == Keyword.NameKey);
      base.OnActivate();
    }

    public string Word
    {
      get { return _word; }
      set
      {
        if (value == _word) return;
        _word = value;
        NotifyOfPropertyChange(() => Word);
        CanSave = !HasErrors;
      }
    }

    public BindableCollection<Keyword> Parents
    {
      get { return _parents; }
      set
      {
        if (Equals(value, _parents)) return;
        _parents = value;
        NotifyOfPropertyChange(() => Parents);
      }
    }

    public Keyword SelectedParent
    {
      get { return _selectedParent; }
      set
      {
        if (Equals(value, _selectedParent)) return;
        _selectedParent = value;
        NotifyOfPropertyChange(() => SelectedParent);
        CanSave = !HasErrors;
      }
    }

    public bool CanSave
    {
      get { return _canSave; }
      set
      {
        if (value == _canSave) return;
        _canSave = value;
        NotifyOfPropertyChange(() => CanSave);
      }
    }

    public void Save()
    {
      var keyword = new Keyword(Word) {Parent = SelectedParent};
      _uow.AddKeyword(keyword);
      _uow.SaveChanges();
      TryClose(true);
    }

    public void Cancel()
    {
      TryClose(false);
    }
  }
}