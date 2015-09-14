namespace PatioBlox2016.Concrete
{
  /// <summary>
  /// Encapsuates data needed to correct a customer-supplied barcode
  /// in the event that such a submission is incorrect or incomplete.
  /// </summary>
  public class BarcodeCorrection
  {
    public int Id { get; set; }
    public int BarcodeId { get; set; }
    public Barcode Barcode { get; set; }
    public string CorrectedValue { get; set; }
  }
}