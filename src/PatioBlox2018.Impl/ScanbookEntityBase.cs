namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Serialization;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public abstract class ScanbookEntityBase<TRoot, TBranch> : IScanbookEntity<TRoot, TBranch>
  {
    protected List<IPatchRow> PatchRows { get; }
    protected List<TBranch> Branches { get; }
    protected JsonSerializerSettings JsonSerializerSettings { get; }

    protected ScanbookEntityBase(IEnumerable<IPatchRow> patchRows)
    {
      PatchRows = patchRows?.ToList() ?? throw new ArgumentNullException(nameof(patchRows));

      Branches = new List<TBranch>();
      JsonSerializerSettings =
        new JsonSerializerSettings
        {
          ContractResolver = new CamelCasePropertyNamesContractResolver(),
          Formatting = Formatting.Indented
        };
    }

    public abstract int SourceRowIndex { get; }
    public abstract string Name { get; }
    public abstract TRoot Root { get; }
    public int BranchCount => Branches.Count;

    public abstract void AddBranch(IPatchRow patchRow);

    public abstract void AddBranches(IEnumerable<IPatchRow> patchRows);

    public virtual string GetJson() => JsonConvert.SerializeObject(this, JsonSerializerSettings);
  }
}