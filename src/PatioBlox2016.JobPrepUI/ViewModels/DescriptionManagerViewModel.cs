namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Linq;
  using Caliburn.Micro;
  using Services.Contracts;

  public class DescriptionManagerViewModel : Screen
  {
    private readonly IExtractionResultValidationUow _uow;
    private BindableCollection<DescriptionViewModel> _descriptions;

    public DescriptionManagerViewModel(IExtractionResultValidationUow uow)
    {
      if (uow == null) throw new ArgumentNullException("uow");

      _uow = uow;

      Descriptions = new BindableCollection<DescriptionViewModel>();
    }

    protected override void OnActivate()
    {
      Descriptions.Clear();

      var descriptions = _uow.GetUnresolvedDescriptions();

      Descriptions.AddRange(descriptions.Select(d => new DescriptionViewModel(d)));

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
      _uow.SaveChanges();
    }
  }
}