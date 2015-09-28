namespace PatioBlox2016.Services.Contracts
{
  using System.Collections.Generic;
  using PatioBlox2016.Abstract;

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
    void PersistUpcReplacementsForInvalidUpcs();

    /// <summary>
    /// Discover new Descriptions from the ExtractionResult
    /// and add them to the database as unresolved Descriptions.
    /// </summary>
    void PersistNewDescriptions();

    /// <summary>
    /// Discover new Keywords from the ExtractionResult
    /// and add the to the database with Parent keyword "New"
    /// </summary>
    void PersistNewKeywords();
  }
}