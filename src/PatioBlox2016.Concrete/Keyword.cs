namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;

  public class Keyword
  {
    public static readonly string ColorKey = "COLOR";
    public static readonly string VendorKey = "VENDOR";
    public static readonly string NameKey = "NAME";
    public static readonly string SizeKey = "SIZE";
    public static readonly string NewKey = "NEW";
    
    public Keyword()
    {
      Members = new List<Keyword>();
    }

    public Keyword(string word) : this()
    {
      Word = word;
    }

    public int Id { get; set; }
    public string Word { get; set; }
    public int? ParentId { get; set; }
    public Keyword Parent { get; set; }

    public ICollection<Keyword> Members { get; set; }

    public string RootWord
    {
      get
      {
        if (Parent == null) return Word;

        var current = Parent;
        while (current.Parent != null) {
          current = current.Parent;
        }

        return current.Word;
      }
    }

    public string Expansion
    {
      get
      {
        // If root keyword, simply return Word...
        if (Parent == null) return Word;

        // If not abbreviated (level2), return Word 
        // else (level3) return Parent.Word...
        return Parent.Parent == null ? Word : Parent.Word;
      }
    }

    public override int GetHashCode()
    {
      return Word.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Keyword) obj);
    }

    protected bool Equals(Keyword other)
    {
      return string.Equals(Word, other.Word);
    }

    public override string ToString()
    {
      return Word;
    }
	}
}