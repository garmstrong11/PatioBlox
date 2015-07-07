namespace PatioBlox2016.Concrete.Seeding
{
  using System;

  public class JobFileDto
  {
    public static int SheetId = 4;
    public static int PrinergyJobIdColumnId = 1;
    public static int FileNameColumnId = 2;

    public JobFileDto(int prinergyJobId, string fileName)
    {
      if (string.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("fileName");

      PrinergyJobId = prinergyJobId;
      FileName = fileName;
    }

    public int PrinergyJobId { get; private set; }
    public string FileName { get; private set; }

    protected bool Equals(JobFileDto other)
    {
      return PrinergyJobId == other.PrinergyJobId && string.Equals(FileName, other.FileName);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((JobFileDto) obj);
    }

    public override int GetHashCode()
    {
      unchecked {
        return (PrinergyJobId * 397) ^ FileName.GetHashCode();
      }
    }

    public static bool operator ==(JobFileDto left, JobFileDto right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(JobFileDto left, JobFileDto right)
    {
      return !Equals(left, right);
    }
  }
}