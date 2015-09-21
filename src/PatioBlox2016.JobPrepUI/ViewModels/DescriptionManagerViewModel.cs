namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Linq;
  using Caliburn.Micro;
  using Extractor;
  using Services.Contracts;

  public class DescriptionManagerViewModel : Screen
  {
    private readonly IDescriptionRepository _descriptionRepository;
    private readonly IDescriptionFactory _descriptionFactory;
    private readonly IExtractionResult _extractionResult;
    private BindableCollection<DescriptionViewModel> _descriptions;

    public DescriptionManagerViewModel(
      IDescriptionRepository descriptionRepository, 
      IDescriptionFactory descriptionFactory,
      IExtractionResult extractionResult)
    {
      if (descriptionRepository == null) throw new ArgumentNullException("descriptionRepository");
      if (descriptionFactory == null) throw new ArgumentNullException("descriptionFactory");
      if (extractionResult == null) throw new ArgumentNullException("extractionResult");

      _descriptionRepository = descriptionRepository;
      _descriptionFactory = descriptionFactory;
      _extractionResult = extractionResult;

      Descriptions = new BindableCollection<DescriptionViewModel>();
    }

    protected override void OnActivate()
    {
      var newTexts = _descriptionRepository.FilterExisting(_extractionResult.UniqueDescriptions);
      _descriptionFactory.UpdateKeywordDict();

      var descriptions = newTexts
        .Select(d => _descriptionFactory.CreateDescription(d))
        .ToList();

      Descriptions.AddRange(descriptions.Select(d => new DescriptionViewModel(d)));

      _descriptionRepository.AddRange(descriptions);

      base.OnActivate();
    }

    public BindableCollection<DescriptionViewModel> Descriptions
    {
      get { return _descriptions; }
      set
      {
        if (Equals(value, _descriptions)) return;
        _descriptions = value;
        NotifyOfPropertyChange(() => Descriptions);
      }
    }

    public DescriptionViewModel SelectedDescription { get; set; }

    public void Save()
    {
      _descriptionRepository.AddRange(Descriptions.Select(d => d.Description));
      _descriptionRepository.SaveChanges();
    }
  }
}