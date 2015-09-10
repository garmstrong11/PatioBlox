namespace PatioBlox2016.DataAccess
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.CodeAnalysis;
  using System.IO.Abstractions;
  using System.Linq;
  using PatioBlox2016.Concrete;
  using PatioBlox2016.Extractor;

  public static class SeedHelpers
  {
    public static IEnumerable<Description> GetDescriptionSeeds()
    {
      var dapter = new FlexCelDataSourceAdapter();
      var fileSystem = new FileSystem();

      var extractor = new DescriptionExtractor(dapter, fileSystem);
      var seedPath = fileSystem.Path.Combine(GetSeedPath(), "Descriptions_2015.xlsx");
      extractor.Initialize(seedPath);

      var descriptions = extractor.Extract().Select(d => new Description(d.Text)
        {
          Color = d.Color,
          Name = d.Name,
          Size = d.Size,
          Vendor = d.Vendor,
          InsertDate = new DateTime(2015, 10, 31)
        });

      return descriptions;
    }

    private static string GetSeedPath()
    {
      var nameDict = new Dictionary<string, string>
                      {
                        {"PRANK", @"C:\Users\gma\Dropbox\PatioBlox\TestSeed"},
                        {"GARMSTRONG", @"C:\Users\garmstrong\Documents\My Dropbox\PatioBlox\TestSeed"},
                        {"WS01IT-GARMSTRO", @"C:\Users\garmstrong\Dropbox\PatioBlox\TestSeed"}
                      };
      var machine = Environment.MachineName;

      string path;
      if (nameDict.TryGetValue(machine, out path)) return path;

      throw new InvalidOperationException("Seed path can't be found. Check your Dropbox path.");
    }

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public static void SeedKeywords(PatioBloxContext context)
    {
      var rootSeeds = Enum.GetNames(typeof(WordType))
        .Select(w => new Keyword(w.ToUpper()))
        .ToList();

      var color = rootSeeds.Single(k => k.Word == "COLOR");
      var vendor = rootSeeds.Single(k => k.Word == "VENDOR");
      var name = rootSeeds.Single(k => k.Word == "NAME");
      var size = rootSeeds.Single(k => k.Word == "SIZE");

      // Calling save changes after each so they will appear 
      // in the db in a specific order.
      context.Keywords.Add(name);
      context.SaveChanges();
      context.Keywords.Add(color);
      context.SaveChanges();
      context.Keywords.Add(vendor);
      context.SaveChanges();
      context.Keywords.Add(size);
      context.SaveChanges();

			var beveled = context.Keywords.Add(new Keyword("BEVELED") {Parent = name});
      var marquette = context.Keywords.Add(new Keyword("MARQUETTE") {Parent = name});
      var countryside = context.Keywords.Add(new Keyword("COUNTRYSIDE") {Parent = name});
      var fourcobble = context.Keywords.Add(new Keyword("FOURCOBBLE") {Parent = name});
      var frederick = context.Keywords.Add(new Keyword("FREDERICK") {Parent = name});
      var weathered = context.Keywords.Add(new Keyword("WEATHERED") {Parent = name});
      var side = context.Keywords.Add(new Keyword("SIDE") {Parent = name});
      var alameda = context.Keywords.Add(new Keyword("ALAMEDA") {Parent = name});
      var antique = context.Keywords.Add(new Keyword("ANTIQUE") {Parent = name});
      var aspen = context.Keywords.Add(new Keyword("ASPEN") {Parent = name});
      var austin = context.Keywords.Add(new Keyword("AUSTIN") {Parent = name});
      var basalt = context.Keywords.Add(new Keyword("BASALT") {Parent = name});
      var basic = context.Keywords.Add(new Keyword("BASIC") {Parent = name});
      var belgium = context.Keywords.Add(new Keyword("BELGIUM") {Parent = name});
      var block = context.Keywords.Add(new Keyword("BLOCK") {Parent = name});
      var brick = context.Keywords.Add(new Keyword("BRICK") {Parent = name});
      var brickface = context.Keywords.Add(new Keyword("BRICKFACE") {Parent = name});
      var bullet = context.Keywords.Add(new Keyword("BULLET") {Parent = name});
      var camden = context.Keywords.Add(new Keyword("CAMDEN") {Parent = name});
      var campton = context.Keywords.Add(new Keyword("CAMPTON") {Parent = name});
      var canyon = context.Keywords.Add(new Keyword("CANYON") {Parent = name});
      var cap = context.Keywords.Add(new Keyword("CAP") {Parent = name});
      var chilton = context.Keywords.Add(new Keyword("CHILTON") {Parent = name});
      var chiseled = context.Keywords.Add(new Keyword("CHISELED") {Parent = name});
      var chiselwall = context.Keywords.Add(new Keyword("CHISELWALL") {Parent = name});
      var cobble = context.Keywords.Add(new Keyword("COBBLE") {Parent = name});
      var cobblestone = context.Keywords.Add(new Keyword("COBBLESTONE") {Parent = name});
      var concord = context.Keywords.Add(new Keyword("CONCORD") {Parent = name});
      var corner = context.Keywords.Add(new Keyword("CORNER") {Parent = name});
      var country = context.Keywords.Add(new Keyword("COUNTRY") {Parent = name});
      var cumberland = context.Keywords.Add(new Keyword("CUMBERLAND") {Parent = name});
      var custom = context.Keywords.Add(new Keyword("CUSTOM") {Parent = name});
      var doublesplit = context.Keywords.Add(new Keyword("DOUBLESPLIT") {Parent = name});
      var durango = context.Keywords.Add(new Keyword("DURANGO") {Parent = name});
      var dutch = context.Keywords.Add(new Keyword("DUTCH") {Parent = name});
      var edger = context.Keywords.Add(new Keyword("EDGER") {Parent = name});
      var edinburgh = context.Keywords.Add(new Keyword("EDINBURGH") {Parent = name});
      var everest = context.Keywords.Add(new Keyword("EVEREST") {Parent = name});
      var flagstone = context.Keywords.Add(new Keyword("FLAGSTONE") {Parent = name});
      var flash = context.Keywords.Add(new Keyword("FLASH") {Parent = name});
      var footnotes = context.Keywords.Add(new Keyword("FOOTNOTES") {Parent = name});
      var fresco = context.Keywords.Add(new Keyword("FRESCO") {Parent = name});
      var galena = context.Keywords.Add(new Keyword("GALENA") {Parent = name});
      var garden = context.Keywords.Add(new Keyword("GARDEN") {Parent = name});
      var geometric = context.Keywords.Add(new Keyword("GEOMETRIC") {Parent = name});
      var german = context.Keywords.Add(new Keyword("GERMAN") {Parent = name});
      var grand = context.Keywords.Add(new Keyword("GRAND") {Parent = name});
      var grandstone = context.Keywords.Add(new Keyword("GRANDSTONE") {Parent = name});
      var hampton = context.Keywords.Add(new Keyword("HAMPTON") {Parent = name});
      var holland = context.Keywords.Add(new Keyword("HOLLAND") {Parent = name});
      var homestead = context.Keywords.Add(new Keyword("HOMESTEAD") {Parent = name});
      var hudson = context.Keywords.Add(new Keyword("HUDSON") {Parent = name});
      var insignia = context.Keywords.Add(new Keyword("INSIGNIA") {Parent = name});
      var joint = context.Keywords.Add(new Keyword("JOINT") {Parent = name});
      var jumbo = context.Keywords.Add(new Keyword("JUMBO") {Parent = name});
      var lakestone = context.Keywords.Add(new Keyword("LAKESTONE") {Parent = name});
      var laredo = context.Keywords.Add(new Keyword("LAREDO") {Parent = name});
      var ledge = context.Keywords.Add(new Keyword("LEDGE") {Parent = name});
      var ledgewall = context.Keywords.Add(new Keyword("LEDGEWALL") {Parent = name});
      var lexington = context.Keywords.Add(new Keyword("LEXINGTON") {Parent = name});
      var log = context.Keywords.Add(new Keyword("LOG") {Parent = name});
      var manor = context.Keywords.Add(new Keyword("MANOR") {Parent = name});
      var mini = context.Keywords.Add(new Keyword("MINI") {Parent = name});
      var mission = context.Keywords.Add(new Keyword("MISSION") {Parent = name});
      var mm = context.Keywords.Add(new Keyword("MM") {Parent = name});
      var old = context.Keywords.Add(new Keyword("OLD") {Parent = name});
      var patio = context.Keywords.Add(new Keyword("PATIO") {Parent = name});
      var paver = context.Keywords.Add(new Keyword("PAVER") {Parent = name});
      var pinnacle = context.Keywords.Add(new Keyword("PINNACLE") {Parent = name});
      var plank = context.Keywords.Add(new Keyword("PLANK") {Parent = name});
      var planter = context.Keywords.Add(new Keyword("PLANTER") {Parent = name});
      var portage = context.Keywords.Add(new Keyword("PORTAGE") {Parent = name});
      var prism = context.Keywords.Add(new Keyword("PRISM") {Parent = name});
      var random = context.Keywords.Add(new Keyword("RANDOM") {Parent = name});
      var rectangular = context.Keywords.Add(new Keyword("RECTANGULAR") {Parent = name});
      var renaissance = context.Keywords.Add(new Keyword("RENAISSANCE") {Parent = name});
      var ring = context.Keywords.Add(new Keyword("RING") {Parent = name});
      var rivers = context.Keywords.Add(new Keyword("RIVERS") {Parent = name});
      var riverwalk = context.Keywords.Add(new Keyword("RIVERWALK") {Parent = name});
      var sandia = context.Keywords.Add(new Keyword("SANDIA") {Parent = name});
      var sandstone = context.Keywords.Add(new Keyword("SANDSTONE") {Parent = name});
      var scallop = context.Keywords.Add(new Keyword("SCALLOP") {Parent = name});
      var select = context.Keywords.Add(new Keyword("SELECT") {Parent = name});
      var sereno = context.Keywords.Add(new Keyword("SERENO") {Parent = name});
      var singles = context.Keywords.Add(new Keyword("SINGLES") {Parent = name});
      var slate = context.Keywords.Add(new Keyword("SLATE") {Parent = name});
      var soldier = context.Keywords.Add(new Keyword("SOLDIER") {Parent = name});
      var southwest = context.Keywords.Add(new Keyword("SOUTHWEST") {Parent = name});
      var splashblock = context.Keywords.Add(new Keyword("SPLASHBLOCK") {Parent = name});
      var split = context.Keywords.Add(new Keyword("SPLIT") {Parent = name});
      var square = context.Keywords.Add(new Keyword("SQUARE") {Parent = size});
      var stacked = context.Keywords.Add(new Keyword("STACKED") {Parent = name});
      var stepper = context.Keywords.Add(new Keyword("STEPPER") {Parent = name});
      var stone = context.Keywords.Add(new Keyword("STONE") {Parent = name});
      var straight = context.Keywords.Add(new Keyword("STRAIGHT") {Parent = name});
      var tahoe = context.Keywords.Add(new Keyword("TAHOE") {Parent = name});
      var tof = context.Keywords.Add(new Keyword("TOF") {Parent = name});
      var tree = context.Keywords.Add(new Keyword("TREE") {Parent = name});
      var tumbled = context.Keywords.Add(new Keyword("TUMBLED") {Parent = name});
      var vrnda = context.Keywords.Add(new Keyword("VRNDA") {Parent = name});
      var wall = context.Keywords.Add(new Keyword("WALL") {Parent = name});
      var wallstone = context.Keywords.Add(new Keyword("WALLSTONE") {Parent = name});
      var wetcast = context.Keywords.Add(new Keyword("WETCAST") {Parent = name});
      var yorkstone = context.Keywords.Add(new Keyword("YORKSTONE") {Parent = name});
      context.SaveChanges();

      var allegheny = context.Keywords.Add(new Keyword("ALLEGHENY") {Parent = color});
      var cappuccino = context.Keywords.Add(new Keyword("CAPPUCCINO") {Parent = color});
      var surrey = context.Keywords.Add(new Keyword("SURREY") {Parent = color});
      var toffee = context.Keywords.Add(new Keyword("TOFFEE") {Parent = color});
      var chaparral = context.Keywords.Add(new Keyword("CHAPARRAL") {Parent = color});
      var terracotta = context.Keywords.Add(new Keyword("TERRACOTTA") {Parent = color});
      var adobe = context.Keywords.Add(new Keyword("ADOBE") {Parent = color});
      var arcadian = context.Keywords.Add(new Keyword("ARCADIAN") {Parent = color});
      var ash = context.Keywords.Add(new Keyword("ASH") {Parent = color});
      var ashberry = context.Keywords.Add(new Keyword("ASHBERRY") {Parent = color});
      var ashland = context.Keywords.Add(new Keyword("ASHLAND") {Parent = color});
      var autumn = context.Keywords.Add(new Keyword("AUTUMN") {Parent = color});
      var black = context.Keywords.Add(new Keyword("BLACK") {Parent = color});
      var blend = context.Keywords.Add(new Keyword("BLEND") {Parent = color});
      var britt = context.Keywords.Add(new Keyword("BRITT") {Parent = color});
      var brown = context.Keywords.Add(new Keyword("BROWN") {Parent = color});
      var buff = context.Keywords.Add(new Keyword("BUFF") {Parent = color});
      var california = context.Keywords.Add(new Keyword("CALIFORNIA") {Parent = color});
      var chandler = context.Keywords.Add(new Keyword("CHANDLER") {Parent = color});
      var charcoal = context.Keywords.Add(new Keyword("CHARCOAL") {Parent = color});
      var coffee = context.Keywords.Add(new Keyword("COFFEE") {Parent = color});
      var copper = context.Keywords.Add(new Keyword("COPPER") {Parent = color});
      var creek = context.Keywords.Add(new Keyword("CREEK") {Parent = color});
      var dark = context.Keywords.Add(new Keyword("DARK") {Parent = color});
      var desert = context.Keywords.Add(new Keyword("DESERT") {Parent = color});
      var duncan = context.Keywords.Add(new Keyword("DUNCAN") {Parent = color});
      var everglade = context.Keywords.Add(new Keyword("EVERGLADE") {Parent = color});
      var gold = context.Keywords.Add(new Keyword("GOLD") {Parent = color});
      var goldrush = context.Keywords.Add(new Keyword("GOLDRUSH") {Parent = color});
      var gray = context.Keywords.Add(new Keyword("GRAY") {Parent = color});
      var grey = context.Keywords.Add(new Keyword("GREY") {Parent = color});
      var harvest = context.Keywords.Add(new Keyword("HARVEST") {Parent = color});
      var hill = context.Keywords.Add(new Keyword("HILL") {Parent = color});
      var jaxon = context.Keywords.Add(new Keyword("JAXON") {Parent = color});
      var limestone = context.Keywords.Add(new Keyword("LIMESTONE") {Parent = color});
      var natural = context.Keywords.Add(new Keyword("NATURAL") {Parent = color});
      var oakrun = context.Keywords.Add(new Keyword("OAKRUN") {Parent = color});
      var pastello = context.Keywords.Add(new Keyword("PASTELLO") {Parent = color});
      var peach = context.Keywords.Add(new Keyword("PEACH") {Parent = color});
      var peyton = context.Keywords.Add(new Keyword("PEYTON") {Parent = color});
      var postiano = context.Keywords.Add(new Keyword("POSTIANO") {Parent = color});
      var red = context.Keywords.Add(new Keyword("RED") {Parent = color});
      var river = context.Keywords.Add(new Keyword("RIVER") {Parent = color});
      var rose = context.Keywords.Add(new Keyword("ROSE") {Parent = color});
      var rush = context.Keywords.Add(new Keyword("RUSH") {Parent = color});
      var sand = context.Keywords.Add(new Keyword("SAND") {Parent = color});
      var sandy = context.Keywords.Add(new Keyword("SANDY") {Parent = color});
      var sierra = context.Keywords.Add(new Keyword("SIERRA") {Parent = color});
      var sierrgray = context.Keywords.Add(new Keyword("SIERRGRAY") {Parent = color});
      var smoke = context.Keywords.Add(new Keyword("SMOKE") {Parent = color});
      var sonoma = context.Keywords.Add(new Keyword("SONOMA") {Parent = color});
      var sunset = context.Keywords.Add(new Keyword("SUNSET") {Parent = color});
      var tan = context.Keywords.Add(new Keyword("TAN") {Parent = color});
      var tranquil = context.Keywords.Add(new Keyword("TRANQUIL") {Parent = color});
      var veranda = context.Keywords.Add(new Keyword("VERANDA") {Parent = color});
      var walnut = context.Keywords.Add(new Keyword("WALNUT") {Parent = color});
      var white = context.Keywords.Add(new Keyword("WHITE") {Parent = color});
      context.SaveChanges();

      var pacificclay = context.Keywords.Add(new Keyword("PACIFICCLAY") {Parent = vendor});
      var riccobene = context.Keywords.Add(new Keyword("RICCOBENE") {Parent = vendor});
      var countrystone = context.Keywords.Add(new Keyword("COUNTRYSTONE") {Parent = vendor});
      var cassay = context.Keywords.Add(new Keyword("CASSAY") {Parent = vendor});
      var aR = context.Keywords.Add(new Keyword("A+R") {Parent = vendor});
      var anchor = context.Keywords.Add(new Keyword("ANCHOR") {Parent = vendor});
      context.SaveChanges();

      var alghn = context.Keywords.Add(new Keyword("ALGHN") {Parent = allegheny});
      var alghny = context.Keywords.Add(new Keyword("ALGHNY") {Parent = allegheny});
      var algny = context.Keywords.Add(new Keyword("ALGNY") {Parent = allegheny});
      var allghny = context.Keywords.Add(new Keyword("ALLGHNY") {Parent = allegheny});
      var bvld = context.Keywords.Add(new Keyword("BVLD") {Parent = beveled});
      var paccly = context.Keywords.Add(new Keyword("PACCLY") {Parent = pacificclay});
      var pcly = context.Keywords.Add(new Keyword("PCLY") {Parent = pacificclay});
      var rcbne = context.Keywords.Add(new Keyword("RCBNE") {Parent = riccobene});
      var capachno = context.Keywords.Add(new Keyword("CAPACHNO") {Parent = cappuccino});
      var capcino = context.Keywords.Add(new Keyword("CAPCINO") {Parent = cappuccino});
      var capcno = context.Keywords.Add(new Keyword("CAPCNO") {Parent = cappuccino});
      var cappcno = context.Keywords.Add(new Keyword("CAPPCNO") {Parent = cappuccino});
      var cpchn = context.Keywords.Add(new Keyword("CPCHN") {Parent = cappuccino});
      var marqutte = context.Keywords.Add(new Keyword("MARQUTTE") {Parent = marquette});
      var sur = context.Keywords.Add(new Keyword("SUR") {Parent = surrey});
      var chprl = context.Keywords.Add(new Keyword("CHPRL") {Parent = chaparral});
      var cnst = context.Keywords.Add(new Keyword("CNST") {Parent = countrystone});
      var cntst = context.Keywords.Add(new Keyword("CNTST") {Parent = countrystone});
      var cntrysd = context.Keywords.Add(new Keyword("CNTRYSD") {Parent = countryside});
      var countrysid = context.Keywords.Add(new Keyword("COUNTRYSID") {Parent = countryside});
      var cssay = context.Keywords.Add(new Keyword("CSSAY") {Parent = cassay});
      var fourcbble = context.Keywords.Add(new Keyword("FOURCBBLE") {Parent = fourcobble});
      var fredrck = context.Keywords.Add(new Keyword("FREDRCK") {Parent = frederick});
      var trracta = context.Keywords.Add(new Keyword("TRRACTA") {Parent = terracotta});
      var wthrd = context.Keywords.Add(new Keyword("WTHRD") {Parent = weathered});
      var almeda = context.Keywords.Add(new Keyword("ALMEDA") {Parent = alameda});
      var anch = context.Keywords.Add(new Keyword("ANCH") {Parent = anchor});
      var anchr = context.Keywords.Add(new Keyword("ANCHR") {Parent = anchor});
      var ancr = context.Keywords.Add(new Keyword("ANCR") {Parent = anchor});
      var arcdn = context.Keywords.Add(new Keyword("ARCDN") {Parent = arcadian});
      var ashbry = context.Keywords.Add(new Keyword("ASHBRY") {Parent = ashberry});
      var ashld = context.Keywords.Add(new Keyword("ASHLD") {Parent = ashland});
      var ashlnd = context.Keywords.Add(new Keyword("ASHLND") {Parent = ashland});
      var aspn = context.Keywords.Add(new Keyword("ASPN") {Parent = aspen});
      var atm = context.Keywords.Add(new Keyword("ATM") {Parent = autumn});
      var atmn = context.Keywords.Add(new Keyword("ATMN") {Parent = autumn});
      var blk = context.Keywords.Add(new Keyword("BLK") {Parent = black});
      var bld = context.Keywords.Add(new Keyword("BLD") {Parent = blend});
      var blnd = context.Keywords.Add(new Keyword("BLND") {Parent = blend});
      var bock = context.Keywords.Add(new Keyword("BOCK") {Parent = block});
      var brckfc = context.Keywords.Add(new Keyword("BRCKFC") {Parent = brickface});
      var brckfce = context.Keywords.Add(new Keyword("BRCKFCE") {Parent = brickface});
      var br = context.Keywords.Add(new Keyword("BR") {Parent = brown});
      var brn = context.Keywords.Add(new Keyword("BRN") {Parent = brown});
      var brw = context.Keywords.Add(new Keyword("BRW") {Parent = brown});
      var brwn = context.Keywords.Add(new Keyword("BRWN") {Parent = brown});
      var bf = context.Keywords.Add(new Keyword("BF") {Parent = buff});
      var bff = context.Keywords.Add(new Keyword("BFF") {Parent = buff});
      var buf = context.Keywords.Add(new Keyword("BUF") {Parent = buff});
      var camdn = context.Keywords.Add(new Keyword("CAMDN") {Parent = camden});
      var chandl = context.Keywords.Add(new Keyword("CHANDL") {Parent = chandler});
      var chndlr = context.Keywords.Add(new Keyword("CHNDLR") {Parent = chandler});
      var chnlr = context.Keywords.Add(new Keyword("CHNLR") {Parent = chandler});
      var ch = context.Keywords.Add(new Keyword("CH") {Parent = charcoal});
      var charq = context.Keywords.Add(new Keyword("CHAR") {Parent = charcoal});
      var charcaol = context.Keywords.Add(new Keyword("CHARCAOL") {Parent = charcoal});
      var chr = context.Keywords.Add(new Keyword("CHR") {Parent = charcoal});
      var chisled = context.Keywords.Add(new Keyword("CHISLED") {Parent = chiseled});
      var chisleled = context.Keywords.Add(new Keyword("CHISLELED") {Parent = chiseled});
      var cbbl = context.Keywords.Add(new Keyword("CBBL") {Parent = cobble});
      var cbl = context.Keywords.Add(new Keyword("CBL") {Parent = cobble});
      var cob = context.Keywords.Add(new Keyword("COB") {Parent = cobble});
      var cobbl = context.Keywords.Add(new Keyword("COBBL") {Parent = cobble});
      var cobl = context.Keywords.Add(new Keyword("COBL") {Parent = cobble});
      var cbblstn = context.Keywords.Add(new Keyword("CBBLSTN") {Parent = cobblestone});
      var cncd = context.Keywords.Add(new Keyword("CNCD") {Parent = concord});
      var cnr = context.Keywords.Add(new Keyword("CNR") {Parent = corner});
      var cor = context.Keywords.Add(new Keyword("COR") {Parent = corner});
      var cntry = context.Keywords.Add(new Keyword("CNTRY") {Parent = country});
      var cnty = context.Keywords.Add(new Keyword("CNTY") {Parent = country});
      var cny = context.Keywords.Add(new Keyword("CNY") {Parent = country});
      var ct = context.Keywords.Add(new Keyword("CT") {Parent = country});
      var cmbrlnd = context.Keywords.Add(new Keyword("CMBRLND") {Parent = cumberland});
      var dcn = context.Keywords.Add(new Keyword("DCN") {Parent = duncan});
      var dncn = context.Keywords.Add(new Keyword("DNCN") {Parent = duncan});
      var edg = context.Keywords.Add(new Keyword("EDG") {Parent = edger});
      var edgerer = context.Keywords.Add(new Keyword("EDGERER") {Parent = edger});
      var edgr = context.Keywords.Add(new Keyword("EDGR") {Parent = edger});
      var evrst = context.Keywords.Add(new Keyword("EVRST") {Parent = everest});
      var flagstn = context.Keywords.Add(new Keyword("FLAGSTN") {Parent = flagstone});
      var flgstn = context.Keywords.Add(new Keyword("FLGSTN") {Parent = flagstone});
      var geometrc = context.Keywords.Add(new Keyword("GEOMETRC") {Parent = geometric});
      var grnd = context.Keywords.Add(new Keyword("GRND") {Parent = grand});
      var gr = context.Keywords.Add(new Keyword("GR") {Parent = gray});
      var gry = context.Keywords.Add(new Keyword("GRY") {Parent = gray});
      var harvst = context.Keywords.Add(new Keyword("HARVST") {Parent = harvest});
      var hrvst = context.Keywords.Add(new Keyword("HRVST") {Parent = harvest});
      var hl = context.Keywords.Add(new Keyword("HL") {Parent = hill});
      var hlland = context.Keywords.Add(new Keyword("HLLAND") {Parent = holland});
      var hlld = context.Keywords.Add(new Keyword("HLLD") {Parent = holland});
      var hllnd = context.Keywords.Add(new Keyword("HLLND") {Parent = holland});
      var hmstd = context.Keywords.Add(new Keyword("HMSTD") {Parent = homestead});
      var homstd = context.Keywords.Add(new Keyword("HOMSTD") {Parent = homestead});
      var jaxn = context.Keywords.Add(new Keyword("JAXN") {Parent = jaxon});
      var jxn = context.Keywords.Add(new Keyword("JXN") {Parent = jaxon});
      var lexngtn = context.Keywords.Add(new Keyword("LEXNGTN") {Parent = lexington});
      var lxingtn = context.Keywords.Add(new Keyword("LXINGTN") {Parent = lexington});
      var lxngtn = context.Keywords.Add(new Keyword("LXNGTN") {Parent = lexington});
      var limstn = context.Keywords.Add(new Keyword("LIMSTN") {Parent = limestone});
      var lm = context.Keywords.Add(new Keyword("LM") {Parent = limestone});
      var lmestn = context.Keywords.Add(new Keyword("LMESTN") {Parent = limestone});
      var lmst = context.Keywords.Add(new Keyword("LMST") {Parent = limestone});
      var lmstn = context.Keywords.Add(new Keyword("LMSTN") {Parent = limestone});
      var mnr = context.Keywords.Add(new Keyword("MNR") {Parent = manor});
      var min = context.Keywords.Add(new Keyword("MIN") {Parent = mini});
      var pvr = context.Keywords.Add(new Keyword("PVR") {Parent = paver});
      var peytn = context.Keywords.Add(new Keyword("PEYTN") {Parent = peyton});
      var pytn = context.Keywords.Add(new Keyword("PYTN") {Parent = peyton});
      var plntr = context.Keywords.Add(new Keyword("PLNTR") {Parent = planter});
      var rd = context.Keywords.Add(new Keyword("RD") {Parent = red});
      var sd = context.Keywords.Add(new Keyword("SD") {Parent = sand});
      var snd = context.Keywords.Add(new Keyword("SND") {Parent = sand});
      var srra = context.Keywords.Add(new Keyword("SRRA") {Parent = sierra});
      var slt = context.Keywords.Add(new Keyword("SLT") {Parent = slate});
      var sq = context.Keywords.Add(new Keyword("SQ") {Parent = square});
      var sqre = context.Keywords.Add(new Keyword("SQRE") {Parent = square});
      var st = context.Keywords.Add(new Keyword("ST") {Parent = stone});
      var stn = context.Keywords.Add(new Keyword("STN") {Parent = stone});
      var stne = context.Keywords.Add(new Keyword("STNE") {Parent = stone});
      var stnev = context.Keywords.Add(new Keyword("STNEV") {Parent = stone});
      var tn = context.Keywords.Add(new Keyword("TN") {Parent = tan});
      var tranql = context.Keywords.Add(new Keyword("TRANQL") {Parent = tranquil});
      var trnql = context.Keywords.Add(new Keyword("TRNQL") {Parent = tranquil});
      var trnqul = context.Keywords.Add(new Keyword("TRNQUL") {Parent = tranquil});
      var wl = context.Keywords.Add(new Keyword("WL") {Parent = wall});
      var wll = context.Keywords.Add(new Keyword("WLL") {Parent = wall});
      context.SaveChanges();
    }
  }
}