namespace PatioBlox2018.Impl
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using PatioBlox2018.Core;
  using PatioBlox2018.Core.ScanbookEntities;

  public class ScanbookPatioBlok : ScanbookEntityBase<IPage, IPatioBlok>, IPatioBlok
  {
    private IPatchRow PatchRow { get; }
    private List<IPage> Pages { get; }
    private Func<IEnumerable<IPage>, int, IPage> FindParentPage { get; }

    public ScanbookPatioBlok(IEnumerable<IPatchRow> patchRows, IEnumerable<IPage> pages) : base(patchRows)
    {
      PatchRow = PatchRows.FirstOrDefault()
                 ?? throw new ArgumentException("No row defined for the requested Patio Blok");

      if (pages == null) throw new ArgumentNullException(nameof(pages));
      Pages = new List<IPage>(pages.OrderBy(p => p.SourceRowIndex));

      if(!Pages.Any()) throw new ArgumentException("Page list cannot be empty", nameof(pages));
    }

    public override int SourceRowIndex => PatchRow.SourceRowIndex;
    public override string Name => PatchRow.Description;

    [JsonProperty("page")]
    public override IPage Root => Pages.FindLast(p => p.SourceRowIndex <= SourceRowIndex);

    public override void AddBranch(IPatchRow patchRow) { }
    public override void AddBranches(IEnumerable<IPatchRow> patchRows) { }

    public int? ItemNumber => PatchRow.ItemNumber;
    public string Vendor => PatchRow.Vendor;
    public string PalletQuantity => PatchRow.PalletQuanity;
    public string Barcode => PatchRow.Barcode;
  }
}