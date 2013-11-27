namespace PatioBlox.DataImport
{
	using System.Collections.Generic;
	using System.Linq;
	using Domain;

	public class PatchMatcher
	{
		private readonly IEnumerable<Patch> _patches;
		private Dictionary<string, bool> _matchedDict; 

		public PatchMatcher(IEnumerable<Patch> patches)
		{
			_patches = patches;
			_matchedDict = _patches
				.Select(p => new { p.Name, IsMatched = false })
				.ToDictionary(k => k.Name, v => v.IsMatched);
		}
		
		public List<List<string>> MatchedPatches
		{
			get
			{
				var result = new List<List<string>>();

				foreach (var patch in _patches) {
					if(_matchedDict[patch.Name]) continue;
					var matchList = FindMatches(patch);
					if (matchList != null) {
						result.Add(matchList);
					}
				}

				return result;
			}
			
		}

		private List<string> FindMatches(Patch patch)
		{
			var result = new List<string>();
			result.Add(patch.Name);

			foreach (var mPatch in _patches) {
				if (mPatch.Name == patch.Name) continue;
				if (_matchedDict[mPatch.Name]) continue;
				if (!patch.SectionsEqual(mPatch)) continue;

				result.Add(mPatch.Name);
				_matchedDict[mPatch.Name] = true;
			}

			return result.Count > 1 ? result : null;
		} 
	}
}