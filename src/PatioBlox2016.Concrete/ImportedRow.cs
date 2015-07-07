namespace PatioBlox2016.Concrete
{
	using System;
	using Abstract;

	public class ImportedRow
	{
		protected ImportedRow() {}

		public ImportedRow(int jobId, int jobFileId, IPatchRowExtract extract)
		{
			if (extract == null) throw new ArgumentNullException("extract");

			Id = -1;
			JobId = jobId;
			JobFileId = jobFileId;

			BookName = extract.PatchName;
			RowIndex = extract.RowIndex;
			Sku = extract.Sku;
			Description = extract.Description;
			Section = extract.Section;
			PalletQuanity = extract.PalletQuanity;
			Upc = extract.Upc;
		}

		public int Id { get; private set; }
		public int JobId { get; private set; }
		public int JobFileId { get; private set; }
		public int RowIndex { get; private set; }
		public string BookName { get; private set; }

		public int Sku { get; set; }
		public string Description { get; set; }
		public string Section { get; set; }
		public string PalletQuanity { get; set; }
		public string Upc { get; set; } 
	}
}