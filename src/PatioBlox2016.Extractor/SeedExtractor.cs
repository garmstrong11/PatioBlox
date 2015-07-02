namespace PatioBlox2016.Extractor
{
	using System.Collections.Generic;
	using System.IO.Abstractions;
	using System.Linq;
	using Abstract;
	using Concrete;

	public class SeedExtractor : ExtractorBase<SeedAggregate>, IKeywordExractor
	{
		public SeedExtractor(IDataSourceAdapter adapter, IFileSystem fileSystem) 
			: base(adapter, fileSystem)
		{
		}

		private IEnumerable<KeywordDto> ExtractKeywords()
		{
			XlAdapter.ActiveSheet = KeywordDto.SheetIndex;
			var rowCount = XlAdapter.RowCount;
			var set = new HashSet<KeywordDto>();

			for (var row = 2; row <= rowCount; row++) {
				var word = XlAdapter.ExtractString(row, KeywordDto.WordColumnIndex);
				var wordType = XlAdapter.ExtractString(row, KeywordDto.TypeColumnIndex);

				set.Add(new KeywordDto(word, row, wordType));
			}

			return set.AsEnumerable();
		}

		private IEnumerable<ExpansionDto> ExtractExpansions()
		{
			XlAdapter.ActiveSheet = ExpansionDto.SheetIndex;
			var rowCount = XlAdapter.RowCount;
			var set = new HashSet<ExpansionDto>();

			for (var row = 2; row <= rowCount; row++) {
				var word = XlAdapter.ExtractString(row, ExpansionDto.WordColumnIndex);
				var expansion = XlAdapter.ExtractString(row, ExpansionDto.ExpansionColumnIndex);

				set.Add(new ExpansionDto(word, expansion));
			}

			return set.AsEnumerable();
		} 

		public override IEnumerable<SeedAggregate> Extract()
		{
			var seedAggregate = new SeedAggregate();
			var result = new List<SeedAggregate> {seedAggregate};

			seedAggregate.KeywordDtos.AddRange(ExtractKeywords());
			seedAggregate.ExpansionDtos.AddRange(ExtractExpansions());

			return result;
		}
	}
}