namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;

	public class Product
  {
		private Product()
		{
			Descriptions = new List<Description>();
		}

		public Product(int sku)
		{
			Id = -1;
			Sku = sku;
		}

		public int PreferredDescriptionIndex { get; set; }

		public Description PreferredDescription
		{
			get { return Descriptions[PreferredDescriptionIndex]; }
		}

		public int Sku { get; private set; }
		public int Id { get; private set; }

	  public IList<Description> Descriptions { get; set; }
  }
}