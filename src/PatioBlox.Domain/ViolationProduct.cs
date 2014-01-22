namespace PatioBlox.Domain
{

	public class ViolationProduct : OneUpProduct
	{
		public ViolationProduct(Product blok) : base(blok)
		{
		}

		public string AppearsOn { get; set; }
	}
}