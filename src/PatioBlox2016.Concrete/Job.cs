namespace PatioBlox2016.Concrete
{
	using System;
	using System.Collections.Generic;
  using Seeding;

  public class Job
  {
    private Job()
    {
      JobFiles = new List<JobFile>();
    }

    public Job(JobDto dto) : this()
    {
      Id = 0;
      PrinergyJobId = dto.PrinergyJobId;
      Year = dto.Year;
      Path = dto.Path;
    }

	  public Job(int prinergyJobId, int year, string path) : this()
	  {
		  if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");

		  PrinergyJobId = prinergyJobId;
		  Year = year;
		  Path = path;
	  }

	  public int Id { get; private set; }
    public int PrinergyJobId  { get; set; }
    public int Year { get; set; }
    public string Path { get; set; }

    public List<JobFile> JobFiles { get; set; }
  }
}