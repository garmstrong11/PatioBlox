namespace PatioBlox2016.JobPrepUI.JobVisualizer
{
  using System.Collections.ObjectModel;
  using System.Linq;
  using Abstract;

  public class PageViewModel : TreeViewItemViewModel
  {
    private readonly IPage _page;

    public PageViewModel(TreeViewItemViewModel parent, IPage page)
      : base(parent, true)
    {
      _page = page;
    }

    public string PageHeader
    {
      get { return string.Format("Page {0} ({1} cells)", _page.Index, _page.Cells.Count); }
    }

    //protected override void LoadChildren()
    //{
    //  foreach (var cell in _page.Cells) {
    //    Children.Add(new CellViewModel(this, cell));
    //  }
    //}

    public ReadOnlyCollection<ICell> Cells
    {
      get { return new ReadOnlyCollection<ICell>(_page.Cells.ToList());}
    } 
  }
}