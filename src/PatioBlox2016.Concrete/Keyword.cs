namespace PatioBlox2016.Concrete
{
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
	}
}