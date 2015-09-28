namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Linq;
  using Caliburn.Micro;
  using Services.Contracts;

  public class UpcReplacementManagerViewModel : Screen
  {
    private readonly IExtractionResultValidationUow _uow;
    private BindableCollection<UpcReplacementViewModel> _upcReplacements;
    private UpcReplacementViewModel _selectedUpcReplacement;

    public UpcReplacementManagerViewModel(IExtractionResultValidationUow uow)
    {
      _uow = uow;

      UpcReplacements = new BindableCollection<UpcReplacementViewModel>();
    }

    protected override void OnActivate()
    {
      var products = _uow.GetProducts()
        .Where(p => p.IsBarcodeInvalid)
        .OrderBy(p => p.Sku).ToList();

      //var dupes = products.Where(p => p.HasPatchProductDuplicates);
      UpcReplacements.Clear();
      UpcReplacements.AddRange(products.Select(p => new UpcReplacementViewModel(p)));
      
      base.OnActivate();
    }

    public BindableCollection<UpcReplacementViewModel> UpcReplacements
    {
      get { return _upcReplacements; }
      set
      {
        if (Equals(value, _upcReplacements)) return;
        _upcReplacements = value;
        NotifyOfPropertyChange(() => UpcReplacements);
      }
    }

    public UpcReplacementViewModel SelectedUpcReplacement
    {
      get { return _selectedUpcReplacement; }
      set
      {
        if (Equals(value, _selectedUpcReplacement)) return;
        _selectedUpcReplacement = value;
        NotifyOfPropertyChange(() => SelectedUpcReplacement);
      }
    }
  }
}