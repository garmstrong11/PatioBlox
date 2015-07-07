namespace PatioBlox2016.Concrete
{
	using System.Collections.Generic;
	using Seeding;

  public class SeedAggregate
	{
		private readonly List<KeywordDto> _keywordDtos;
		private readonly List<ExpansionDto> _expansionDtos;
    private readonly List<JobDto> _jobDtos;
    private readonly List<JobFileDto> _jobFileDtos; 

		public SeedAggregate()
		{
			_keywordDtos = new List<KeywordDto>();
			_expansionDtos = new List<ExpansionDto>();
      _jobDtos = new List<JobDto>();
      _jobFileDtos = new List<JobFileDto>();
		}


		public List<KeywordDto> KeywordDtos
		{
			get { return _keywordDtos; }
		}

		public List<ExpansionDto> ExpansionDtos
		{
			get { return _expansionDtos; }
		}

    public List<JobDto> JobDtos
    {
      get { return _jobDtos; }
    }

    public List<JobFileDto> JobFileDtos
    {
      get { return _jobFileDtos; }
    }
	}
}