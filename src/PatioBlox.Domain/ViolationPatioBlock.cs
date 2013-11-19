namespace PatioBlox.Domain
{

	public class ViolationPatioBlock : OneUpPatioBlock
	{
		public ViolationPatioBlock(PatioBlock blok) : base(blok)
		{
		}

		public string AppearsOn { get; set; }
	}
}