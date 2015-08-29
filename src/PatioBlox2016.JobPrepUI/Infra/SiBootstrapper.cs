namespace PatioBlox2016.JobPrepUI.Infra
{
  using System;
  using System.Collections.Generic;
  using System.Windows;
  using Caliburn.Micro;
  using PatioBlox2016.JobPrepUI.ViewModels;
  using SimpleInjector;

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

      _container.RegisterSingle<IEventAggregator, EventAggregator>();
      _container.Register<IWindowManager, WindowManager>();
      _container.RegisterSingle<IShell, ShellViewModel>();

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