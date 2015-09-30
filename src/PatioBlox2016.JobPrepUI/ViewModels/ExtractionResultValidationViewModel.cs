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
    private int _missingDescriptionCount;
    private readonly List<IProduct> _products;

    public ExtractionResultValidationViewModel(
      IExtractionResultValidationUow uow,
      IJobFolders jobFolders)
    {
      _uow = uow;
      _jobFolders = jobFolders;
      _products = new List<IProduct>();

      InvalidProducts = new BindableCollection<IProduct>();
      MissingPhotos = new BindableCollection<string>();
      DuplicateProducts = new BindableCollection<IPatchProductDuplicate>();
    }

    protected override void OnActivate()
    {
      _uow.PersistNewDescriptions();
      _uow.PersistNewUpcReplacements();
      _uow.PersistNewKeywords();

      _products.AddRange(_uow.GetProducts());
      InvalidProducts.AddRange(FindInvalidProducts());

      MissingPhotos.AddRange(FindSkusWithNoPhoto());
      MissingDescriptionCount = _uow.GetUnresolvedDescriptions().Count;
      DuplicateProducts.AddRange(FindDuplicateProducts());
    }

    protected override void OnDeactivate(bool close)
    {
      _products.Clear();
      InvalidProducts.Clear();
      MissingPhotos.Clear();
      DuplicateProducts.Clear();
      base.OnDeactivate(close);
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

    public BindableCollection<IProduct> InvalidProducts { get; private set; }

    public BindableCollection<string> MissingPhotos { get; private set; }

    public BindableCollection<IPatchProductDuplicate> DuplicateProducts { get; private set; }


    private IEnumerable<string> FindSkusWithNoPhoto()
    {
      return _uow.GetUniqueSkus()
        .Select(s => s.ToString())
        .Except(_jobFolders.GetExistingPhotoFileNames())
        .OrderBy(sku => sku);
    }

    private IEnumerable<IProduct> FindInvalidProducts()
    {
      var products = _products
        .Where(p => p.IsBarcodeInvalid)
        .OrderBy(p => p.Sku).ToList();

      return products;
    }

    private IEnumerable<IPatchProductDuplicate> FindDuplicateProducts()
    {
      return _uow.GetPatchProductDuplicates();
    } 
  }
}