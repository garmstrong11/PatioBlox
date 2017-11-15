namespace PatioBlox2016.Extractor
{
  using System.Configuration;
  using PatioBlox2018.Core;

  public class FlexcelColumnIndexService : IColumnIndexService
  {
    public int SectionIndex => 
      int.Parse(ConfigurationManager.AppSettings["SectionIndex"]);

    public int ItemNumberIndex => 
      int.Parse(ConfigurationManager.AppSettings["ItemIndex"]);

    public int DescriptionIndex => 
      int.Parse(ConfigurationManager.AppSettings["DescriptionIndex"]);

    public int PalletQtyIndex => 
      int.Parse(ConfigurationManager.AppSettings["PalletQtyIndex"]);

    public int BarcodeIndex => 
      int.Parse(ConfigurationManager.AppSettings["UpcIndex"]);

    public int PageOrderIndex => 
      int.Parse(ConfigurationManager.AppSettings["PageOrderIndex"]);

    public int VendorIndex => 
      int.Parse(ConfigurationManager.AppSettings["VendorIndex"]);

    public int PatchAreaIndex => 
      int.Parse(ConfigurationManager.AppSettings["PatchAreaIndex"]);

    public int StoreIdIndex => 
      int.Parse(ConfigurationManager.AppSettings["StoreIdIndex"]);
  }
}