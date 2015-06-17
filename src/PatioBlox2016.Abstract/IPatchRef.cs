namespace PatioBlox2016.Abstract
{
  public interface IPatchRef
  {
    string FilePath { get; }
    string PatchName { get; }
    int Rowindex { get; }
  }
}