namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using System.Linq;
  using Abstract;
  using Caliburn.Micro;
  using Extractor;
  using Services.Contracts;

  public class ExtractionResultValidationViewModel : Screen
  {
    private readonly IDescriptionRepository _descriptionRepo;
    private readonly IUpcReplacementRepository _upcRepo;
    private readonly IExtractionResult _extractionResult;
    private readonly IJobFolders _jobFolders;
    private BindableCollection<string> _missingDescriptions;
    private BindableCollection<IProduct> _invalidProducts;
    private BindableCollection<string> _missingPhotos;
    private BindableCollection<IProduct> _duplicateProducts;

    public ExtractionResultValidationViewModel(
      IDescriptionRepository descriptionRepo, 
      IUpcReplacementRepository upcRepo, 
      IExtractionResult extractionResult, 
      IJobFolders jobFolders)
    {
      _descriptionRepo = descriptionRepo;
      _upcRepo = upcRepo;
      _extractionResult = extractionResult;
      _jobFolders = jobFolders;

      _missingDescriptions = new BindableCollection<string>();
      _invalidProducts = new BindableCollection<IProduct>();
      _missingPhotos = new BindableCollection<string>();
      _duplicateProducts = new BindableCollection<IProduct>();
    }

    protected override void OnActivate()
    {
      var missingDesrciptions = _descriptionRepo
        .FilterExisting(_extractionResult.UniqueDescriptions)
        .OrderBy(d => d);

      MissingDescriptions.AddRange(missingDesrciptions);
      InvalidProducts.AddRange(FindInvalidProducts());
      MissingPhotos.AddRange(FindSkusWithNoPhoto());
      DuplicateProducts.AddRange(FindDuplicateProducts());
    }

    public BindableCollection<string> MissingDescriptions
    {
      get { return _missingDescriptions; }
      set
      {
        if (Equals(value, _missingDescriptions)) return;
        _missingDescriptions = value;
        NotifyOfPropertyChange(() => MissingDescriptions);
      }
    }

    public BindableCollection<IProduct> InvalidProducts
    {
      get { return _invalidProducts; }
      set
      {
        if (Equals(value, _invalidProducts)) return;
        _invalidProducts = value;
        NotifyOfPropertyChange(() => InvalidProducts);
      }
    }

    public BindableCollection<string> MissingPhotos
    {
      get { return _missingPhotos; }
      set
      {
        if (Equals(value, _missingPhotos)) return;
        _missingPhotos = value;
        NotifyOfPropertyChange(() => MissingPhotos);
      }
    }

    public BindableCollection<IProduct> DuplicateProducts
    {
      get { return _duplicateProducts; }
      set
      {
        if (Equals(value, _duplicateProducts)) return;
        _duplicateProducts = value;
        NotifyOfPropertyChange(() => DuplicateProducts);
      }
    }

    private IEnumerable<string> FindSkusWithNoPhoto()
    {
      return _extractionResult.UniqueSkus.Select(s => s.ToString())
        .Except(_jobFolders.GetExistingPhotoFileNames());
    }

    private IEnumerable<IProduct> FindInvalidProducts()
    {
      var existingReplacements = _upcRepo.GetAll().Select(u => u.InvalidUpc);

      var products = _extractionResult.GetUniqueProducts()
        .Where(p => p.IsBarcodeInvalid && !existingReplacements.Contains(p.Upc))
        .OrderBy(p => p.Sku).ToList();

      return products;
    }

    private IEnumerable<IProduct> FindDuplicateProducts()
    {
      return _extractionResult.GetUniqueProducts()
        .Where(p => p.HasPatchProductDuplicates);
    } 

  }
}