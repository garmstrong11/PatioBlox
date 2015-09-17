﻿namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using System.Linq;
  using Caliburn.Micro;
  using PatioBlox2016.Concrete;
  using PatioBlox2016.Extractor;
  using PatioBlox2016.Services.Contracts;

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
      var badUpcs = _extractionResult.InvalidUpcs
        .Select(b => new UpcReplacement() {InvalidUpc = b});

      UpcReplacements.AddRange(badUpcs.Select(b => new UpcReplacementViewModel(b)));
      
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