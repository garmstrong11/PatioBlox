namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  [JsonObject(MemberSerialization.OptIn)]
  public abstract class ScanbookEntityBase<TContainer, TContained>
  {
    protected IEnumerable<IPatchRow> PatchRows { get; }
    protected TContainer Parent { get; }
    protected List<TContained> Children { get; }

    protected ScanbookEntityBase(
      IEnumerable<IPatchRow> patchRows, Func<int, TContainer> parentFinder)
    {
      PatchRows = patchRows;
      Parent = parentFinder(PatchRows.First().SourceRowIndex);
      Children = new List<TContained>();
    }

    internal void AddChild(TContained child)
    {
      Children.Add(child);
    }
  }
}