﻿namespace PatioBlox2016.JobPrepUI.JobBuilders
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using Abstract;
  using Concrete;

  public class Job : IJob
  {
    private readonly IBookFactory _bookFactory;
    private readonly IJobFolders _jobFolders;
    private readonly List<IBook> _books;
    private readonly List<IDescription> _descriptions;
    private readonly List<IProduct> _products; 

    /// <summary>
    /// The globally accessible name of the job's data file
    /// </summary>
    public static readonly string JobDataFileName = "JobData.jsx";
    
    public Job(IBookFactory bookFactory, IJobFolders jobFolders)
    {
      if (bookFactory == null) throw new ArgumentNullException("bookFactory");

      _bookFactory = bookFactory;
      _jobFolders = jobFolders;
      _books = new List<IBook>();
      _descriptions = new List<IDescription>();
      _products = new List<IProduct>();
    }

    public IReadOnlyCollection<IBook> Books
    {
      get { return _books.AsReadOnly();}
    }

    public void PopulateBooks(IEnumerable<IGrouping<string, IPatchRowExtract>> bookGroups)
    {
      var books = bookGroups
        .Select(bg => _bookFactory.CreateBook(this, bg.Key, bg));

      _books.AddRange(books);
    }

    public void ClearBooks()
    {
      _books.Clear();
    }

    public void ClearDescriptions()
    {
      _descriptions.Clear();
    }

    public IReadOnlyCollection<IProduct> Products
    {
      get { return _products.AsReadOnly(); }
    }

    public void PopulateProducts(IEnumerable<IProduct> products)
    {
      _products.AddRange(products);
    }

    public void ClearProducts()
    {
      _products.Clear();
    }

    public void AddDescriptionRange(IEnumerable<IDescription> descriptions)
    {
      _descriptions.AddRange(descriptions);
    }

    public IReadOnlyCollection<IDescription> Descriptions
    {
      get { return _descriptions.AsReadOnly(); }
    }

    public string ToJsxString(int indentLevel)
    {
      var sb = new StringBuilder();
      var contentLevel = indentLevel + 1;

      sb.AppendLine(_jobFolders.ToJsxString(indentLevel));

      sb.AppendLine("var patches = {".Indent(indentLevel));

      var books = Books.Select(b => b.ToJsxString(contentLevel));
      var bookStrings = string.Join(",\n", books);

      sb.AppendLine(bookStrings);
      sb.AppendLine("};".Indent(indentLevel));

      sb.AppendLine("\nvar descriptions = {".Indent(indentLevel));

      var descriptions = Descriptions
        .Select(d => d.ToJsxString(contentLevel));

      var descText = string.Join(",\n", descriptions);

      sb.AppendLine(descText);
      sb.AppendLine("};".Indent(indentLevel));

      sb.AppendLine("\nvar products = [".Indent(indentLevel));

      var products = Products
        .OrderBy(p => p.Sku)
        .Select(p => p.ToJsxString(contentLevel));

      var prodText = string.Join(",\n", products);

      sb.AppendLine(prodText);
      sb.AppendLine("];".Indent(indentLevel));

      return sb.ToString();
    }
  }
}