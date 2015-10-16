namespace PatioBlox2016.Abstract
{
	public interface ISettingsService
	{
		int CellsPerPage { get; set; }
    int SectionIndex { get; set; }
    int ItemIndex { get; set; }
    int DescriptionIndex { get; set; }
    int PalletQtyIndex { get; set; }
    int UpcIndex { get; set; }

    int PatchAreaIndex { get; set; }
    int StoreIdIndex { get; set; }

    string PatioBloxFactoryPath { get; set; }
    int CopiesPerStore { get; set; }
	}
}