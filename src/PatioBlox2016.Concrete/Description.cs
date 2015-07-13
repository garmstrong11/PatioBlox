namespace PatioBlox2016.Concrete
{
  using System;

	public class Description
  {
		private Description()
		{
		}

		public Description(int jobId, string text) : this()
    {
      if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

		  Id = 0;
      JobId = jobId;
      Text = text;
    }

		public int Id { get; private set; }
    public int JobId { get; private set; }
    public string Text { get; private set; }

    public string Vendor { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }

    protected bool Equals(Description other)
    {
      return string.Equals(Text, other.Text);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Description) obj);
    }

    public override int GetHashCode()
    {
      return Text.GetHashCode();
    }

    public static bool operator ==(Description left, Description right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Description left, Description right)
    {
      return !Equals(left, right);
    }
  }
}