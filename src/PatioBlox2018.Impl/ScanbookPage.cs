namespace PatioBlox2018.Impl
{
  using System.Collections.Generic;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;
  public class ScanbookPage : ScanbookEntityBase<ISection, IPatioBlok>, IPage
  {
    public ScanbookPage(IEnumerable<IPatchRow> patchRows) : base(patchRows) { }
    public override int SourceRowIndex { get; }
    public override string Name { get; }

    IBook IScanbookEntity<IBook, IPatioBlok>.Root
    {
      get { throw new System.NotImplementedException(); }
    }

    public override ISection Root { get; }
    public override void AddBranch(IPatchRow patchRow)
    {
      throw new System.NotImplementedException();
    }

    public override void AddBranches(IEnumerable<IPatchRow> patchRows)
    {
      throw new System.NotImplementedException();
    }

    public string Header { get; }
  }
}