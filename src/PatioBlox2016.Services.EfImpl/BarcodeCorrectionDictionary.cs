namespace PatioBlox2016.Services.EfImpl
{
  using System.Collections.Generic;
  using System.Linq;
  using Concrete;
  using Contracts;
  using DataAccess;

  public class BarcodeCorrectionDictionary : RepositoryBase<BarcodeCorrection>, IBarcodeCorrectionRepository
  {
    public BarcodeCorrectionDictionary(PatioBloxContext context) : base(context)
    {
    }

    public Dictionary<string, BarcodeCorrection> GetCorrectionDictionary()
    {
      return GetAll().ToDictionary(d => d.CorrectedValue);
    }
  }
}