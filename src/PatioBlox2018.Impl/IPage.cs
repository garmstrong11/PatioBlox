namespace PatioBlox2018.Impl {
  using System.Collections.Generic;

  public interface IPage {
    List<ScanbookPatioBlock> PatioBlocks { get; }
    ScanbookSection Section { get; }
    int BlockCount { get; }
  }
}