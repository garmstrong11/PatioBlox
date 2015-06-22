namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;
	using Abstract;

	public class BookModel
	{
		private readonly IEnumerable<IPatchRowExtract> _patchRows;

		public BookModel(string bookName, IEnumerable<IPatchRowExtract> patchRows)
		{
			_patchRows = patchRows;
			BookName = bookName;
		}

		public string BookName { get; private set; }
	}
}