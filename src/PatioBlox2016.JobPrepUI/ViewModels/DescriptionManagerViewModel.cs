namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity.Core;
  using System.Linq;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;
  using Services.Contracts;

  public class DescriptionManagerViewModel : Screen
  {
    private readonly IExtractionResultValidationUow _uow;
    private readonly IWindowManager _windowManager;
    private BindableCollection<DescriptionViewModel> _descriptions;
    private readonly Dictionary<string, Keyword> _keywordDict; 

    public DescriptionManagerViewModel(IExtractionResultValidationUow uow, IWindowManager windowManager)
    {
      if (uow == null) throw new ArgumentNullException("uow");

      _uow = uow;
      _windowManager = windowManager;
      _keywordDict = _uow.GetKeywordDict();

      Descriptions = new BindableCollection<DescriptionViewModel>();
    }

    protected override void OnActivate()
    {
      Descriptions.Clear();

      var descriptions = _uow.GetUnresolvedDescriptions();

      Descriptions.AddRange(descriptions.Select(d => new DescriptionViewModel(d, _keywordDict)));

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

    public void ResolveAll()
    {
      foreach (var descriptionViewModel in Descriptions) {
        descriptionViewModel.Resolve();
      }
    }

    public void Save()
    {
      try {
        _uow.SaveChanges();
        Descriptions.Clear();

        var messageWindow = new MessageWindowViewModel("Descriptions saved successfully!") 
          {DisplayName = "Success!"};

        _windowManager.ShowDialog(messageWindow);
      }

      catch (EntityException exc) {
        var errorWindow = new ErrorWindowViewModel();
        var errorString = exc.Message;
        errorWindow.DisplayName = "Database Error";
        errorWindow.Errors = errorString;

        _windowManager.ShowDialog(errorWindow);
      }
    }
  }
}