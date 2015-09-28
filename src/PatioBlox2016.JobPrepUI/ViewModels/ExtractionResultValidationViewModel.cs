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
    private readonly IExtractionResult _extractionResult;
    private readonly IJobFolders _jobFolders;
    private BindableCollection<string> _missingDescriptions;
    private BindableCollection<IProduct> _invalidProducts;
    private BindableCollection<string> _missingPhotos;
    private BindableCollection<IProduct> _duplicateProducts;
    private int _missingDescriptionCount;
    private readonly List<IProduct> _products; 

    public ExtractionResultValidationViewModel(
      IDescriptionRepository descriptionRepo, 
      IExtractionResult extractionResult, 
      IJobFolders jobFolders, IProductUow productUow)
    {
      _descriptionRepo = descriptionRepo;
      _extractionResult = extractionResult;
      _jobFolders = jobFolders;
      _products = productUow.GetProducts().ToList();

      _missingDescriptions = new BindableCollection<string>();
      _invalidProducts = new BindableCollection<IProduct>();
      _missingPhotos = new BindableCollection<string>();
      _duplicateProducts = new BindableCollection<IProduct>();
    }

    protected override void OnActivate()
    {
      MissingDescriptions .Clear();
      var missingDescriptions = _descriptionRepo
        .FilterExisting(_extractionResult.UniqueDescriptions)
        .OrderBy(d => d);

      MissingDescriptions.AddRange(missingDescriptions);
      MissingDescriptionCount = MissingDescriptions.Count;

      InvalidProducts.AddRange(FindInvalidProducts());
      MissingPhotos.AddRange(FindSkusWithNoPhoto());
      DuplicateProducts.AddRange(FindDuplicateProducts());
    }

    public string PatchCount
    {
      get { return _extractionResult.PatchNames.Count().ToString(); }
    }

    public string DescriptionCount
    {
      get { return _extractionResult.UniqueDescriptions.Count().ToString(); }
    }

    public string JobName
    {
      get { return _jobFolders.JobName; }
    }

    public int MissingDescriptionCount
    {
      get { return _missingDescriptionCount; }
      set
      {
        if (value == _missingDescriptionCount) return;
        _missingDescriptionCount = value;
        NotifyOfPropertyChange(() => MissingDescriptionCount);
      }
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
      var products = _products
        .Where(p => p.IsBarcodeInvalid)
        .OrderBy(p => p.Sku).ToList();

      return products;
    }

    private IEnumerable<IProduct> FindDuplicateProducts()
    {
      return _products.Where(p => p.HasPatchProductDuplicates);
    } 
  }
}