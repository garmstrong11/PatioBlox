namespace PatioBlox2016.Abstract
{
	public interface IColumnIndexService
	{
    int SectionIndex { get; set; }
    int ItemIndex { get; set; }
    int DescriptionIndex { get; set; }
    int PalletQtyIndex { get; set; }
    int UpcIndex { get; set; }

    int PatchAreaIndex { get; set; }
    int StoreIdIndex { get; set; }
    int StoreNameIndex { get; set; }
    int RegionIndex { get; set; }
	}
}