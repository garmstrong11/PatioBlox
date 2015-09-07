﻿namespace PatioBlox2016.JobPrepUI.Infra
{
  using System;
  using System.Collections.Generic;
  using System.IO.Abstractions;
  using System.Windows;
  using Abstract;
  using Caliburn.Micro;
  using Concrete;
  using Extractor;
  using DataAccess;
  using Services.Contracts;
  using Services.EfImpl;
  using SimpleInjector;
  using ViewModels;

  public class SiBootstrapper : BootstrapperBase
  {
    private Container _container;

    public SiBootstrapper()
    {
      LogManager.GetLog = type => new DebugLogger(type);
      Initialize();
    }

    protected override void Configure()
    {
      _container = new Container();

      _container.RegisterSingle(new PatioBloxContext());
      _container.RegisterSingle<IEventAggregator, EventAggregator>();
      _container.RegisterSingle<IShell, ShellViewModel>();
      _container.RegisterSingle<IColumnIndexService, ColumnIndexService>();
      _container.RegisterSingle<IFileSystem, FileSystem>();
      _container.RegisterSingle<IJobFolders, JobFolders>();
      _container.RegisterSingle<ISettingsService, SettingsService>();
      _container.RegisterSingle<IDataSourceAdapter, FlexCelDataSourceAdapter>();
      _container.RegisterSingle<IPatchExtractor, PatchExtractor>();
      _container.RegisterSingle<IExtractionResult, ExtractionResult>();
      _container.RegisterSingle<IDescriptionFactory, DescriptionFactory>();
      _container.RegisterSingle<IKeywordRepository, KeywordRepository>();
      _container.RegisterSingle<IDescriptionRepository, DescriptionRepository>();

      _container.Register<IWindowManager, WindowManager>();
      _container.Register<IPatchFileDropViewModel, PatchFileDropViewModel>();
      _container.Register<IActivitiesViewModel, ActivitiesViewModel>();

      _container.RegisterInitializer<ActivitiesViewModel>(vm =>
      {
        var keywords = _container.GetInstance<KeywordManagerViewModel>();
        var descriptions = _container.GetInstance<DescriptionManagerViewModel>();
        keywords.DisplayName = "Keyword Manager";
        descriptions.DisplayName = "Description Manager";

        vm.Screens.Add(keywords);
        vm.Screens.Add(descriptions);
      });

      _container.Verify();
    }

    protected override void OnStartup(object sender, StartupEventArgs e)
    {
      DisplayRootViewFor<IShell>();
    }

    protected override object GetInstance(Type service, string key)
    {
      return _container.GetInstance(service);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
      return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
      //_container.InjectProperties(instance);
    }
  }
}