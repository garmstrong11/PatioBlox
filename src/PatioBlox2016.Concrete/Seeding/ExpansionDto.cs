namespace PatioBlox2016.Concrete.Seeding
{
	using System;

	public class ExpansionDto
	{
		public static int SheetIndex = 2;
		public static int WordColumnIndex = 1;
		public static int ExpansionColumnIndex = 2;
		
		public ExpansionDto(string word, string expansion)
		{
			if (string.IsNullOrWhiteSpace(word)) throw new ArgumentNullException("word");
			if (string.IsNullOrWhiteSpace(expansion)) throw new ArgumentNullException("expansion");

			Word = word;
			Expansion = expansion;
		}

		public string Word { get; private set; }
		public string Expansion { get; private set; }

		protected bool Equals(ExpansionDto other)
		{
			return string.Equals(Word, other.Word);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((ExpansionDto) obj);
		}

		public override int GetHashCode()
		{
			return (Word != null ? Word.GetHashCode() : 0);
		}

		public static bool operator ==(ExpansionDto left, ExpansionDto right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ExpansionDto left, ExpansionDto right)
		{
			return !Equals(left, right);
		}
	}
}