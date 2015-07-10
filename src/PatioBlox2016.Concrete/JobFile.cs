namespace PatioBlox2016.Concrete
{
	using System;
	using System.Collections.Generic;
	using Seeding;

  public class JobFile
  {
	  private JobFile()
	  {
		  Books = new HashSet<Book>();
	  }
    
    public JobFile(JobFileDto dto) : this()
    {
      FileName = dto.FileName;
    }

	  public JobFile(Job job, string path) : this()
	  {
		  if (job == null) throw new ArgumentNullException("job");
		  if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException("path");

		  Job = job;
		  FileName = path;
	  }

	  public int Id { get; private set; }
    public int JobId { get; set; }
	  public Job Job { get; set; }
    public string FileName { get; set; }
		public ICollection<Book> Books { get; set; } 
  }
}