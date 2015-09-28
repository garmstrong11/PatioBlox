namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Linq;
  using Caliburn.Micro;
  using Extractor;
  using Services.Contracts;

  public class UpcReplacementManagerViewModel : Screen
  {
    private readonly IExtractionResult _extractionResult;
    private readonly IUpcReplacementRepository _upcReplacementRepository;
    private readonly IProductUow _productUow;
    private BindableCollection<UpcReplacementViewModel> _upcReplacements;
    private UpcReplacementViewModel _selectedUpcReplacement;

    public UpcReplacementManagerViewModel(
      IExtractionResult extractionResult, 
      IProductUow productUow)
    {
      if (extractionResult == null) throw new ArgumentNullException("extractionResult");
      if (productUow == null) throw new ArgumentNullException("productUow");

      _extractionResult = extractionResult;
      _productUow = productUow;

      UpcReplacements = new BindableCollection<UpcReplacementViewModel>();
    }

    protected override void OnActivate()
    {
      var products = _productUow.GetProducts()
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