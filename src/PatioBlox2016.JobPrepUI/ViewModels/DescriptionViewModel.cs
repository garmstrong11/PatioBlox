namespace PatioBlox2016.JobPrepUI.ViewModels
{
  using System;
  using Caliburn.Micro;
  using Concrete;

  public class DescriptionViewModel : PropertyChangedBase
  {
    private readonly Description _description;

    public DescriptionViewModel(Description description)
    {
      _description = description;
    }

    public int Id
    {
      get { return _description.Id; }
    }

    public string Text
    {
      get { return _description.Text; }
    }

    public string Vendor
    {
      get { return _description.Vendor; }
      set
      {
        if (value == _description.Vendor) return;
        _description.Vendor = value;
        NotifyOfPropertyChange(() => Vendor);
      }
    }

    public Description Description
    {
      get { return _description; }
    }

    public string Size
    {
      get { return _description.Size; }
      set
      {
        if (value == _description.Size) return;
        _description.Size = value;
        NotifyOfPropertyChange(() => Size);
      }
    }

    public string Color
    {
      get { return _description.Color; }
      set
      {
        if (value == _description.Color) return;
        _description.Color = value;
        NotifyOfPropertyChange(() => Color);
      }
    }

    public string Name
    {
      get { return _description.Name; }
      set
      {
        if (value == _description.Name) return;
        _description.Name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

    public DateTime InsertDate
    {
      get { return _description.InsertDate; }
    }
  }
}