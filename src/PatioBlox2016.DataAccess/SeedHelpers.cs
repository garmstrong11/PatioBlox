namespace PatioBlox2016.DataAccess
{
  using System;
  using System.Collections.Generic;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Diagnostics.CodeAnalysis;
  using System.IO.Abstractions;
  using System.Linq;
  using Concrete;
  using Extractor;

  public static class SeedHelpers
  {
    public static IEnumerable<Description> GetDescriptionSeeds()
    {
      var dapter = new FlexCelDataSourceAdapter();
      var fileSystem = new FileSystem();

      var extractor = new DescriptionExtractor(dapter, fileSystem);
      var seedPath = fileSystem.Path.Combine(GetSeedPath(), "Descriptions_All.xlsx");
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

    private static void SeedUpcReplacements(PatioBloxContext context)
    {
      var ae34 = new UpcReplacement() {InvalidUpc = "712626103100", Replacement = "712626103101"};
      context.UpcReplacements.AddOrUpdate(v => v.InvalidUpc, ae34);
      var vb26 = new UpcReplacement() {InvalidUpc = "42911120453", Replacement = "042911120453"};
      context.UpcReplacements.AddOrUpdate(v => v.InvalidUpc, vb26);

      context.SaveChanges();
    }

    private static void AddKeywordToContextIfNotExists(
      Keyword keyword, PatioBloxContext context, bool saveNow = false)
    {
      var existingKeyword = context.Keywords.Local.FirstOrDefault(k => k.Word == keyword.Word);
      if (existingKeyword != null) return;

      context.Keywords.Add(keyword);
      if (saveNow) {
        context.SaveChanges();
      }
    }

    public static void FullSeed(PatioBloxContext context)
    {
      SeedKeywords(context, true);

      var descriptions = GetDescriptionSeeds();

      foreach (var description in descriptions)
      {
        context.Descriptions.AddOrUpdate(d => d.Text, description);
      }

      SeedUpcReplacements(context);
      context.SaveChanges();
    }

    public static void TestSeed(PatioBloxContext context)
    {
      SeedKeywords(context);
    }

    [SuppressMessage("ReSharper", "UnusedVariable")]
    private static void SeedKeywords(PatioBloxContext context, bool isFullSeed = false)
    {
      context.Keywords.Load();
      var unknown = new Keyword(Keyword.NewKey) {Parent = null};
      var color = new Keyword(Keyword.ColorKey) {Parent = null};
      var vendor = new Keyword(Keyword.VendorKey) { Parent = null };
      var name = new Keyword(Keyword.NameKey) { Parent = null };
      var size = new Keyword(Keyword.SizeKey) { Parent = null };

      // Calling save changes after each so they will appear 
      // in the db in a specific order.
      AddKeywordToContextIfNotExists(unknown, context, true);
      AddKeywordToContextIfNotExists(name, context, true);
      AddKeywordToContextIfNotExists(color, context, true);
      AddKeywordToContextIfNotExists(vendor, context, true);
      AddKeywordToContextIfNotExists(size, context, true);

      if (!isFullSeed) return;

      var beveled = new Keyword("BEVELED") {Parent = name};
      AddKeywordToContextIfNotExists(beveled, context);

      var marquette = new Keyword("MARQUETTE") { Parent = name };
      AddKeywordToContextIfNotExists(marquette, context);

      var countryside = new Keyword("COUNTRYSIDE") { Parent = name };
      AddKeywordToContextIfNotExists(countryside, context);

      var fourcobble = new Keyword("FOURCOBBLE") { Parent = name };
      AddKeywordToContextIfNotExists(fourcobble, context);

      var frederick = new Keyword("FREDERICK") { Parent = name };
      AddKeywordToContextIfNotExists(frederick, context);

      var weathered = new Keyword("WEATHERED") { Parent = name };
      AddKeywordToContextIfNotExists(weathered, context);

      var side = new Keyword("SIDE") { Parent = name };
      AddKeywordToContextIfNotExists(side, context);

      var alameda = new Keyword("ALAMEDA") { Parent = name };
      AddKeywordToContextIfNotExists(alameda, context);

      var antique = new Keyword("ANTIQUE") { Parent = name };
      AddKeywordToContextIfNotExists(antique, context);

      var aspen = new Keyword("ASPEN") { Parent = name };
      AddKeywordToContextIfNotExists(aspen, context);

      var austin = new Keyword("AUSTIN") { Parent = name };
      AddKeywordToContextIfNotExists(austin, context);

      var basalt = new Keyword("BASALT") { Parent = name };
      AddKeywordToContextIfNotExists(basalt, context);

      var basic = new Keyword("BASIC") { Parent = name };
      AddKeywordToContextIfNotExists(basic, context);

      var belgium = new Keyword("BELGIUM") { Parent = name };
      AddKeywordToContextIfNotExists(belgium, context);

      var block = new Keyword("BLOCK") { Parent = name };
      AddKeywordToContextIfNotExists(block, context);

      var brick = new Keyword("BRICK") { Parent = name };
      AddKeywordToContextIfNotExists(brick, context);

      var brickface = new Keyword("BRICKFACE") { Parent = name };
      AddKeywordToContextIfNotExists(brickface, context);

      var bullet = new Keyword("BULLET") { Parent = name };
      AddKeywordToContextIfNotExists(bullet, context);

      var camden = new Keyword("CAMDEN") { Parent = name };
      AddKeywordToContextIfNotExists(camden, context);

      var campton = new Keyword("CAMPTON") { Parent = name };
      AddKeywordToContextIfNotExists(campton, context);

      var canyon = new Keyword("CANYON") { Parent = name };
      AddKeywordToContextIfNotExists(canyon, context);

      var cap = new Keyword("CAP") { Parent = name };
      AddKeywordToContextIfNotExists(cap, context);

      var chilton = new Keyword("CHILTON") { Parent = name };
      AddKeywordToContextIfNotExists(chilton, context);

      var chiseled = new Keyword("CHISELED") { Parent = name };
      AddKeywordToContextIfNotExists(chiseled, context);

      var chiselwall = new Keyword("CHISELWALL") { Parent = name };
      AddKeywordToContextIfNotExists(chiselwall, context);

      var cobble = new Keyword("COBBLE") { Parent = name };
      AddKeywordToContextIfNotExists(cobble, context);

      var cobblestone = new Keyword("COBBLESTONE") { Parent = name };
      AddKeywordToContextIfNotExists(cobblestone, context);

      var concord = new Keyword("CONCORD") { Parent = name };
      AddKeywordToContextIfNotExists(concord, context);

      var corner = new Keyword("CORNER") { Parent = name };
      AddKeywordToContextIfNotExists(corner, context);

      var country = new Keyword("COUNTRY") { Parent = name };
      AddKeywordToContextIfNotExists(country, context);

      var countrymanor = new Keyword("COUNTRYMANOR") { Parent = name };
      AddKeywordToContextIfNotExists(countrymanor, context);

      var cumberland = new Keyword("CUMBERLAND") { Parent = name };
      AddKeywordToContextIfNotExists(cumberland, context);

      var custom = new Keyword("CUSTOM") { Parent = name };
      AddKeywordToContextIfNotExists(custom, context);

      var doublesplit = new Keyword("DOUBLESPLIT") { Parent = name };
      AddKeywordToContextIfNotExists(doublesplit, context);

      var durango = new Keyword("DURANGO") { Parent = name };
      AddKeywordToContextIfNotExists(durango, context);

      var dutch = new Keyword("DUTCH") { Parent = name };
      AddKeywordToContextIfNotExists(dutch, context);

      var edger = new Keyword("EDGER") { Parent = name };
      AddKeywordToContextIfNotExists(edger, context);

      var edinburgh = new Keyword("EDINBURGH") { Parent = name };
      AddKeywordToContextIfNotExists(edinburgh, context);

      var everest = new Keyword("EVEREST") { Parent = name };
      AddKeywordToContextIfNotExists(everest, context);

      var flagstone = new Keyword("FLAGSTONE") { Parent = name };
      AddKeywordToContextIfNotExists(flagstone, context);

      var flash = new Keyword("FLASH") { Parent = name };
      AddKeywordToContextIfNotExists(flash, context);

      var footnotes = new Keyword("FOOTNOTES") { Parent = name };
      AddKeywordToContextIfNotExists(footnotes, context);

      var fresco = new Keyword("FRESCO") { Parent = name };
      AddKeywordToContextIfNotExists(fresco, context);

      var galena = new Keyword("GALENA") { Parent = name };
      AddKeywordToContextIfNotExists(galena, context);

      var garden = new Keyword("GARDEN") { Parent = name };
      AddKeywordToContextIfNotExists(garden, context);

      var geometric = new Keyword("GEOMETRIC") { Parent = name };
      AddKeywordToContextIfNotExists(geometric, context);

      var german = new Keyword("GERMAN") { Parent = name };
      AddKeywordToContextIfNotExists(german, context);

      var grand = new Keyword("GRAND") { Parent = name };
      AddKeywordToContextIfNotExists(grand, context);

      var grandstone = new Keyword("GRANDSTONE") { Parent = name };
      AddKeywordToContextIfNotExists(grandstone, context);

      var hampton = new Keyword("HAMPTON") { Parent = name };
      AddKeywordToContextIfNotExists(hampton, context);

      var holland = new Keyword("HOLLAND") { Parent = name };
      AddKeywordToContextIfNotExists(holland, context);

      var homestead = new Keyword("HOMESTEAD") { Parent = name };
      AddKeywordToContextIfNotExists(homestead, context);

      var hudson = new Keyword("HUDSON") { Parent = name };
      AddKeywordToContextIfNotExists(hudson, context);

      var insignia = new Keyword("INSIGNIA") { Parent = name };
      AddKeywordToContextIfNotExists(insignia, context);

      var joint = new Keyword("JOINT") { Parent = name };
      AddKeywordToContextIfNotExists(joint, context);

      var lakestone = new Keyword("LAKESTONE") { Parent = name };
      AddKeywordToContextIfNotExists(lakestone, context);

      var laredo = new Keyword("LAREDO") { Parent = name };
      AddKeywordToContextIfNotExists(laredo, context);

      var ledge = new Keyword("LEDGE") { Parent = name };
      AddKeywordToContextIfNotExists(ledge, context);

      var ledgewall = new Keyword("LEDGEWALL") { Parent = name };
      AddKeywordToContextIfNotExists(ledgewall, context);

      var lexington = new Keyword("LEXINGTON") { Parent = name };
      AddKeywordToContextIfNotExists(lexington, context);

      var log = new Keyword("LOG") { Parent = name };
      AddKeywordToContextIfNotExists(log, context);

      var manor = new Keyword("MANOR") { Parent = name };
      AddKeywordToContextIfNotExists(manor, context);

      var mini = new Keyword("MINI") { Parent = name };
      AddKeywordToContextIfNotExists(mini, context);

      var mission = new Keyword("MISSION") { Parent = name };
      AddKeywordToContextIfNotExists(mission, context);

      var mm = new Keyword("MM") { Parent = name };
      AddKeywordToContextIfNotExists(mm, context);

      var old = new Keyword("OLD") { Parent = name };
      AddKeywordToContextIfNotExists(old, context);

      var patio = new Keyword("PATIO") { Parent = name };
      AddKeywordToContextIfNotExists(patio, context);

      var paver = new Keyword("PAVER") { Parent = name };
      AddKeywordToContextIfNotExists(paver, context);

      var pinnacle = new Keyword("PINNACLE") { Parent = name };
      AddKeywordToContextIfNotExists(pinnacle, context);

      var plank = new Keyword("PLANK") { Parent = name };
      AddKeywordToContextIfNotExists(plank, context);

      var planter = new Keyword("PLANTER") { Parent = name };
      AddKeywordToContextIfNotExists(planter, context);

      var portage = new Keyword("PORTAGE") { Parent = name };
      AddKeywordToContextIfNotExists(portage, context);

      var prism = new Keyword("PRISM") { Parent = name };
      AddKeywordToContextIfNotExists(prism, context);

      var random = new Keyword("RANDOM") { Parent = name };
      AddKeywordToContextIfNotExists(random, context);

      var rectangular = new Keyword("RECTANGULAR") { Parent = name };
      AddKeywordToContextIfNotExists(rectangular, context);

      var renaissance = new Keyword("RENAISSANCE") { Parent = name };
      AddKeywordToContextIfNotExists(renaissance, context);

      var ring = new Keyword("RING") { Parent = name };
      AddKeywordToContextIfNotExists(ring, context);

      var rivers = new Keyword("RIVERS") { Parent = name };
      AddKeywordToContextIfNotExists(rivers, context);

      var riverwalk = new Keyword("RIVERWALK") { Parent = name };
      AddKeywordToContextIfNotExists(riverwalk, context);

      var sandia = new Keyword("SANDIA") { Parent = name };
      AddKeywordToContextIfNotExists(sandia, context);

      var sandstone = new Keyword("SANDSTONE") { Parent = name };
      AddKeywordToContextIfNotExists(sandstone, context);

      var scallop = new Keyword("SCALLOP") { Parent = name };
      AddKeywordToContextIfNotExists(scallop, context);

      var select = new Keyword("SELECT") { Parent = name };
      AddKeywordToContextIfNotExists(@select, context);

      var sereno = new Keyword("SERENO") { Parent = name };
      AddKeywordToContextIfNotExists(sereno, context);

      var singles = new Keyword("SINGLES") { Parent = name };
      AddKeywordToContextIfNotExists(singles, context);

      var slate = new Keyword("SLATE") { Parent = name };
      AddKeywordToContextIfNotExists(slate, context);

      var soldier = new Keyword("SOLDIER") { Parent = name };
      AddKeywordToContextIfNotExists(soldier, context);

      var southwest = new Keyword("SOUTHWEST") { Parent = name };
      AddKeywordToContextIfNotExists(southwest, context);

      var splashblock = new Keyword("SPLASHBLOCK") { Parent = name };
      AddKeywordToContextIfNotExists(splashblock, context);

      var split = new Keyword("SPLIT") { Parent = name };
      AddKeywordToContextIfNotExists(split, context);

      var square = new Keyword("SQUARE") { Parent = size };
      AddKeywordToContextIfNotExists(square, context);

      var stacked = new Keyword("STACKED") { Parent = name };
      AddKeywordToContextIfNotExists(stacked, context);

      var stepper = new Keyword("STEPPER") { Parent = name };
      AddKeywordToContextIfNotExists(stepper, context);

      var stone = new Keyword("STONE") { Parent = name };
      AddKeywordToContextIfNotExists(stone, context);

      var straight = new Keyword("STRAIGHT") { Parent = name };
      AddKeywordToContextIfNotExists(straight, context);

      var tahoe = new Keyword("TAHOE") { Parent = name };
      AddKeywordToContextIfNotExists(tahoe, context);

      var tof = new Keyword("TOF") { Parent = name };
      AddKeywordToContextIfNotExists(tof, context);

      var tree = new Keyword("TREE") { Parent = name };
      AddKeywordToContextIfNotExists(tree, context);

      var tumbled = new Keyword("TUMBLED") { Parent = name };
      AddKeywordToContextIfNotExists(tumbled, context);

      var vrnda = new Keyword("VRNDA") { Parent = name };
      AddKeywordToContextIfNotExists(vrnda, context);

      var wall = new Keyword("WALL") { Parent = name };
      AddKeywordToContextIfNotExists(wall, context);

      var wallstone = new Keyword("WALLSTONE") { Parent = name };
      AddKeywordToContextIfNotExists(wallstone, context);

      var wetcast = new Keyword("WETCAST") { Parent = name };
      AddKeywordToContextIfNotExists(wetcast, context);

      var yorkstone = new Keyword("YORKSTONE") { Parent = name };
      AddKeywordToContextIfNotExists(yorkstone, context);

      var stepping = new Keyword("STEPPING") { Parent = name };
      AddKeywordToContextIfNotExists(stepping, context);

      context.SaveChanges();

      var large = new Keyword("LARGE") { Parent = size };
      AddKeywordToContextIfNotExists(large, context);

      var medium = new Keyword("MEDIUM") { Parent = size };
      AddKeywordToContextIfNotExists(medium, context);

      var small = new Keyword("SMALL") { Parent = size };
      AddKeywordToContextIfNotExists(small, context);

      var jumbo = new Keyword("JUMBO") { Parent = size };
      AddKeywordToContextIfNotExists(jumbo, context);

      context.SaveChanges();

      var allegheny = new Keyword("ALLEGHENY") { Parent = color };
      AddKeywordToContextIfNotExists(allegheny, context);

      var cappuccino = new Keyword("CAPPUCCINO") { Parent = color };
      AddKeywordToContextIfNotExists(cappuccino, context);

      var surrey = new Keyword("SURREY") { Parent = color };
      AddKeywordToContextIfNotExists(surrey, context);

      var toffee = new Keyword("TOFFEE") { Parent = color };
      AddKeywordToContextIfNotExists(toffee, context);

      var chaparral = new Keyword("CHAPARRAL") { Parent = color };
      AddKeywordToContextIfNotExists(chaparral, context);

      var terracotta = new Keyword("TERRACOTTA") { Parent = color };
      AddKeywordToContextIfNotExists(terracotta, context);

      var adobe = new Keyword("ADOBE") { Parent = color };
      AddKeywordToContextIfNotExists(adobe, context);

      var arcadian = new Keyword("ARCADIAN") { Parent = color };
      AddKeywordToContextIfNotExists(arcadian, context);

      var ash = new Keyword("ASH") { Parent = color };
      AddKeywordToContextIfNotExists(ash, context);

      var ashberry = new Keyword("ASHBERRY") { Parent = color };
      AddKeywordToContextIfNotExists(ashberry, context);

      var ashland = new Keyword("ASHLAND") { Parent = color };
      AddKeywordToContextIfNotExists(ashland, context);

      var autumn = new Keyword("AUTUMN") { Parent = color };
      AddKeywordToContextIfNotExists(autumn, context);

      var black = new Keyword("BLACK") { Parent = color };
      AddKeywordToContextIfNotExists(black, context);

      var blend = new Keyword("BLEND") { Parent = color };
      AddKeywordToContextIfNotExists(blend, context);

      var britt = new Keyword("BRITT") { Parent = color };
      AddKeywordToContextIfNotExists(britt, context);

      var brown = new Keyword("BROWN") { Parent = color };
      AddKeywordToContextIfNotExists(brown, context);

      var buff = new Keyword("BUFF") { Parent = color };
      AddKeywordToContextIfNotExists(buff, context);

      var california = new Keyword("CALIFORNIA") { Parent = color };
      AddKeywordToContextIfNotExists(california, context);

      var chandler = new Keyword("CHANDLER") { Parent = color };
      AddKeywordToContextIfNotExists(chandler, context);

      var charcoal = new Keyword("CHARCOAL") { Parent = color };
      AddKeywordToContextIfNotExists(charcoal, context);

      var coffee = new Keyword("COFFEE") { Parent = color };
      AddKeywordToContextIfNotExists(coffee, context);

      var copper = new Keyword("COPPER") { Parent = color };
      AddKeywordToContextIfNotExists(copper, context);

      var creek = new Keyword("CREEK") { Parent = color };
      AddKeywordToContextIfNotExists(creek, context);

      var dark = new Keyword("DARK") { Parent = color };
      AddKeywordToContextIfNotExists(dark, context);

      var darkbuff = new Keyword("DARKBUFF") { Parent = color };
      AddKeywordToContextIfNotExists(darkbuff, context);

      var desert = new Keyword("DESERT") { Parent = color };
      AddKeywordToContextIfNotExists(desert, context);

      var duncan = new Keyword("DUNCAN") { Parent = color };
      AddKeywordToContextIfNotExists(duncan, context);

      var fredrickson = new Keyword("FREDRICKSON") { Parent = color };
      AddKeywordToContextIfNotExists(fredrickson, context);

      var everglade = new Keyword("EVERGLADE") { Parent = color };
      AddKeywordToContextIfNotExists(everglade, context);

      var gold = new Keyword("GOLD") { Parent = color };
      AddKeywordToContextIfNotExists(gold, context);

      var goldrush = new Keyword("GOLDRUSH") { Parent = color };
      AddKeywordToContextIfNotExists(goldrush, context);

      var gray = new Keyword("GRAY") { Parent = color };
      AddKeywordToContextIfNotExists(gray, context);

      var grey = new Keyword("GREY") { Parent = color };
      AddKeywordToContextIfNotExists(grey, context);

      var grigio = new Keyword("GRIGIO") { Parent = color };
      AddKeywordToContextIfNotExists(grigio, context);

      var harvest = new Keyword("HARVEST") { Parent = color };
      AddKeywordToContextIfNotExists(harvest, context);

      var hill = new Keyword("HILL") { Parent = color };
      AddKeywordToContextIfNotExists(hill, context);

      var jaxon = new Keyword("JAXON") { Parent = color };
      AddKeywordToContextIfNotExists(jaxon, context);

      var limestone = new Keyword("LIMESTONE") { Parent = color };
      AddKeywordToContextIfNotExists(limestone, context);

      var natural = new Keyword("NATURAL") { Parent = color };
      AddKeywordToContextIfNotExists(natural, context);

      var oakrun = new Keyword("OAKRUN") { Parent = color };
      AddKeywordToContextIfNotExists(oakrun, context);

      var pastello = new Keyword("PASTELLO") { Parent = color };
      AddKeywordToContextIfNotExists(pastello, context);

      var peach = new Keyword("PEACH") { Parent = color };
      AddKeywordToContextIfNotExists(peach, context);

      var peyton = new Keyword("PEYTON") { Parent = color };
      AddKeywordToContextIfNotExists(peyton, context);

      var postiano = new Keyword("POSTIANO") { Parent = color };
      AddKeywordToContextIfNotExists(postiano, context);

      var red = new Keyword("RED") { Parent = color };
      AddKeywordToContextIfNotExists(red, context);

      var river = new Keyword("RIVER") { Parent = color };
      AddKeywordToContextIfNotExists(river, context);

      var rose = new Keyword("ROSE") { Parent = color };
      AddKeywordToContextIfNotExists(rose, context);

      var rush = new Keyword("RUSH") { Parent = color };
      AddKeywordToContextIfNotExists(rush, context);

      var sand = new Keyword("SAND") { Parent = color };
      AddKeywordToContextIfNotExists(sand, context);

      var sandy = new Keyword("SANDY") { Parent = color };
      AddKeywordToContextIfNotExists(sandy, context);

      var sierra = new Keyword("SIERRA") { Parent = color };
      AddKeywordToContextIfNotExists(sierra, context);

      var sierrgray = new Keyword("SIERRGRAY") { Parent = color };
      AddKeywordToContextIfNotExists(sierrgray, context);

      var smoke = new Keyword("SMOKE") { Parent = color };
      AddKeywordToContextIfNotExists(smoke, context);

      var sonoma = new Keyword("SONOMA") { Parent = color };
      AddKeywordToContextIfNotExists(sonoma, context);

      var sunset = new Keyword("SUNSET") { Parent = color };
      AddKeywordToContextIfNotExists(sunset, context);

      var tan = new Keyword("TAN") { Parent = color };
      AddKeywordToContextIfNotExists(tan, context);

      var tranquil = new Keyword("TRANQUIL") { Parent = color };
      AddKeywordToContextIfNotExists(tranquil, context);

      var veranda = new Keyword("VERANDA") { Parent = color };
      AddKeywordToContextIfNotExists(veranda, context);

      var walnut = new Keyword("WALNUT") { Parent = color };
      AddKeywordToContextIfNotExists(walnut, context);

      var white = new Keyword("WHITE") { Parent = color };
      AddKeywordToContextIfNotExists(white, context);

      context.SaveChanges();

      var pacificclay = new Keyword("PACIFICCLAY") { Parent = vendor };
      AddKeywordToContextIfNotExists(pacificclay, context);

      var riccobene = new Keyword("RICCOBENE") { Parent = vendor };
      AddKeywordToContextIfNotExists(riccobene, context);

      var countrystone = new Keyword("COUNTRYSTONE") { Parent = vendor };
      AddKeywordToContextIfNotExists(countrystone, context);

      var cassay = new Keyword("CASSAY") { Parent = vendor };
      AddKeywordToContextIfNotExists(cassay, context);

      var aR = new Keyword("A+R") { Parent = vendor };
      AddKeywordToContextIfNotExists(aR, context);

      var anchor = new Keyword("ANCHOR") { Parent = vendor };
      AddKeywordToContextIfNotExists(anchor, context);

      var davis = new Keyword("DAVIS") { Parent = vendor };
      AddKeywordToContextIfNotExists(davis, context);

      var fulton = new Keyword("FULTON") { Parent = vendor };
      AddKeywordToContextIfNotExists(fulton, context);

      var bertram = new Keyword("BERTRAM") { Parent = vendor };
      AddKeywordToContextIfNotExists(bertram, context);

      var luxora = new Keyword("LUXORA") { Parent = vendor };
      AddKeywordToContextIfNotExists(luxora, context);

      context.SaveChanges();

      var alghn = new Keyword("ALGHN") { Parent = allegheny };
      AddKeywordToContextIfNotExists(alghn, context);

      var alghny = new Keyword("ALGHNY") { Parent = allegheny };
      AddKeywordToContextIfNotExists(alghny, context);

      var algny = new Keyword("ALGNY") { Parent = allegheny };
      AddKeywordToContextIfNotExists(algny, context);

      var allghny = new Keyword("ALLGHNY") { Parent = allegheny };
      AddKeywordToContextIfNotExists(allghny, context);

      var bvld = new Keyword("BVLD") { Parent = beveled };
      AddKeywordToContextIfNotExists(bvld, context);

      var paccly = new Keyword("PACCLY") { Parent = pacificclay };
      AddKeywordToContextIfNotExists(paccly, context);

      var pcly = new Keyword("PCLY") { Parent = pacificclay };
      AddKeywordToContextIfNotExists(pcly, context);

      var rcbne = new Keyword("RCBNE") { Parent = riccobene };
      AddKeywordToContextIfNotExists(rcbne, context);

      var capachno = new Keyword("CAPACHNO") { Parent = cappuccino };
      AddKeywordToContextIfNotExists(capachno, context);

      var capcino = new Keyword("CAPCINO") { Parent = cappuccino };
      AddKeywordToContextIfNotExists(capcino, context);

      var capcno = new Keyword("CAPCNO") { Parent = cappuccino };
      AddKeywordToContextIfNotExists(capcno, context);

      var cappcno = new Keyword("CAPPCNO") { Parent = cappuccino };
      AddKeywordToContextIfNotExists(cappcno, context);

      var cm = new Keyword("CM") { Parent = countrymanor };
      AddKeywordToContextIfNotExists(cm, context);

      var cobbble = new Keyword("COBBBLE") { Parent = cobble };
      AddKeywordToContextIfNotExists(cobbble, context);

      var cobbel = new Keyword("COBBEL") { Parent = cobble };
      AddKeywordToContextIfNotExists(cobbel, context);

      var cpchn = new Keyword("CPCHN") { Parent = cappuccino };
      AddKeywordToContextIfNotExists(cpchn, context);

      var marqutte = new Keyword("MARQUTTE") { Parent = marquette };
      AddKeywordToContextIfNotExists(marqutte, context);

      var sur = new Keyword("SUR") { Parent = surrey };
      AddKeywordToContextIfNotExists(sur, context);

      var chprl = new Keyword("CHPRL") { Parent = chaparral };
      AddKeywordToContextIfNotExists(chprl, context);

      var cnst = new Keyword("CNST") { Parent = countrystone };
      AddKeywordToContextIfNotExists(cnst, context);

      var cntst = new Keyword("CNTST") { Parent = countrystone };
      AddKeywordToContextIfNotExists(cntst, context);

      var cntrysd = new Keyword("CNTRYSD") { Parent = countryside };
      AddKeywordToContextIfNotExists(cntrysd, context);

      var countrysid = new Keyword("COUNTRYSID") { Parent = countryside };
      AddKeywordToContextIfNotExists(countrysid, context);

      var cssay = new Keyword("CSSAY") { Parent = cassay };
      AddKeywordToContextIfNotExists(cssay, context);

      var fourcbble = new Keyword("FOURCBBLE") { Parent = fourcobble };
      AddKeywordToContextIfNotExists(fourcbble, context);

      var fredrck = new Keyword("FREDRCK") { Parent = frederick };
      AddKeywordToContextIfNotExists(fredrck, context);

      var trracta = new Keyword("TRRACTA") { Parent = terracotta };
      AddKeywordToContextIfNotExists(trracta, context);

      var wthrd = new Keyword("WTHRD") { Parent = weathered };
      AddKeywordToContextIfNotExists(wthrd, context);

      var almeda = new Keyword("ALMEDA") { Parent = alameda };
      AddKeywordToContextIfNotExists(almeda, context);

      var anch = new Keyword("ANCH") { Parent = anchor };
      AddKeywordToContextIfNotExists(anch, context);

      var anchr = new Keyword("ANCHR") { Parent = anchor };
      AddKeywordToContextIfNotExists(anchr, context);

      var ancr = new Keyword("ANCR") { Parent = anchor };
      AddKeywordToContextIfNotExists(ancr, context);

      var arcdn = new Keyword("ARCDN") { Parent = arcadian };
      AddKeywordToContextIfNotExists(arcdn, context);

      var ashbry = new Keyword("ASHBRY") { Parent = ashberry };
      AddKeywordToContextIfNotExists(ashbry, context);

      var ashld = new Keyword("ASHLD") { Parent = ashland };
      AddKeywordToContextIfNotExists(ashld, context);

      var ashlnd = new Keyword("ASHLND") { Parent = ashland };
      AddKeywordToContextIfNotExists(ashlnd, context);

      var aspn = new Keyword("ASPN") { Parent = aspen };
      AddKeywordToContextIfNotExists(aspn, context);

      var atm = new Keyword("ATM") { Parent = autumn };
      AddKeywordToContextIfNotExists(atm, context);

      var atmn = new Keyword("ATMN") { Parent = autumn };
      AddKeywordToContextIfNotExists(atmn, context);

      var blk = new Keyword("BLK") { Parent = black };
      AddKeywordToContextIfNotExists(blk, context);

      var bld = new Keyword("BLD") { Parent = blend };
      AddKeywordToContextIfNotExists(bld, context);

      var blnd = new Keyword("BLND") { Parent = blend };
      AddKeywordToContextIfNotExists(blnd, context);

      var bock = new Keyword("BOCK") { Parent = block };
      AddKeywordToContextIfNotExists(bock, context);

      var brckfc = new Keyword("BRCKFC") { Parent = brickface };
      AddKeywordToContextIfNotExists(brckfc, context);

      var brckfce = new Keyword("BRCKFCE") { Parent = brickface };
      AddKeywordToContextIfNotExists(brckfce, context);

      var br = new Keyword("BR") { Parent = brown };
      AddKeywordToContextIfNotExists(br, context);

      var brn = new Keyword("BRN") { Parent = brown };
      AddKeywordToContextIfNotExists(brn, context);

      var brw = new Keyword("BRW") { Parent = brown };
      AddKeywordToContextIfNotExists(brw, context);

      var brwn = new Keyword("BRWN") { Parent = brown };
      AddKeywordToContextIfNotExists(brwn, context);

      var bf = new Keyword("BF") { Parent = buff };
      AddKeywordToContextIfNotExists(bf, context);

      var bff = new Keyword("BFF") { Parent = buff };
      AddKeywordToContextIfNotExists(bff, context);

      var buf = new Keyword("BUF") { Parent = buff };
      AddKeywordToContextIfNotExists(buf, context);

      var camdn = new Keyword("CAMDN") { Parent = camden };
      AddKeywordToContextIfNotExists(camdn, context);

      var chandl = new Keyword("CHANDL") { Parent = chandler };
      AddKeywordToContextIfNotExists(chandl, context);

      var chndlr = new Keyword("CHNDLR") { Parent = chandler };
      AddKeywordToContextIfNotExists(chndlr, context);

      var chnlr = new Keyword("CHNLR") { Parent = chandler };
      AddKeywordToContextIfNotExists(chnlr, context);

      var ch = new Keyword("CH") { Parent = charcoal };
      AddKeywordToContextIfNotExists(ch, context);

      var charq = new Keyword("CHAR") { Parent = charcoal };
      AddKeywordToContextIfNotExists(charq, context);

      var charcaol = new Keyword("CHARCAOL") { Parent = charcoal };
      AddKeywordToContextIfNotExists(charcaol, context);

      var chr = new Keyword("CHR") { Parent = charcoal };
      AddKeywordToContextIfNotExists(chr, context);

      var chisled = new Keyword("CHISLED") { Parent = chiseled };
      AddKeywordToContextIfNotExists(chisled, context);

      var chisleled = new Keyword("CHISLELED") { Parent = chiseled };
      AddKeywordToContextIfNotExists(chisleled, context);

      var cbbl = new Keyword("CBBL") { Parent = cobble };
      AddKeywordToContextIfNotExists(cbbl, context);

      var cbl = new Keyword("CBL") { Parent = cobble };
      AddKeywordToContextIfNotExists(cbl, context);

      var cob = new Keyword("COB") { Parent = cobble };
      AddKeywordToContextIfNotExists(cob, context);

      var cobbl = new Keyword("COBBL") { Parent = cobble };
      AddKeywordToContextIfNotExists(cobbl, context);

      var cobl = new Keyword("COBL") { Parent = cobble };
      AddKeywordToContextIfNotExists(cobl, context);

      var cbblstn = new Keyword("CBBLSTN") { Parent = cobblestone };
      AddKeywordToContextIfNotExists(cbblstn, context);

      var cncd = new Keyword("CNCD") { Parent = concord };
      AddKeywordToContextIfNotExists(cncd, context);

      var cnr = new Keyword("CNR") { Parent = corner };
      AddKeywordToContextIfNotExists(cnr, context);

      var cor = new Keyword("COR") { Parent = corner };
      AddKeywordToContextIfNotExists(cor, context);

      var cntry = new Keyword("CNTRY") { Parent = country };
      AddKeywordToContextIfNotExists(cntry, context);

      var cnty = new Keyword("CNTY") { Parent = country };
      AddKeywordToContextIfNotExists(cnty, context);

      var cny = new Keyword("CNY") { Parent = country };
      AddKeywordToContextIfNotExists(cny, context);

      var ct = new Keyword("CT") { Parent = country };
      AddKeywordToContextIfNotExists(ct, context);

      var cmbrlnd = new Keyword("CMBRLND") { Parent = cumberland };
      AddKeywordToContextIfNotExists(cmbrlnd, context);

      var dcn = new Keyword("DCN") { Parent = duncan };
      AddKeywordToContextIfNotExists(dcn, context);

      var dncn = new Keyword("DNCN") { Parent = duncan };
      AddKeywordToContextIfNotExists(dncn, context);

      var edg = new Keyword("EDG") { Parent = edger };
      AddKeywordToContextIfNotExists(edg, context);

      var edgerer = new Keyword("EDGERER") { Parent = edger };
      AddKeywordToContextIfNotExists(edgerer, context);

      var edgr = new Keyword("EDGR") { Parent = edger };
      AddKeywordToContextIfNotExists(edgr, context);

      var evrst = new Keyword("EVRST") { Parent = everest };
      AddKeywordToContextIfNotExists(evrst, context);

      var flagstn = new Keyword("FLAGSTN") { Parent = flagstone };
      AddKeywordToContextIfNotExists(flagstn, context);

      var flgstn = new Keyword("FLGSTN") { Parent = flagstone };
      AddKeywordToContextIfNotExists(flgstn, context);

      var geometrc = new Keyword("GEOMETRC") { Parent = geometric };
      AddKeywordToContextIfNotExists(geometrc, context);

      var grnd = new Keyword("GRND") { Parent = grand };
      AddKeywordToContextIfNotExists(grnd, context);

      var gr = new Keyword("GR") { Parent = gray };
      AddKeywordToContextIfNotExists(gr, context);

      var gry = new Keyword("GRY") { Parent = gray };
      AddKeywordToContextIfNotExists(gry, context);

      var harvst = new Keyword("HARVST") { Parent = harvest };
      AddKeywordToContextIfNotExists(harvst, context);

      var hrvst = new Keyword("HRVST") { Parent = harvest };
      AddKeywordToContextIfNotExists(hrvst, context);

      var hl = new Keyword("HL") { Parent = hill };
      AddKeywordToContextIfNotExists(hl, context);

      var hlland = new Keyword("HLLAND") { Parent = holland };
      AddKeywordToContextIfNotExists(hlland, context);

      var hlld = new Keyword("HLLD") { Parent = holland };
      AddKeywordToContextIfNotExists(hlld, context);

      var hllnd = new Keyword("HLLND") { Parent = holland };
      AddKeywordToContextIfNotExists(hllnd, context);

      var hmstd = new Keyword("HMSTD") { Parent = homestead };
      AddKeywordToContextIfNotExists(hmstd, context);

      var homstd = new Keyword("HOMSTD") { Parent = homestead };
      AddKeywordToContextIfNotExists(homstd, context);

      var jaxn = new Keyword("JAXN") { Parent = jaxon };
      AddKeywordToContextIfNotExists(jaxn, context);

      var jxn = new Keyword("JXN") { Parent = jaxon };
      AddKeywordToContextIfNotExists(jxn, context);

      var lexngtn = new Keyword("LEXNGTN") { Parent = lexington };
      AddKeywordToContextIfNotExists(lexngtn, context);

      var lxingtn = new Keyword("LXINGTN") { Parent = lexington };
      AddKeywordToContextIfNotExists(lxingtn, context);

      var lxngtn = new Keyword("LXNGTN") { Parent = lexington };
      AddKeywordToContextIfNotExists(lxngtn, context);

      var limstn = new Keyword("LIMSTN") { Parent = limestone };
      AddKeywordToContextIfNotExists(limstn, context);

      var lm = new Keyword("LM") { Parent = limestone };
      AddKeywordToContextIfNotExists(lm, context);

      var lmestn = new Keyword("LMESTN") { Parent = limestone };
      AddKeywordToContextIfNotExists(lmestn, context);

      var lmst = new Keyword("LMST") { Parent = limestone };
      AddKeywordToContextIfNotExists(lmst, context);

      var lmstn = new Keyword("LMSTN") { Parent = limestone };
      AddKeywordToContextIfNotExists(lmstn, context);

      var mnr = new Keyword("MNR") { Parent = manor };
      AddKeywordToContextIfNotExists(mnr, context);

      var min = new Keyword("MIN") { Parent = mini };
      AddKeywordToContextIfNotExists(min, context);

      var pvr = new Keyword("PVR") { Parent = paver };
      AddKeywordToContextIfNotExists(pvr, context);

      var peytn = new Keyword("PEYTN") { Parent = peyton };
      AddKeywordToContextIfNotExists(peytn, context);

      var pytn = new Keyword("PYTN") { Parent = peyton };
      AddKeywordToContextIfNotExists(pytn, context);

      var plntr = new Keyword("PLNTR") { Parent = planter };
      AddKeywordToContextIfNotExists(plntr, context);

      var rd = new Keyword("RD") { Parent = red };
      AddKeywordToContextIfNotExists(rd, context);

      var sd = new Keyword("SD") { Parent = sand };
      AddKeywordToContextIfNotExists(sd, context);

      var snd = new Keyword("SND") { Parent = sand };
      AddKeywordToContextIfNotExists(snd, context);

      var srra = new Keyword("SRRA") { Parent = sierra };
      AddKeywordToContextIfNotExists(srra, context);

      var slt = new Keyword("SLT") { Parent = slate };
      AddKeywordToContextIfNotExists(slt, context);

      var sq = new Keyword("SQ") { Parent = square };
      AddKeywordToContextIfNotExists(sq, context);

      var sqre = new Keyword("SQRE") { Parent = square };
      AddKeywordToContextIfNotExists(sqre, context);

      var st = new Keyword("ST") { Parent = stone };
      AddKeywordToContextIfNotExists(st, context);

      var stn = new Keyword("STN") { Parent = stone };
      AddKeywordToContextIfNotExists(stn, context);

      var stne = new Keyword("STNE") { Parent = stone };
      AddKeywordToContextIfNotExists(stne, context);

      var stnev = new Keyword("STNEV") { Parent = stone };
      AddKeywordToContextIfNotExists(stnev, context);

      var tn = new Keyword("TN") { Parent = tan };
      AddKeywordToContextIfNotExists(tn, context);

      var tranql = new Keyword("TRANQL") { Parent = tranquil };
      AddKeywordToContextIfNotExists(tranql, context);

      var trnql = new Keyword("TRNQL") { Parent = tranquil };
      AddKeywordToContextIfNotExists(trnql, context);

      var trnqul = new Keyword("TRNQUL") { Parent = tranquil };
      AddKeywordToContextIfNotExists(trnqul, context);

      var wl = new Keyword("WL") { Parent = wall };
      AddKeywordToContextIfNotExists(wl, context);

      var wll = new Keyword("WLL") { Parent = wall };
      AddKeywordToContextIfNotExists(wll, context);

      context.SaveChanges();
    }
  }
}