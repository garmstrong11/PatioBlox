namespace PatioBlox2016.Concrete
{
  using System;
  using System.Collections.Generic;

	public class Description
  {
		private Description()
		{
			Usages = new List<DescriptionUsage>();
		}

		public Description(int jobId, string text) : this()
    {
      if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

			Id = -1;
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

		public ICollection<DescriptionUsage> Usages { get; set; }

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