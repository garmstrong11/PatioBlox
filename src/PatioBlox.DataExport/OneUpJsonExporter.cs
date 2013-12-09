namespace PatioBlox.DataExport
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Domain;
  using Newtonsoft.Json.Linq;

  public class OneUpJsonExporter
  {
    private readonly List<OneUpPatioBlock> _blox;

    public OneUpJsonExporter(List<OneUpPatioBlock> blox)
    {
      _blox = blox;
    }

    public string BloxArray
    {
      get
      {
        var jsonResult = new JArray(
        _blox.Select(ComposeOneUpJsonObject));

        return jsonResult.ToString();
      }
    }

    public bool TryGetJsonDict(out string result)
    {
      //var violators = _blox.Where(b => !b.Barcode.IsValid).ToList();
      //if (violators.Any())
      //{
      //  result = null;
      //  return false;
      //}

      var jsonResult = new JObject(
        _blox.Select(b => new JProperty(
          String.Format("{0}_{1}", b.ItemNumber.ToString(), b.Barcode.Value),
          ComposeOneUpJsonObject(b))
          )
        );

      result = jsonResult.ToString();
      return true;
    }

    public string BloxDict
    {
      get
      {
        var jsonResult = new JObject(
          _blox.Select(b => new JProperty(
            String.Format("{0}_{1}", b.ItemNumber.ToString(), b.Barcode.Value),
            ComposeOneUpJsonObject(b))
            )
          );

        return jsonResult.ToString();
      }
    }

    private static JObject ComposeOneUpJsonObject(OneUpPatioBlock blok)
    {
      return new JObject(
        new JProperty("itemNumber", blok.ItemNumber.ToString()),
        new JProperty("description", blok.Description),
        new JProperty("name", blok.Name),
        new JProperty("size", blok.Size),
        new JProperty("color", blok.Color),
        new JProperty("palletQty", blok.PalletQuantity),
        new JProperty("barcode", blok.Barcode.Value),
        new JProperty("image", string.IsNullOrWhiteSpace(blok.Image) ? null : blok.Image),
        new JProperty("approvalStatus", blok.ApprovalStatusString)
      );
    }
  }
}