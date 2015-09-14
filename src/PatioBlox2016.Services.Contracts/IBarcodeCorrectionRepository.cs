namespace PatioBlox2016.Services.Contracts
{
  using System.Collections.Generic;
  using Abstract;
  using Concrete;

  public interface IBarcodeCorrectionRepository : IRepository<BarcodeCorrection>
  {
    Dictionary<string, BarcodeCorrection> GetCorrectionDictionary();
  }
}