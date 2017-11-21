namespace PatioBlox2018.Impl
{
  using System.Collections.Generic;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookBook : ScanbookEntityBase<IJob, ISection>, IBook
  {
    public ScanbookBook(IEnumerable<IPatchRow> patchRows) : base(patchRows) { }
    public override int SourceRowIndex { get; }
    public override string Name { get; }
    public override IJob Root { get; }
    public override void AddBranch(IPatchRow patchRow)
    {
      throw new System.NotImplementedException();
    }

    public override void AddBranches(IEnumerable<IPatchRow> patchRows)
    {
      throw new System.NotImplementedException();
    }

    public int PageCount { get; }
  }
}