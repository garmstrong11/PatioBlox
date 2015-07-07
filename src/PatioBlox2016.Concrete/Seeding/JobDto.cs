namespace PatioBlox2016.Concrete.Seeding
{
  using System;

  public class JobDto
  {
    public static int SheetIndex = 3;
    public static int PrinergyJobIdColumnIndex = 1;
    public static int YearColumnIndex = 2;
    public static int PathColumnIndex = 3;

    public JobDto(int prinergyJobId, int year, string path)
    {
      if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");

      PrinergyJobId = prinergyJobId;
      Year = year;
      Path = path;
    }

    public int PrinergyJobId { get; private set; }
    public int Year { get; private set; }
    public string Path { get; private set; }

    protected bool Equals(JobDto other)
    {
      return PrinergyJobId == other.PrinergyJobId;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      return obj.GetType() == GetType() && Equals((JobDto) obj);
    }

    public override int GetHashCode()
    {
      return PrinergyJobId;
    }

    public static bool operator ==(JobDto left, JobDto right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(JobDto left, JobDto right)
    {
      return !Equals(left, right);
    }
  }
}