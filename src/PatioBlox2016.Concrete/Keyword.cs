namespace PatioBlox2016.Concrete
{
  using System.Collections.Generic;
  using Seeding;

  public class Keyword
	{
		public Keyword()
		{}

		public Keyword(KeywordDto dto)
		{
			Word = dto.Word;
			WordType = dto.WordType;
		}
		
		public int Id { get; set; }
		public string Word { get; set; }
		public WordType WordType { get; set; }

    public int? ExpansionId { get; set; }
    public Keyword Expansion { get; set; }

    public ICollection<Keyword> Abbreviations { get; set; } 
	}
}