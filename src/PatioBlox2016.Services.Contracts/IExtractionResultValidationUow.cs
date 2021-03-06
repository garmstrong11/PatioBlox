﻿namespace PatioBlox2016.Services.Contracts
{
  using System.Collections.Generic;
  using System.Collections.ObjectModel;
  using System.Linq;
  using Abstract;
  using Concrete;

  /// <summary>
  /// Supports the data needs of ExtractionResultViewModel
  /// </summary>
  public interface IExtractionResultValidationUow
  {
    /// <summary>
    /// Fetches the unique products from the ExtractionResult
    /// </summary>
    /// <returns></returns>
    IEnumerable<IProduct> GetProducts();

    /// <summary>
    /// Discover new invalid upcs from the ExtractionResult 
    /// and add them to the database in their invalid state.
    /// </summary>
    void PersistNewUpcReplacements();

    /// <summary>
    /// Discover new Descriptions from the ExtractionResult
    /// and add them to the database as unresolved Descriptions.
    /// </summary>
    void PersistNewDescriptions();

    /// <summary>
    /// Discover new Keywords from the ExtractionResult
    /// and add them to the database with Parent keyword "New"
    /// </summary>
    void PersistNewKeywords();

    /// <summary>
    /// Get an ObservableCollection of Keywords tracked by the
    /// PatioBloxContext
    /// </summary>
    /// <returns></returns>
    ObservableCollection<Keyword> GetKeywords();

    /// <summary>
    /// Add a Keyword to the PatioBloxContext
    /// </summary>
    /// <param name="keyword"></param>
    void AddKeyword(Keyword keyword);

      /// <summary>
    /// Get a list of top-level keywords that represent the
    /// all the available WordType variants.
    /// </summary>
    /// <returns></returns>
    List<Keyword> GetRootKeywords();

    /// <summary>
    /// Get a list of level two Keywords that represent
    /// the range of expansions
    /// </summary>
    /// <returns></returns>
    List<Keyword> GetParentKeywords();

    /// <summary>
    /// Gets the default parent Keyword "NEW"
    /// </summary>
    /// <returns></returns>
    Keyword GetDefaultParent();

    /// <summary>
    /// Gets a list of Keywords that have the default parent
    /// </summary>
    /// <returns></returns>
    List<Keyword> GetNewKeywords();

    /// <summary>
    /// Gets a list of Descriptions that contain a given word
    /// </summary>
    /// <param name="word">The key for finding Description usages.</param>
    /// <returns></returns>
    List<string> GetUsagesForWord(string word);

    /// <summary>
    /// Gets a dictionary of Keywords, indexed by word.
    /// </summary>
    /// <returns></returns>
    Dictionary<string, Keyword> GetKeywordDict(); 

    /// <summary>
    /// Exposes a method that saves changes that have been made to the context.
    /// </summary>
    int SaveChanges();

    /// <summary>
    /// Gets the count of Patches in the Extraction.
    /// </summary>
    /// <returns></returns>
    int GetPatchCount();

    /// <summary>
    /// Gets the count of unique description texts in the Extraction.
    /// </summary>
    int GetUniqueDescriptionCount();

    /// <summary>
    /// Gets a list of descriptions whose Vendor, 
    /// Size, Color, and Name have not been set.
    /// </summary>
    /// <returns></returns>
    List<Description> GetUnresolvedDescriptions();

    /// <summary>
    /// Gets a dictionary of Descriptions indexed by Text property.
    /// </summary>
    /// <returns></returns>
    Dictionary<string, int> GetDescriptionTextToIdDict();

    /// <summary>
    /// Gets a list of the unique skus in the Extraction.
    /// </summary>
    /// <returns></returns>
    IEnumerable<int> GetUniqueSkus();

    /// <summary>
    /// Gets a sequence of Products that are duplicated on a patch
    /// </summary>
    /// <returns></returns>
    IEnumerable<IPatchProductDuplicate> GetPatchProductDuplicates();

    /// <summary>
    /// Gets a sequence of PatchRowExtracts, grouped by Patch name.
    /// </summary>
    IEnumerable<IGrouping<string, IPatchRowExtract>> GetBookGroups();

    /// <summary>
    /// Gets a sequence of Descriptions specific to this job.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Description> GetDescriptionsForJob();

    /// <summary>
    /// Gets a dictionary of UpcReplacements indexed by incorrect
    /// Upc value and referencing the corrected value.
    /// </summary>
    /// <returns></returns>
    IDictionary<string, string> GetUpcReplacementDictionary();
  }
}