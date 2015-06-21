namespace PatioBlox2016.Concrete
{
  using System.IO;
  using System.Runtime.InteropServices;
  using System.Text;

  public static class Pathing
	{
		[DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int WNetGetConnection(
			[MarshalAs(UnmanagedType.LPTStr)] string localName, 
			[MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName, 
			ref int length);

		/// <summary>
		/// Given a path, returns the UNC path or the original. (No exceptions
		/// are raised by this function directly). For example, "P:\2008-02-29"
		/// might return: "\\networkserver\Shares\Photos\2008-02-09"
		/// </summary>
		/// <param name="originalPath">The path to convert to a UNC Path</param>
		/// <returns>A UNC path. If a network drive letter is specified, the
		/// drive letter is converted to a UNC or network path. If the 
		/// originalPath cannot be converted, it is returned unchanged.</returns>
		public static string GetUncPath(string originalPath)
		{
			var sb = new StringBuilder(512);
			var size = sb.Capacity;

			// look for the {LETTER}: combination ...
			if (originalPath.Length <= 2 || originalPath[1] != ':') return originalPath;

			// don't use char.IsLetter here - as that can be misleading
			// the only valid drive letters are a-z && A-Z.
			var c = originalPath[0];
			if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z')) return originalPath;

			var error = WNetGetConnection(originalPath.Substring(0, 2), sb, ref size);
			if (error != 0) return originalPath;

			var path = Path.GetFullPath(originalPath).Substring(Path.GetPathRoot(originalPath).Length);

			return Path.Combine(sb.ToString().TrimEnd(), path);
		}
	}
}
