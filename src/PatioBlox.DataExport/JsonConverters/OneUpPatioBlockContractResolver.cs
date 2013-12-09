namespace PatioBlox.DataExport.JsonConverters
{
  using System.Collections.Generic;
  using System.Reflection;
  using Domain;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Serialization;

  public class OneUpPatioBlockContractResolver : DefaultContractResolver
  {
    public static readonly OneUpPatioBlockContractResolver Instance =
      new OneUpPatioBlockContractResolver();

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
      var property = base.CreateProperty(member, memberSerialization);
      //if (property.DeclaringType != typeof (OneUpPatioBlock)) return property;
      var blackList = new List<string>
                      {
                        "Id",
                        "ApprovalStatus",
                        "Patch",
                        "SectionId",
                        "Section",
                        "Index"
                      };

      //if (property.PropertyName == "Section") property.ShouldSerialize = p => false;
      if (blackList.Contains(property.PropertyName)) property.Ignored = true;

      //if (property.PropertyName == "Barcode")
      //{
      //  property.ValueProvider = CreateMemberValueProvider(member)
      //  {
      //    return member.
      //  }
      //}

      return property;
    }
  }
}