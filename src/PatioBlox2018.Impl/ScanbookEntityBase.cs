namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;

  [JsonObject(MemberSerialization.OptIn)]
  public abstract class ScanbookEntityBase<TContainer, TContained> 
    : IComparable<ScanbookEntityBase<TContainer, TContained>>
  {
    protected IPatchRow PatchRow { get; }
    protected TContainer Parent { get; }
    protected SortedSet<TContained> Children { get; }

    protected ScanbookEntityBase(
      IPatchRow patchRow, Func<int, TContainer> parentFinder)
    {
      PatchRow = patchRow;
      Parent = parentFinder(PatchRow.SourceRowIndex);
      Children = new SortedSet<TContained>();
    }

    public int SourceRowIndex => PatchRow.SourceRowIndex;

    public string PatchName => PatchRow.PatchName;

    public void AddChild(TContained child) => Children.Add(child);

    public override string ToString()
    {
      return $"{GetType().Name}_{PatchName}_{SourceRowIndex}";
    }

    #region Comparison Infrastructure

    public int CompareTo(ScanbookEntityBase<TContainer, TContained> other)
    {
      if (ReferenceEquals(this, other)) return 0;
      if (ReferenceEquals(null, other)) return 1;

      var sourceRowIndexComparison = SourceRowIndex.CompareTo(other.SourceRowIndex);

      return sourceRowIndexComparison != 0 
        ? sourceRowIndexComparison 
        : string.Compare(PatchName, other.PatchName, StringComparison.OrdinalIgnoreCase);
    }

    protected bool Equals(ScanbookEntityBase<TContainer, TContained> other)
    {
      return SourceRowIndex == other.SourceRowIndex && string.Equals(PatchName, other.PatchName);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;

      return obj.GetType() == GetType() && Equals((ScanbookEntityBase<TContainer, TContained>) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (SourceRowIndex * 397) ^ PatchName.GetHashCode();
      }
    }

    #endregion
  }
}