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
    private BindableCollection<UpcReplacementViewModel> _upcReplacements;
    private UpcReplacementViewModel _selectedUpcReplacement;

    public UpcReplacementManagerViewModel(
      IExtractionResult extractionResult, 
      IUpcReplacementRepository upcReplacementRepository)
    {
      if (extractionResult == null) throw new ArgumentNullException("extractionResult");
      if (upcReplacementRepository == null) throw new ArgumentNullException("upcReplacementRepository");

      _extractionResult = extractionResult;
      _upcReplacementRepository = upcReplacementRepository;

      UpcReplacements = new BindableCollection<UpcReplacementViewModel>();
    }

    protected override void OnActivate()
    {
      var existingReplacements = _upcReplacementRepository.GetAll().Select(u => u.InvalidUpc);

      var products = _extractionResult.GetUniqueProducts()
        .Where(p => p.IsBarcodeInvalid && !existingReplacements.Contains(p.Upc))
        .OrderBy(p => p.Sku).ToList();


      //var dupes = products.Where(p => p.HasPatchProductDuplicates);
      
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