namespace PatioBlox2016.Concrete
{
	public class Expansion
	{
		public Expansion()
		{}

		public Expansion(ExpansionDto dto)
		{
			Word = dto.Word;
		}
		
		public int Id { get; set; }
		public string Word { get; set; }
		public int KeywordId { get; set; }

		public Keyword Keyword { get; set; }
	}
}