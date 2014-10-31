namespace PatioBlox.DataImport
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Domain;

	public class StoreListImporter : PatioBlockImporter
	{
		public StoreListImporter(List<string> filePaths) : base(filePaths)
		{
		}

		public List<Store> StoreList
		{
			get
			{
				var stores = new List<Store>();

				foreach (var xlsFile in XlsFiles) {
					xlsFile.ActiveSheet = 1;
					for (var row = 2; row <= xlsFile.RowCount; row++) {
						var val = xlsFile.GetCellValue(row, 2);
						var patchName = "";

						if (val == null) continue;

						patchName = val.ToString();
						val = xlsFile.GetCellValue(row, 1);
						int id = 0;
						if (val != null) id = Convert.ToInt32(val);

						var store = new Store
							{
							Id = id, PatchName = patchName
							};
						stores.Add(store);
					}
				}
				return stores;
			}
		}

		public Dictionary<string, int> PatchStoreCounts
		{
			get
			{
				return StoreList
					.GroupBy(g => g.PatchName)
					.ToDictionary(g => g.Key, g => g.Count());
			}
		} 
	}
}