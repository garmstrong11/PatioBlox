namespace PatioBlox2018.Impl.Barcodes {
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using LanguageExt;
  using LanguageExt.ClassInstances.Pred;
  using static LanguageExt.Prelude;

  public class Barcode
  {
    private Validation<Error, string> Validation { get; }

    public Barcode(Validation<Error, string> validation)
    {
      Validation = validation;
    }

    //private static Option<int> TryGetCheckDigit(Option<string> candidate)
    //{
    //  if (candidate.IsNone) return None;
    //  if (candidate)
    //}

    //private static Option<int> GetLastDigit(Option<string> source)
    //{
    //  int result;
    //  source.Match(
    //    None: () => None,
    //    Some: src => 
    //    {
    //      var last = src.ToCharArray().Last();
    //      digit = Convert.ToInt32(char.GetNumericValue(last));
    //      return digit == -1 ? None : Some(src);
    //    }
    //  );
    //}

    //private static Option<int> CalculateCheckDigit(string candidate)
    //{
    //  var len = candidate.Length;
    //  var multiplier =
    //    _factorMap.Find(len)
    //      .IfNone(() => failwith<IEnumerable<int>>($"Can't find a factorMap for key '{len}'"));

    //  var digits = candidate.GetBarcodeDigits(multiplier);
    //  var calculatedCheckDigit = 10 - (digits.Sum() % 10) % 10;

    //  return Some(calculatedCheckDigit);
    //}

    //private static Map<int, IEnumerable<int>> _factorMap 
    //  = ((12, GetUpcaMultipliers()), (13, GetEan13Multipliers()));

    //public static IEnumerable<int> GetUpcaMultipliers()
    //{
    //  yield return 1;
    //  yield return 3;
    //}

    //private static IEnumerable<int> GetEan13Multipliers()
    //{
    //  yield return 3;
    //  yield return 1;
    //}
  }
}