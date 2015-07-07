namespace PatioBlox2016.Concrete
{
  using System;

  public class Description
  {
    public Description(int jobId, string rawText)
    {
      if (string.IsNullOrWhiteSpace(rawText)) throw new ArgumentNullException("rawText");

      JobId = jobId;
      RawText = rawText;
    }

    public int JobId { get; private set; }
    public string RawText { get; private set; }

    public string Vendor { get; set; }
    public string Size { get; set; }
    public string Color { get; set; }
    public string Name { get; set; }

    protected bool Equals(Description other)
    {
      return string.Equals(RawText, other.RawText);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((Description) obj);
    }

    public override int GetHashCode()
    {
      return RawText.GetHashCode();
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