namespace PatioBlox.Domain
{
	using System.Linq;
	using System.Text;
	using Properties;

	public static class JobFolders
	{
		public static string GetJsxString()
		{
			var sb = new StringBuilder();
			var def = Settings.Default;
			const string term = ";";

			sb.AppendLine("function JobFolders() {");
			sb.AppendLine("\tvar root = \"" + ConvertPathToIndd(def.FactoryRootPath) + term);
			sb.AppendLine();
			sb.AppendLine("\tthis.scriptPath = root + \"" + ConvertPathToIndd(def.SubpathJsx) + term);
			sb.AppendLine();
			sb.AppendLine("\tthis.slotInddPath = root + \"" + ConvertPathToIndd(def.SubPathOutputSlotIndd) + term);
			sb.AppendLine("\tthis.slotPdfPath = root + \"" + ConvertPathToIndd(def.SubPathOutputSlotPdf) + term);
			sb.AppendLine("\tthis.slotSupportPath = root + \"" + ConvertPathToIndd(def.SubpathSupport) + term);
			sb.AppendLine("\tthis.slotTemplatePath = root + \"" + ConvertPathToIndd(def.SubPathTemplateSlot) + term);
			sb.AppendLine();
			sb.AppendLine("\tthis.compositeInddPath = root + \"" + ConvertPathToIndd(def.SubPathOutputCompositeIndd) + term);
			sb.AppendLine("\tthis.compositePdfPath = root + \"" + ConvertPathToIndd(def.SubPathOutputCompositePdf) + term);
			sb.AppendLine("\tthis.compositeSupportPath = root + \"" + ConvertPathToIndd(def.SubPathOutputSlotPdf) + term);
			sb.AppendLine("\tthis.compositeTemplatePath = root + \"" + ConvertPathToIndd(def.SubPathTemplateComposite) + term);
			sb.Append("}");

			var result = sb.ToString();
			return result;
		}

		private static string ConvertPathToIndd(string path)
		{
			var isLocal = path.Contains(":");

			var split = path.Split('\\')
				.Select(p => p.Replace(":", ""))
				.ToArray();

			var str = isLocal ? "/" : "";

			str += string.Join("/", split);
			str += @"/""";

			return str;
		}
	}
}