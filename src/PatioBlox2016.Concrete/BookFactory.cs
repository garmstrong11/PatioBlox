namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;
	using Abstract;

	public class BookFactory
	{
		private readonly IJobFolders _jobFolders;

		public BookFactory(IJobFolders jobFolders)
		{
			_jobFolders = jobFolders;
		}

	}
}