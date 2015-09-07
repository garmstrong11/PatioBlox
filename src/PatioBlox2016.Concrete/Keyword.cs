namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;

  public class Keyword
	{
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

    public string Root
    {
      get { return FindRoot(this).Word; }
    }

    public string Expansion
    {
      get
      {
        if (Parent == null) return Word;
        return Parent.Parent == null ? Word : Parent.Word;
      }
    }

    private static Keyword FindRoot(Keyword keyword)
    {
      while (true)
      {
        var current = keyword;
        if (current.Parent != null) { keyword = current.Parent; }
        else { return current; }
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

    public override string ToString()
    {
      return Word;
    }

    protected bool Equals(Keyword other)
    {
      return string.Equals(Word, other.Word);
    }

    public static bool operator ==(Keyword left, Keyword right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Keyword left, Keyword right)
    {
      return !Equals(left, right);
    }
	}
}