namespace PatioBlox2018.Impl
{
  using System.Collections.Generic;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookSection : ScanbookEntityBase<IBook, IPage>, ISection
  {
    public ScanbookSection(IEnumerable<IPatchRow> patchRows) : base(patchRows) { }
    public override int SourceRowIndex { get; }
    public override string Name { get; }
    public override IBook Root { get; }
    public override void AddBranch(IPatchRow patchRow)
    {
      throw new System.NotImplementedException();
    }

    public override void AddBranches(IEnumerable<IPatchRow> patchRows)
    {
      throw new System.NotImplementedException();
    }
  }
}