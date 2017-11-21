namespace PatioBlox2018.Impl
{
  using System.Collections.Generic;
  using System.Configuration;
  using System.Linq;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookJob : ScanbookEntityBase<IJob, IBook>, IJob
  {
    private List<string> SourcePaths { get; }

    public ScanbookJob(IEnumerable<IPatchRow> patchRows) : base(patchRows)
    {
      SourcePaths = new List<string>();
    }

    public override int SourceRowIndex => 0;
    public override string Name => ConfigurationManager.AppSettings["JobName"];
    public override IJob Root => this;

    public override void AddBranch(IPatchRow patchRow)
    {
      throw new System.NotImplementedException();
    }

    public override void AddBranches(IEnumerable<IPatchRow> patchRows)
    {
      throw new System.NotImplementedException();
    }

    public override string GetJson()
    {
      throw new System.NotImplementedException();
    }

    public IEnumerable<string> DataSourcePaths => SourcePaths.AsEnumerable();

    public void AddDataSource(string dataSourcePath)
    {
      SourcePaths.Add(dataSourcePath);
    }
  }
}