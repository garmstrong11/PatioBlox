namespace PatioBlox2016.Concrete
{
  using System;
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

    public WordType WordType
    {
      get
      {
        WordType wordType;
        string word;

        if (Parent == null) word = Word;
        else {
          var current = Parent;
          while (current.Parent != null) current = current.Parent;
          word = current.Word;
        }

        return Enum.TryParse(word, true, out wordType) ? wordType : WordType.Name;
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