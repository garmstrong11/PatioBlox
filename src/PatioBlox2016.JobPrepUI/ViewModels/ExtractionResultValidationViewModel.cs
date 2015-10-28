namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using Abstract;
  using Caliburn.Micro;
  using Services.Contracts;

  public class ExtractionResultValidationViewModel : Screen
  {
    private readonly IWindowManager _windowManager;
    private readonly IExtractionResultValidationUow _uow;
    private readonly IJobFolders _jobFolders;
    private readonly IJob _job;
    private int _missingDescriptionCount;

    public ExtractionResultValidationViewModel(
      IWindowManager windowManager,
      IExtractionResultValidationUow uow,
      IJobFolders jobFolders, 
      IJob job)
    {
      _windowManager = windowManager;
      _uow = uow;
      _jobFolders = jobFolders;
      _job = job;

      InvalidProducts = new BindableCollection<IProduct>();
      MissingPhotos = new BindableCollection<string>();
      DuplicateProducts = new BindableCollection<IPatchProductDuplicate>();
    }

    protected override void OnActivate()
    {
      _uow.PersistNewDescriptions();
      _uow.PersistNewUpcReplacements();
      _uow.PersistNewKeywords();

      //_job.ClearBooks();
      //_job.ClearDescriptions();
      //_job.ClearProducts();

      _job.AddDescriptionRange(_uow.GetDescriptionsForJob());
      _job.PopulateBooks(_uow.GetBookGroups());
      _job.PopulateProducts(_uow.GetProducts());

      InvalidProducts.AddRange(FindInvalidProducts());
      MissingPhotos.AddRange(FindSkusWithNoPhoto());
      MissingDescriptionCount = _uow.GetUnresolvedDescriptions().Count;
      DuplicateProducts.AddRange(FindDuplicateProducts());
    }

    protected override void OnDeactivate(bool close)
    {
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
      var products = _job.Products
        .Where(p => p.IsBarcodeInvalid)
        .OrderBy(p => p.Sku).ToList();

      return products;
    }

    private IEnumerable<IPatchProductDuplicate> FindDuplicateProducts()
    {
      return _uow.GetPatchProductDuplicates();
    }

    public void SaveReport()
    {
      var sb = new StringBuilder();
      sb.AppendFormat("Job name is {0}\n", _jobFolders.JobName);
      sb.AppendFormat("Extractor found {0} patches in the data files\n", PatchCount);
      sb.AppendFormat("\nPhotos are not found for these {0} item numbers:\n", MissingPhotos.Count);

      foreach (var photo in MissingPhotos) {
        sb.AppendFormat("\t{0}\n", photo);
      }

      sb.AppendFormat("\nThese {0} upcs are invalid:\n", InvalidProducts.Count);

      foreach (var invalidProduct in InvalidProducts) {
        sb.AppendFormat("\tUpc {0}, Item {1}: {2}\n",
          invalidProduct.Upc, invalidProduct.Sku, string.Join(", ", invalidProduct.BarcodeErrors));
      }

      sb.AppendFormat("\nThese {0} Patches have duplicate rows:\n", DuplicateProducts.Count);

      foreach (var duplicate in DuplicateProducts) {
        sb.AppendFormat("\t{0}\n", duplicate);
      }

      File.WriteAllText(_jobFolders.ExtractionReportPath, sb.ToString());

      var messageWindow = new MessageWindowViewModel("File saved successfully!") {DisplayName = "Success!"};

      _windowManager.ShowDialog(messageWindow);
    }
  }
}