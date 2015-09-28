namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using System.Linq;
  using Abstract;
  using Caliburn.Micro;
  using Services.Contracts;

  public class ExtractionResultValidationViewModel : Screen
  {
    private readonly IExtractionResultValidationUow _uow;
    private readonly IJobFolders _jobFolders;
    private BindableCollection<IProduct> _invalidProducts;
    private BindableCollection<string> _missingPhotos;
    private BindableCollection<IProduct> _duplicateProducts;
    private int _missingDescriptionCount;
    private readonly List<IProduct> _products; 

    public ExtractionResultValidationViewModel(
      IExtractionResultValidationUow uow,
      IJobFolders jobFolders)
    {
      _uow = uow;
      _jobFolders = jobFolders;
      _products = new List<IProduct>();

      _invalidProducts = new BindableCollection<IProduct>();
      _missingPhotos = new BindableCollection<string>();
      _duplicateProducts = new BindableCollection<IProduct>();
    }

    protected override void OnActivate()
    {
      ClearAllCollections();

      _uow.PersistNewDescriptions();
      _uow.PersistNewUpcReplacements();
      _uow.PersistNewKeywords();

      _products.AddRange(_uow.GetProducts());
      InvalidProducts.AddRange(FindInvalidProducts());
      MissingPhotos.AddRange(FindSkusWithNoPhoto());
      MissingDescriptionCount = _uow.GetUnresolvedDescriptions().Count;
      DuplicateProducts.AddRange(FindDuplicateProducts());
    }

    private void ClearAllCollections()
    {
      _products.Clear();
      InvalidProducts.Clear();
      MissingPhotos.Clear();
      DuplicateProducts.Clear();
    }

    public int PatchCount
    {
      get { return _uow.GetPatchCount(); }
    }

    public int DescriptionCount
    {
      get { return _uow.GetUniqueDescriptionCount(); }
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
      return _uow.GetUniqueSkus()
        .Select(s => s.ToString())
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