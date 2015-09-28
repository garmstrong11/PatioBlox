namespace PatioBlox2016.DataAccess
{
  using System;
  using System.Collections.Generic;
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

    public static string GetSeedPath()
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

    public static void SeedUpcReplacements(PatioBloxContext context)
    {
      var ae34 = new UpcReplacement() {InvalidUpc = "712626103100", Replacement = "712626103101"};
      context.UpcReplacements.AddOrUpdate(v => v.InvalidUpc, ae34);
      var vb26 = new UpcReplacement() {InvalidUpc = "42911120453", Replacement = "042911120453"};
      context.UpcReplacements.AddOrUpdate(v => v.InvalidUpc, vb26);

      context.SaveChanges();
    }

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public static void SeedKeywords(PatioBloxContext context)
    {

      var unknown = new Keyword("NEW") {Parent = null};
      var color = new Keyword("COLOR") {Parent = null};
      var vendor = new Keyword("VENDOR") { Parent = null };
      var name = new Keyword("NAME") { Parent = null };
      var size = new Keyword("SIZE") { Parent = null };

      // Calling save changes after each so they will appear 
      // in the db in a specific order.
      context.Keywords.AddOrUpdate(v => v.Word, unknown);
      context.SaveChanges();
      context.Keywords.AddOrUpdate(v => v.Word, name);
      context.SaveChanges();
      context.Keywords.AddOrUpdate(v => v.Word, color);
      context.SaveChanges();
      context.Keywords.AddOrUpdate(v => v.Word, vendor);
      context.SaveChanges();
      context.Keywords.AddOrUpdate(v => v.Word, size);
      context.SaveChanges();

      var beveled = new Keyword("BEVELED") {Parent = name};
      context.Keywords.AddOrUpdate(k => new { k.Word, k.ParentId}, beveled);

      var marquette = new Keyword("MARQUETTE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, marquette);

      var countryside = new Keyword("COUNTRYSIDE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, countryside);

      var fourcobble = new Keyword("FOURCOBBLE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, fourcobble);

      var frederick = new Keyword("FREDERICK") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, frederick);

      var weathered = new Keyword("WEATHERED") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, weathered);

      var side = new Keyword("SIDE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, side);

      var alameda = new Keyword("ALAMEDA") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, alameda);

      var antique = new Keyword("ANTIQUE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, antique);

      var aspen = new Keyword("ASPEN") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, aspen);

      var austin = new Keyword("AUSTIN") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, austin);

      var basalt = new Keyword("BASALT") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, basalt);

      var basic = new Keyword("BASIC") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, basic);

      var belgium = new Keyword("BELGIUM") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, belgium);

      var block = new Keyword("BLOCK") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, block);

      var brick = new Keyword("BRICK") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, brick);

      var brickface = new Keyword("BRICKFACE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, brickface);

      var bullet = new Keyword("BULLET") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, bullet);

      var camden = new Keyword("CAMDEN") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, camden);

      var campton = new Keyword("CAMPTON") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, campton);

      var canyon = new Keyword("CANYON") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, canyon);

      var cap = new Keyword("CAP") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, cap);

      var chilton = new Keyword("CHILTON") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, chilton);

      var chiseled = new Keyword("CHISELED") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, chiseled);

      var chiselwall = new Keyword("CHISELWALL") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, chiselwall);

      var cobble = new Keyword("COBBLE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, cobble);

      var cobblestone = new Keyword("COBBLESTONE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, cobblestone);

      var concord = new Keyword("CONCORD") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, concord);

      var corner = new Keyword("CORNER") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, corner);

      var country = new Keyword("COUNTRY") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, country);

      var cumberland = new Keyword("CUMBERLAND") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, cumberland);

      var custom = new Keyword("CUSTOM") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, custom);

      var doublesplit = new Keyword("DOUBLESPLIT") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, doublesplit);

      var durango = new Keyword("DURANGO") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, durango);

      var dutch = new Keyword("DUTCH") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, dutch);

      var edger = new Keyword("EDGER") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, edger);

      var edinburgh = new Keyword("EDINBURGH") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, edinburgh);

      var everest = new Keyword("EVEREST") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, everest);

      var flagstone = new Keyword("FLAGSTONE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, flagstone);

      var flash = new Keyword("FLASH") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, flash);

      var footnotes = new Keyword("FOOTNOTES") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, footnotes);

      var fresco = new Keyword("FRESCO") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, fresco);

      var galena = new Keyword("GALENA") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, galena);

      var garden = new Keyword("GARDEN") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, garden);

      var geometric = new Keyword("GEOMETRIC") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, geometric);

      var german = new Keyword("GERMAN") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, german);

      var grand = new Keyword("GRAND") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, grand);

      var grandstone = new Keyword("GRANDSTONE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, grandstone);

      var hampton = new Keyword("HAMPTON") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, hampton);

      var holland = new Keyword("HOLLAND") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, holland);

      var homestead = new Keyword("HOMESTEAD") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, homestead);

      var hudson = new Keyword("HUDSON") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, hudson);

      var insignia = new Keyword("INSIGNIA") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, insignia);

      var joint = new Keyword("JOINT") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, joint);

      var jumbo = new Keyword("JUMBO") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, jumbo);

      var lakestone = new Keyword("LAKESTONE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, lakestone);

      var laredo = new Keyword("LAREDO") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, laredo);

      var ledge = new Keyword("LEDGE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, ledge);

      var ledgewall = new Keyword("LEDGEWALL") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, ledgewall);

      var lexington = new Keyword("LEXINGTON") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, lexington);

      var log = new Keyword("LOG") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, log);

      var manor = new Keyword("MANOR") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, manor);

      var mini = new Keyword("MINI") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, mini);

      var mission = new Keyword("MISSION") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, mission);

      var mm = new Keyword("MM") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, mm);

      var old = new Keyword("OLD") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, old);

      var patio = new Keyword("PATIO") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, patio);

      var paver = new Keyword("PAVER") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, paver);

      var pinnacle = new Keyword("PINNACLE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, pinnacle);

      var plank = new Keyword("PLANK") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, plank);

      var planter = new Keyword("PLANTER") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, planter);

      var portage = new Keyword("PORTAGE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, portage);

      var prism = new Keyword("PRISM") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, prism);

      var random = new Keyword("RANDOM") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, random);

      var rectangular = new Keyword("RECTANGULAR") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, rectangular);

      var renaissance = new Keyword("RENAISSANCE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, renaissance);

      var ring = new Keyword("RING") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, ring);

      var rivers = new Keyword("RIVERS") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, rivers);

      var riverwalk = new Keyword("RIVERWALK") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, riverwalk);

      var sandia = new Keyword("SANDIA") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, sandia);

      var sandstone = new Keyword("SANDSTONE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, sandstone);

      var scallop = new Keyword("SCALLOP") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, scallop);

      var select = new Keyword("SELECT") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, select);

      var sereno = new Keyword("SERENO") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, sereno);

      var singles = new Keyword("SINGLES") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, singles);

      var slate = new Keyword("SLATE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, slate);

      var soldier = new Keyword("SOLDIER") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, soldier);

      var southwest = new Keyword("SOUTHWEST") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, southwest);

      var splashblock = new Keyword("SPLASHBLOCK") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, splashblock);

      var split = new Keyword("SPLIT") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, split);

      var square = new Keyword("SQUARE") { Parent = size };
      context.Keywords.AddOrUpdate(k => k.Word, square);

      var stacked = new Keyword("STACKED") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, stacked);

      var stepper = new Keyword("STEPPER") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, stepper);

      var stone = new Keyword("STONE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, stone);

      var straight = new Keyword("STRAIGHT") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, straight);

      var tahoe = new Keyword("TAHOE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, tahoe);

      var tof = new Keyword("TOF") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, tof);

      var tree = new Keyword("TREE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, tree);

      var tumbled = new Keyword("TUMBLED") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, tumbled);

      var vrnda = new Keyword("VRNDA") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, vrnda);

      var wall = new Keyword("WALL") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, wall);

      var wallstone = new Keyword("WALLSTONE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, wallstone);

      var wetcast = new Keyword("WETCAST") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, wetcast);

      var yorkstone = new Keyword("YORKSTONE") { Parent = name };
      context.Keywords.AddOrUpdate(k => k.Word, yorkstone);

      context.SaveChanges();

      var allegheny = new Keyword("ALLEGHENY") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, allegheny);

      var cappuccino = new Keyword("CAPPUCCINO") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, cappuccino);

      var surrey = new Keyword("SURREY") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, surrey);

      var toffee = new Keyword("TOFFEE") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, toffee);

      var chaparral = new Keyword("CHAPARRAL") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, chaparral);

      var terracotta = new Keyword("TERRACOTTA") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, terracotta);

      var adobe = new Keyword("ADOBE") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, adobe);

      var arcadian = new Keyword("ARCADIAN") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, arcadian);

      var ash = new Keyword("ASH") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, ash);

      var ashberry = new Keyword("ASHBERRY") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, ashberry);

      var ashland = new Keyword("ASHLAND") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, ashland);

      var autumn = new Keyword("AUTUMN") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, autumn);

      var black = new Keyword("BLACK") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, black);

      var blend = new Keyword("BLEND") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, blend);

      var britt = new Keyword("BRITT") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, britt);

      var brown = new Keyword("BROWN") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, brown);

      var buff = new Keyword("BUFF") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, buff);

      var california = new Keyword("CALIFORNIA") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, california);

      var chandler = new Keyword("CHANDLER") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, chandler);

      var charcoal = new Keyword("CHARCOAL") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, charcoal);

      var coffee = new Keyword("COFFEE") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, coffee);

      var copper = new Keyword("COPPER") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, copper);

      var creek = new Keyword("CREEK") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, creek);

      var dark = new Keyword("DARK") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, dark);

      var desert = new Keyword("DESERT") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, desert);

      var duncan = new Keyword("DUNCAN") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, duncan);

      var everglade = new Keyword("EVERGLADE") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, everglade);

      var gold = new Keyword("GOLD") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, gold);

      var goldrush = new Keyword("GOLDRUSH") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, goldrush);

      var gray = new Keyword("GRAY") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, gray);

      var grey = new Keyword("GREY") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, grey);

      var harvest = new Keyword("HARVEST") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, harvest);

      var hill = new Keyword("HILL") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, hill);

      var jaxon = new Keyword("JAXON") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, jaxon);

      var limestone = new Keyword("LIMESTONE") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, limestone);

      var natural = new Keyword("NATURAL") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, natural);

      var oakrun = new Keyword("OAKRUN") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, oakrun);

      var pastello = new Keyword("PASTELLO") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, pastello);

      var peach = new Keyword("PEACH") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, peach);

      var peyton = new Keyword("PEYTON") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, peyton);

      var postiano = new Keyword("POSTIANO") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, postiano);

      var red = new Keyword("RED") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, red);

      var river = new Keyword("RIVER") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, river);

      var rose = new Keyword("ROSE") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, rose);

      var rush = new Keyword("RUSH") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, rush);

      var sand = new Keyword("SAND") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, sand);

      var sandy = new Keyword("SANDY") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, sandy);

      var sierra = new Keyword("SIERRA") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, sierra);

      var sierrgray = new Keyword("SIERRGRAY") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, sierrgray);

      var smoke = new Keyword("SMOKE") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, smoke);

      var sonoma = new Keyword("SONOMA") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, sonoma);

      var sunset = new Keyword("SUNSET") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, sunset);

      var tan = new Keyword("TAN") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, tan);

      var tranquil = new Keyword("TRANQUIL") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, tranquil);

      var veranda = new Keyword("VERANDA") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, veranda);

      var walnut = new Keyword("WALNUT") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, walnut);

      var white = new Keyword("WHITE") { Parent = color };
      context.Keywords.AddOrUpdate(k => k.Word, white);

      context.SaveChanges();

      var pacificclay = new Keyword("PACIFICCLAY") { Parent = vendor };
      context.Keywords.AddOrUpdate(k => k.Word, pacificclay);

      var riccobene = new Keyword("RICCOBENE") { Parent = vendor };
      context.Keywords.AddOrUpdate(k => k.Word, riccobene);

      var countrystone = new Keyword("COUNTRYSTONE") { Parent = vendor };
      context.Keywords.AddOrUpdate(k => k.Word, countrystone);

      var cassay = new Keyword("CASSAY") { Parent = vendor };
      context.Keywords.AddOrUpdate(k => k.Word, cassay);

      var aR = new Keyword("A+R") { Parent = vendor };
      context.Keywords.AddOrUpdate(k => k.Word, aR);

      var anchor = new Keyword("ANCHOR") { Parent = vendor };
      context.Keywords.AddOrUpdate(k => k.Word, anchor);

      context.SaveChanges();

      var alghn = new Keyword("ALGHN") { Parent = allegheny };
      context.Keywords.AddOrUpdate(k => k.Word, alghn);

      var alghny = new Keyword("ALGHNY") { Parent = allegheny };
      context.Keywords.AddOrUpdate(k => k.Word, alghny);

      var algny = new Keyword("ALGNY") { Parent = allegheny };
      context.Keywords.AddOrUpdate(k => k.Word, algny);

      var allghny = new Keyword("ALLGHNY") { Parent = allegheny };
      context.Keywords.AddOrUpdate(k => k.Word, allghny);

      var bvld = new Keyword("BVLD") { Parent = beveled };
      context.Keywords.AddOrUpdate(k => k.Word, bvld);

      var paccly = new Keyword("PACCLY") { Parent = pacificclay };
      context.Keywords.AddOrUpdate(k => k.Word, paccly);

      var pcly = new Keyword("PCLY") { Parent = pacificclay };
      context.Keywords.AddOrUpdate(k => k.Word, pcly);

      var rcbne = new Keyword("RCBNE") { Parent = riccobene };
      context.Keywords.AddOrUpdate(k => k.Word, rcbne);

      var capachno = new Keyword("CAPACHNO") { Parent = cappuccino };
      context.Keywords.AddOrUpdate(k => k.Word, capachno);

      var capcino = new Keyword("CAPCINO") { Parent = cappuccino };
      context.Keywords.AddOrUpdate(k => k.Word, capcino);

      var capcno = new Keyword("CAPCNO") { Parent = cappuccino };
      context.Keywords.AddOrUpdate(k => k.Word, capcno);

      var cappcno = new Keyword("CAPPCNO") { Parent = cappuccino };
      context.Keywords.AddOrUpdate(k => k.Word, cappcno);

      var cpchn = new Keyword("CPCHN") { Parent = cappuccino };
      context.Keywords.AddOrUpdate(k => k.Word, cpchn);

      var marqutte = new Keyword("MARQUTTE") { Parent = marquette };
      context.Keywords.AddOrUpdate(k => k.Word, marqutte);

      var sur = new Keyword("SUR") { Parent = surrey };
      context.Keywords.AddOrUpdate(k => k.Word, sur);

      var chprl = new Keyword("CHPRL") { Parent = chaparral };
      context.Keywords.AddOrUpdate(k => k.Word, chprl);

      var cnst = new Keyword("CNST") { Parent = countrystone };
      context.Keywords.AddOrUpdate(k => k.Word, cnst);

      var cntst = new Keyword("CNTST") { Parent = countrystone };
      context.Keywords.AddOrUpdate(k => k.Word, cntst);

      var cntrysd = new Keyword("CNTRYSD") { Parent = countryside };
      context.Keywords.AddOrUpdate(k => k.Word, cntrysd);

      var countrysid = new Keyword("COUNTRYSID") { Parent = countryside };
      context.Keywords.AddOrUpdate(k => k.Word, countrysid);

      var cssay = new Keyword("CSSAY") { Parent = cassay };
      context.Keywords.AddOrUpdate(k => k.Word, cssay);

      var fourcbble = new Keyword("FOURCBBLE") { Parent = fourcobble };
      context.Keywords.AddOrUpdate(k => k.Word, fourcbble);

      var fredrck = new Keyword("FREDRCK") { Parent = frederick };
      context.Keywords.AddOrUpdate(k => k.Word, fredrck);

      var trracta = new Keyword("TRRACTA") { Parent = terracotta };
      context.Keywords.AddOrUpdate(k => k.Word, trracta);

      var wthrd = new Keyword("WTHRD") { Parent = weathered };
      context.Keywords.AddOrUpdate(k => k.Word, wthrd);

      var almeda = new Keyword("ALMEDA") { Parent = alameda };
      context.Keywords.AddOrUpdate(k => k.Word, almeda);

      var anch = new Keyword("ANCH") { Parent = anchor };
      context.Keywords.AddOrUpdate(k => k.Word, anch);

      var anchr = new Keyword("ANCHR") { Parent = anchor };
      context.Keywords.AddOrUpdate(k => k.Word, anchr);

      var ancr = new Keyword("ANCR") { Parent = anchor };
      context.Keywords.AddOrUpdate(k => k.Word, ancr);

      var arcdn = new Keyword("ARCDN") { Parent = arcadian };
      context.Keywords.AddOrUpdate(k => k.Word, arcdn);

      var ashbry = new Keyword("ASHBRY") { Parent = ashberry };
      context.Keywords.AddOrUpdate(k => k.Word, ashbry);

      var ashld = new Keyword("ASHLD") { Parent = ashland };
      context.Keywords.AddOrUpdate(k => k.Word, ashld);

      var ashlnd = new Keyword("ASHLND") { Parent = ashland };
      context.Keywords.AddOrUpdate(k => k.Word, ashlnd);

      var aspn = new Keyword("ASPN") { Parent = aspen };
      context.Keywords.AddOrUpdate(k => k.Word, aspn);

      var atm = new Keyword("ATM") { Parent = autumn };
      context.Keywords.AddOrUpdate(k => k.Word, atm);

      var atmn = new Keyword("ATMN") { Parent = autumn };
      context.Keywords.AddOrUpdate(k => k.Word, atmn);

      var blk = new Keyword("BLK") { Parent = black };
      context.Keywords.AddOrUpdate(k => k.Word, blk);

      var bld = new Keyword("BLD") { Parent = blend };
      context.Keywords.AddOrUpdate(k => k.Word, bld);

      var blnd = new Keyword("BLND") { Parent = blend };
      context.Keywords.AddOrUpdate(k => k.Word, blnd);

      var bock = new Keyword("BOCK") { Parent = block };
      context.Keywords.AddOrUpdate(k => k.Word, bock);

      var brckfc = new Keyword("BRCKFC") { Parent = brickface };
      context.Keywords.AddOrUpdate(k => k.Word, brckfc);

      var brckfce = new Keyword("BRCKFCE") { Parent = brickface };
      context.Keywords.AddOrUpdate(k => k.Word, brckfce);

      var br = new Keyword("BR") { Parent = brown };
      context.Keywords.AddOrUpdate(k => k.Word, br);

      var brn = new Keyword("BRN") { Parent = brown };
      context.Keywords.AddOrUpdate(k => k.Word, brn);

      var brw = new Keyword("BRW") { Parent = brown };
      context.Keywords.AddOrUpdate(k => k.Word, brw);

      var brwn = new Keyword("BRWN") { Parent = brown };
      context.Keywords.AddOrUpdate(k => k.Word, brwn);

      var bf = new Keyword("BF") { Parent = buff };
      context.Keywords.AddOrUpdate(k => k.Word, bf);

      var bff = new Keyword("BFF") { Parent = buff };
      context.Keywords.AddOrUpdate(k => k.Word, bff);

      var buf = new Keyword("BUF") { Parent = buff };
      context.Keywords.AddOrUpdate(k => k.Word, buf);

      var camdn = new Keyword("CAMDN") { Parent = camden };
      context.Keywords.AddOrUpdate(k => k.Word, camdn);

      var chandl = new Keyword("CHANDL") { Parent = chandler };
      context.Keywords.AddOrUpdate(k => k.Word, chandl);

      var chndlr = new Keyword("CHNDLR") { Parent = chandler };
      context.Keywords.AddOrUpdate(k => k.Word, chndlr);

      var chnlr = new Keyword("CHNLR") { Parent = chandler };
      context.Keywords.AddOrUpdate(k => k.Word, chnlr);

      var ch = new Keyword("CH") { Parent = charcoal };
      context.Keywords.AddOrUpdate(k => k.Word, ch);

      var charq = new Keyword("CHAR") { Parent = charcoal };
      context.Keywords.AddOrUpdate(k => k.Word, charq);

      var charcaol = new Keyword("CHARCAOL") { Parent = charcoal };
      context.Keywords.AddOrUpdate(k => k.Word, charcaol);

      var chr = new Keyword("CHR") { Parent = charcoal };
      context.Keywords.AddOrUpdate(k => k.Word, chr);

      var chisled = new Keyword("CHISLED") { Parent = chiseled };
      context.Keywords.AddOrUpdate(k => k.Word, chisled);

      var chisleled = new Keyword("CHISLELED") { Parent = chiseled };
      context.Keywords.AddOrUpdate(k => k.Word, chisleled);

      var cbbl = new Keyword("CBBL") { Parent = cobble };
      context.Keywords.AddOrUpdate(k => k.Word, cbbl);

      var cbl = new Keyword("CBL") { Parent = cobble };
      context.Keywords.AddOrUpdate(k => k.Word, cbl);

      var cob = new Keyword("COB") { Parent = cobble };
      context.Keywords.AddOrUpdate(k => k.Word, cob);

      var cobbl = new Keyword("COBBL") { Parent = cobble };
      context.Keywords.AddOrUpdate(k => k.Word, cobbl);

      var cobl = new Keyword("COBL") { Parent = cobble };
      context.Keywords.AddOrUpdate(k => k.Word, cobl);

      var cbblstn = new Keyword("CBBLSTN") { Parent = cobblestone };
      context.Keywords.AddOrUpdate(k => k.Word, cbblstn);

      var cncd = new Keyword("CNCD") { Parent = concord };
      context.Keywords.AddOrUpdate(k => k.Word, cncd);

      var cnr = new Keyword("CNR") { Parent = corner };
      context.Keywords.AddOrUpdate(k => k.Word, cnr);

      var cor = new Keyword("COR") { Parent = corner };
      context.Keywords.AddOrUpdate(k => k.Word, cor);

      var cntry = new Keyword("CNTRY") { Parent = country };
      context.Keywords.AddOrUpdate(k => k.Word, cntry);

      var cnty = new Keyword("CNTY") { Parent = country };
      context.Keywords.AddOrUpdate(k => k.Word, cnty);

      var cny = new Keyword("CNY") { Parent = country };
      context.Keywords.AddOrUpdate(k => k.Word, cny);

      var ct = new Keyword("CT") { Parent = country };
      context.Keywords.AddOrUpdate(k => k.Word, ct);

      var cmbrlnd = new Keyword("CMBRLND") { Parent = cumberland };
      context.Keywords.AddOrUpdate(k => k.Word, cmbrlnd);

      var dcn = new Keyword("DCN") { Parent = duncan };
      context.Keywords.AddOrUpdate(k => k.Word, dcn);

      var dncn = new Keyword("DNCN") { Parent = duncan };
      context.Keywords.AddOrUpdate(k => k.Word, dncn);

      var edg = new Keyword("EDG") { Parent = edger };
      context.Keywords.AddOrUpdate(k => k.Word, edg);

      var edgerer = new Keyword("EDGERER") { Parent = edger };
      context.Keywords.AddOrUpdate(k => k.Word, edgerer);

      var edgr = new Keyword("EDGR") { Parent = edger };
      context.Keywords.AddOrUpdate(k => k.Word, edgr);

      var evrst = new Keyword("EVRST") { Parent = everest };
      context.Keywords.AddOrUpdate(k => k.Word, evrst);

      var flagstn = new Keyword("FLAGSTN") { Parent = flagstone };
      context.Keywords.AddOrUpdate(k => k.Word, flagstn);

      var flgstn = new Keyword("FLGSTN") { Parent = flagstone };
      context.Keywords.AddOrUpdate(k => k.Word, flgstn);

      var geometrc = new Keyword("GEOMETRC") { Parent = geometric };
      context.Keywords.AddOrUpdate(k => k.Word, geometrc);

      var grnd = new Keyword("GRND") { Parent = grand };
      context.Keywords.AddOrUpdate(k => k.Word, grnd);

      var gr = new Keyword("GR") { Parent = gray };
      context.Keywords.AddOrUpdate(k => k.Word, gr);

      var gry = new Keyword("GRY") { Parent = gray };
      context.Keywords.AddOrUpdate(k => k.Word, gry);

      var harvst = new Keyword("HARVST") { Parent = harvest };
      context.Keywords.AddOrUpdate(k => k.Word, harvst);

      var hrvst = new Keyword("HRVST") { Parent = harvest };
      context.Keywords.AddOrUpdate(k => k.Word, hrvst);

      var hl = new Keyword("HL") { Parent = hill };
      context.Keywords.AddOrUpdate(k => k.Word, hl);

      var hlland = new Keyword("HLLAND") { Parent = holland };
      context.Keywords.AddOrUpdate(k => k.Word, hlland);

      var hlld = new Keyword("HLLD") { Parent = holland };
      context.Keywords.AddOrUpdate(k => k.Word, hlld);

      var hllnd = new Keyword("HLLND") { Parent = holland };
      context.Keywords.AddOrUpdate(k => k.Word, hllnd);

      var hmstd = new Keyword("HMSTD") { Parent = homestead };
      context.Keywords.AddOrUpdate(k => k.Word, hmstd);

      var homstd = new Keyword("HOMSTD") { Parent = homestead };
      context.Keywords.AddOrUpdate(k => k.Word, homstd);

      var jaxn = new Keyword("JAXN") { Parent = jaxon };
      context.Keywords.AddOrUpdate(k => k.Word, jaxn);

      var jxn = new Keyword("JXN") { Parent = jaxon };
      context.Keywords.AddOrUpdate(k => k.Word, jxn);

      var lexngtn = new Keyword("LEXNGTN") { Parent = lexington };
      context.Keywords.AddOrUpdate(k => k.Word, lexngtn);

      var lxingtn = new Keyword("LXINGTN") { Parent = lexington };
      context.Keywords.AddOrUpdate(k => k.Word, lxingtn);

      var lxngtn = new Keyword("LXNGTN") { Parent = lexington };
      context.Keywords.AddOrUpdate(k => k.Word, lxngtn);

      var limstn = new Keyword("LIMSTN") { Parent = limestone };
      context.Keywords.AddOrUpdate(k => k.Word, limstn);

      var lm = new Keyword("LM") { Parent = limestone };
      context.Keywords.AddOrUpdate(k => k.Word, lm);

      var lmestn = new Keyword("LMESTN") { Parent = limestone };
      context.Keywords.AddOrUpdate(k => k.Word, lmestn);

      var lmst = new Keyword("LMST") { Parent = limestone };
      context.Keywords.AddOrUpdate(k => k.Word, lmst);

      var lmstn = new Keyword("LMSTN") { Parent = limestone };
      context.Keywords.AddOrUpdate(k => k.Word, lmstn);

      var mnr = new Keyword("MNR") { Parent = manor };
      context.Keywords.AddOrUpdate(k => k.Word, mnr);

      var min = new Keyword("MIN") { Parent = mini };
      context.Keywords.AddOrUpdate(k => k.Word, min);

      var pvr = new Keyword("PVR") { Parent = paver };
      context.Keywords.AddOrUpdate(k => k.Word, pvr);

      var peytn = new Keyword("PEYTN") { Parent = peyton };
      context.Keywords.AddOrUpdate(k => k.Word, peytn);

      var pytn = new Keyword("PYTN") { Parent = peyton };
      context.Keywords.AddOrUpdate(k => k.Word, pytn);

      var plntr = new Keyword("PLNTR") { Parent = planter };
      context.Keywords.AddOrUpdate(k => k.Word, plntr);

      var rd = new Keyword("RD") { Parent = red };
      context.Keywords.AddOrUpdate(k => k.Word, rd);

      var sd = new Keyword("SD") { Parent = sand };
      context.Keywords.AddOrUpdate(k => k.Word, sd);

      var snd = new Keyword("SND") { Parent = sand };
      context.Keywords.AddOrUpdate(k => k.Word, snd);

      var srra = new Keyword("SRRA") { Parent = sierra };
      context.Keywords.AddOrUpdate(k => k.Word, srra);

      var slt = new Keyword("SLT") { Parent = slate };
      context.Keywords.AddOrUpdate(k => k.Word, slt);

      var sq = new Keyword("SQ") { Parent = square };
      context.Keywords.AddOrUpdate(k => k.Word, sq);

      var sqre = new Keyword("SQRE") { Parent = square };
      context.Keywords.AddOrUpdate(k => k.Word, sqre);

      var st = new Keyword("ST") { Parent = stone };
      context.Keywords.AddOrUpdate(k => k.Word, st);

      var stn = new Keyword("STN") { Parent = stone };
      context.Keywords.AddOrUpdate(k => k.Word, stn);

      var stne = new Keyword("STNE") { Parent = stone };
      context.Keywords.AddOrUpdate(k => k.Word, stne);

      var stnev = new Keyword("STNEV") { Parent = stone };
      context.Keywords.AddOrUpdate(k => k.Word, stnev);

      var tn = new Keyword("TN") { Parent = tan };
      context.Keywords.AddOrUpdate(k => k.Word, tn);

      var tranql = new Keyword("TRANQL") { Parent = tranquil };
      context.Keywords.AddOrUpdate(k => k.Word, tranql);

      var trnql = new Keyword("TRNQL") { Parent = tranquil };
      context.Keywords.AddOrUpdate(k => k.Word, trnql);

      var trnqul = new Keyword("TRNQUL") { Parent = tranquil };
      context.Keywords.AddOrUpdate(k => k.Word, trnqul);

      var wl = new Keyword("WL") { Parent = wall };
      context.Keywords.AddOrUpdate(k => k.Word, wl);

      var wll = new Keyword("WLL") { Parent = wall };
      context.Keywords.AddOrUpdate(k => k.Word, wll);

      context.SaveChanges();
    }
  }
}