namespace PatioBlox2016.Abstract
{
  public interface IUpcReplacement
  {
    int Id { get; set; }
    string InvalidUpc { get; set; }
    string Replacement { get; set; }
  }
}