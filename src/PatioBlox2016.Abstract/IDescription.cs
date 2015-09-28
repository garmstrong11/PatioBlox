namespace PatioBlox2016.Abstract
{
  using System;

  public interface IDescription
  {
    DateTime InsertDate { get; set; }
    int Id { get; }
    string Text { get; }
    string Vendor { get; set; }
    string Size { get; set; }
    string Color { get; set; }
    string Name { get; set; }

    bool IsUnresolved { get; }
  }
}