namespace PatioBlox2016.Abstract
{
  public interface IJob
  {
    int Id { get; }
    int PrinergyJobId { get; set; }
    int Year { get; set; }
    string Path { get; set; }
    ICollection<Book> Books { get; set; }
  }
}