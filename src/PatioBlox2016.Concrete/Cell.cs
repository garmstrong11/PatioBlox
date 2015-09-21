﻿namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using PatioBlox2016.Abstract;
  using PatioBlox2016.Concrete.Exceptions;

  public class Cell
  {
    public Cell(int sourceRowIndex, int sku, string palletQty, string description)
    {
      SourceRowIndex = sourceRowIndex;
      Sku = sku;
      PalletQty = palletQty;
      Description = description;
      //Page = page;
    }

    public Cell(IPatchRowExtract extract)
    {
      SourceRowIndex = extract.RowIndex;
      Sku = extract.Sku;
      PalletQty = extract.PalletQuanity;
      Description = extract.Description;
    }
    
    public int SourceRowIndex { get; private set; }
    public int Sku { get; private set; }
    public string Description { get; private set; }
    public string PalletQty { get; private set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public string Name { get; set; }
    public string Upc { get; set; }
    public Section Section { get; set; }

    public void FindSection(IEnumerable<Section> sections)
    {
      var section = sections
        .OrderBy(s => s.SourceRowIndex)
        .LastOrDefault(s => s.SourceRowIndex <= SourceRowIndex);

      if (section != null) {
        Section = section;
        section.AddCell(this);
        
        return;
      }

      // Handle section not found...
      var message = string.Format(
        "Unable to find a Section for the Cell at index {0}, Sku: {1}, Description: {2}",
        SourceRowIndex, Sku, Description);

      var exception = new CellConstructionException(SourceRowIndex, Sku, Description, message);

      throw exception;
    }

	  public string Image
	  {
			get { return string.Format("{0}.psd", Sku); }
	  }

    protected bool Equals(Cell other)
    {
      return Sku == other.Sku
             && string.Equals(PalletQty, other.PalletQty)
             && string.Equals(Upc, other.Upc)
             && string.Equals(Description, other.Description);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Cell) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        var hashCode = Sku;
        hashCode = (hashCode*397) ^ (PalletQty != null ? PalletQty.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ (Upc != null ? Upc.GetHashCode() : 0);
        hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
        return hashCode;
      }
    }

    public override string ToString()
    {
      return string.Format("{0}_{1}", SourceRowIndex, Description);
    }

    public string ToJsxString(int indentCount)
    {
      const string fmt =
        "{{ 'sku' : {0}, 'upc' : '{1}', 'size' : '{2}', 'color' : '{3}', 'name' : '{4}', 'palletQty' : '{5}' }}";

      return string.Format(fmt, Sku, Upc, Size, Color, Name, PalletQty).Indent(indentCount);
    }
  }
}