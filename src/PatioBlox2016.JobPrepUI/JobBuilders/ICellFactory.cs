﻿namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using Abstract;
  using Concrete;

  public interface ICellFactory
  {
    Cell CreateCell(Page page, IPatchRowExtract extract);
  }
}