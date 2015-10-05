namespace PatioBlox2016.JobPrepUI.JobVisualizer
{
  using Abstract;

  public class SectionViewModel : TreeViewItemViewModel
  {
    private readonly ISection _section;
    
    public SectionViewModel(TreeViewItemViewModel book, ISection section) 
      : base(book, true)
    {
      _section = section;
    }

    public string SectionName
    {
      get { return _section.SectionName; }
    }

    protected override void LoadChildren()
    {
      foreach (var page in _section.Pages) {
        Children.Add(new PageViewModel(this, page));
      }
      base.LoadChildren();
    }
  }
}