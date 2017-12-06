namespace PatioBlox2018.Validators
{
  using System.Collections.Generic;
  using System.Configuration;
  using System.IO;
  using System.Linq;
  using FluentValidation;

  public class PhotoValidator : AbstractValidator<string>
  {
    private static IEnumerable<string> PhotoFileNames { get; }

    static PhotoValidator()
    {
      var factoryPath = ConfigurationManager.AppSettings["PatioBloxFactoryPath"];
      var photoDir = Path.Combine(factoryPath, "support");

      var info = new DirectoryInfo(photoDir);
      PhotoFileNames = 
        info.EnumerateFiles("*.psd")
        .Select(i => Path.GetFileNameWithoutExtension(i.FullName));
    }

    public PhotoValidator()
    {
      RuleFor(p => p)
        .Must(f => PhotoFileNames.Contains(f))
        .WithMessage("The photo for item {PropertyValue} could not be found at the path {}");
    }
  }
}