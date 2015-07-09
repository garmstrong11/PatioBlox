namespace PatioBlox2016.Concrete
{
	public class DescriptionUsage
	{
		private DescriptionUsage() {}

		public DescriptionUsage(int bookId, int sku, int rowIndex, Description description) : this()
		{
			Id = -1; // will be updated on insertion.
			BookId = bookId;
		  Sku = sku;
		  RowIndex = rowIndex;
			Description = description;
			DescriptionId = description.Id;
		}

		public int Id { get; private set; }
		public int BookId { get; private set; }
		public int RowIndex { get; private set; }
	  public int Sku { get; private set; }
		public int DescriptionId { get; private set; }
		public Description Description { get; private set; }
	}
}