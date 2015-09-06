namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;

  public class Keyword
	{
    public Keyword()
    {
      Abbreviations = new List<Keyword>();
    }

    public Keyword(string word) : this()
    {
      Word = word;
    }

    public int Id { get; set; }

    public string Word { get; set; }

    public WordType WordType { get; set; }

    public int? ExpansionId { get; set; }

    public Keyword Expansion { get; set; }

    public ICollection<Keyword> Abbreviations { get; set; }

    public override int GetHashCode()
    {
      return Word.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Keyword) obj);
    }

    protected bool Equals(Keyword other)
    {
      return string.Equals(Word, other.Word);
    }
	}
}