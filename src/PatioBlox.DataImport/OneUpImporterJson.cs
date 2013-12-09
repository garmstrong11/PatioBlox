namespace PatioBlox.DataImport
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Domain;
  using Newtonsoft.Json.Linq;

  public class OneUpImporterJson : PatioBlockImporter
  {
		public OneUpImporterJson(List<string> filePaths) : base(filePaths)
		{
		}

		public String JsonData
		{
			get
			{
			  var jsonResult = new JArray();

				foreach (var xlsFile in _xlsFiles) {
					for (var row = 2; row < xlsFile.RowCount; row++) {
						var val = xlsFile.GetCellValue(row, 1);

						if(val == null) continue;
					  var blok = new JObject(new JProperty("itemNumber", val.ToString()));

						val = xlsFile.GetCellValue(row, 2);
						blok.Add(new JProperty("description", val != null ? val.ToString() : ""));

						val = xlsFile.GetCellValue(row, 3);
            blok.Add(new JProperty("name", val != null ? val.ToString() : ""));

						val = xlsFile.GetCellValue(row, 4);
            blok.Add(new JProperty("size", val != null ? val.ToString() : ""));

						val = xlsFile.GetCellValue(row, 5);
            blok.Add(new JProperty("color", val != null ? val.ToString() : ""));

						val = xlsFile.GetCellValue(row, 6);
					  blok.Add(new JProperty("palletQty", val != null ? val.ToString() : ""));

						val = xlsFile.GetCellValue(row, 7);
					  var bc = val != null ? new Barcode(val.ToString()) : new Barcode("");

            blok.Add(new JProperty("barcode",  bc.IsValid ? bc.Value : null));

						val = xlsFile.GetCellValue(row, 8);
            blok.Add(new JProperty("image", val != null ? val.ToString() : null));

						val = xlsFile.GetCellValue(row, 9);
            blok.Add(new JProperty("approvalStatus", val != null ? val.ToString() : ""));

						jsonResult.Add(blok);
					}
				}

			  return String.Format("var products = {0}", jsonResult.ToString());
			}
		}
	} 
}