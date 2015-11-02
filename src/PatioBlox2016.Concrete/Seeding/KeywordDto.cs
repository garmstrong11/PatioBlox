namespace PatioBlox2016.Concrete.Seeding
{
  using System;
  using System.Text;

  public class KeywordDto
	{
		public static int SheetIndex = 1;
		public static int WordColumnIndex = 1;
		public static int TypeColumnIndex = 2;
		
		public KeywordDto(string word, int rowIndex, string wordType)
		{
			if (string.IsNullOrWhiteSpace(word)) throw new ArgumentNullException("word");

			Word = word;

			WordType wt;
			if (Enum.TryParse(wordType, true, out wt)) {
				WordType = wt;
				return;
			}

			var sb = new StringBuilder();
			sb.AppendFormat("The WordType found in the config file on line {0} ", rowIndex);
			sb.Append("does not correspond to a member of the WordType enumeration.");

			throw new ArgumentException(sb.ToString(), "wordType");
		}

		public string Word { get; private set; }
		public WordType WordType { get; private set; }

		protected bool Equals(KeywordDto other)
		{
			return string.Equals(Word, other.Word);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((KeywordDto) obj);
		}

		public override int GetHashCode()
		{
			return (Word != null ? Word.GetHashCode() : 0);
		}

		public static bool operator ==(KeywordDto left, KeywordDto right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(KeywordDto left, KeywordDto right)
		{
			return !Equals(left, right);
		}
	}
}