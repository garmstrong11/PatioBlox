namespace PatioBlox2016.Concrete
{
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
      Id = -1;
      FileName = dto.FileName;
    }

    public int Id { get; private set; }
    public int JobId { get; set; }
    public string FileName { get; set; }
		public ICollection<Book> Books { get; set; } 
  }
}