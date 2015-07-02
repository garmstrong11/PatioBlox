namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;

	public class SeedAggregate
	{
		private readonly List<KeywordDto> _keywordDtos;
		private readonly List<ExpansionDto> _expansionDtos;

		public SeedAggregate()
		{
			_keywordDtos = new List<KeywordDto>();
			_expansionDtos = new List<ExpansionDto>();
		}


		public List<KeywordDto> KeywordDtos
		{
			get { return _keywordDtos; }
		}

		public List<ExpansionDto> ExpansionDtos
		{
			get { return _expansionDtos; }
		}
	}
}