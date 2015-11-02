﻿namespace PatioBlox2016.DataAccess
{
  using System.Data.Entity;

  public class TestInitializer : DropCreateDatabaseAlways<PatioBloxContext>
  {
    protected override void Seed(PatioBloxContext context)
    {
      SeedHelpers.TestSeed(context);

      base.Seed(context);
    }
  }
}