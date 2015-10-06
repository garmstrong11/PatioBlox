namespace PatioBlox2016.JobPrepUI.JobVisualizer
{
  using System.Linq;
  using System.Windows.Data;
  using PatioBlox2016.Abstract;

  public class CellSetViewModel : TreeViewItemViewModel
  {    
    public CellSetViewModel(TreeViewItemViewModel sectionVm, ISection section)
      : base(sectionVm, true)
    {
      Cells = new ListCollectionView(
        section.Cells.Select(c => new CellViewModel(c)).ToList());

      if (Cells.GroupDescriptions != null) 
        Cells.GroupDescriptions.Add(new PropertyGroupDescription("PageIndex"));
    }

    public ListCollectionView Cells { get; private set; } 
  }
}