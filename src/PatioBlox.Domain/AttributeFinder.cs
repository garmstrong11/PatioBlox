namespace PatioBlox.Domain
{
	using System.Collections.Generic;

	public class AttributeFinder
	{
		public AttributeType Type { get; set; }
		public string SearchExpression { get; set; }
		public string Expansion { get; set; }

		public static List<AttributeFinder> Finders
		{
			get
			{
				return new List<AttributeFinder>
					{
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^(A\+R) (?!BERTRAM|CASSAY|INSIGNIA|LUXORA)",
						Expansion = "A+R"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^(A\+R BERTRAM)",
						Expansion = "A+R Bertram"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^(A\+R CASSAY)",
						Expansion = "A+R Cassay"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^(A\+R INSIGNIA)",
						Expansion = "A+R Insignia"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^(A\+R LUXORA)",
						Expansion = "A+R Luxora"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^ALA?MEDA",
						Expansion = "Alameda"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^ANCHO?R?",
						Expansion = "Anchor"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^CNTST",
						Expansion = "Country Stone"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^CUSTOM",
						Expansion = "Custom"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^DAVIS",
						Expansion = "Davis"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^FULTON",
						Expansion = "Fulton"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^GARDEN",
						Expansion = "Garden"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^PA?CC?LY",
						Expansion = "Pacific Clay"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^SELECT",
						Expansion = "Select"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^SOUTHWEST",
						Expansion = "Southwest"
						},
						new AttributeFinder
						{
						Type = AttributeType.Vendor,
						SearchExpression = @"^RCBNE",
						Expansion = "Riccobene"
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"(STEPPING *|PATIO *|FLAG *|DUTCH *)?(COBBLE *|GRAND *|SAND *|ASPEN *|LAREDO *|DURANGO *|LEXINGTON MINI *|YORK *|FOOTNOTES *|PORTAGE *|CANYON *|DUTCH *|EVE?RE?ST WA?LL *|PINNACLE *|RIVERWALK |MINI *|LAKE *|FOURCBBLE *|GERMAN ANTIQUE *|BRICKFACE *|PRISM *)?STO?NE? ?$",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"(BASIC *|WALL *|CHISELED *|LEXINGTON *|GALENA *)?(WALL *|SPLIT *|ALAMEDA *|COUNTRYMANOR *|CM *)?CAP ?$",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"(BASIC *|COUNTRY ?MANOR *|CM *|CHISELE?D? *|LEDGE *|STACKED *|COB+LE *|(DOUBLE)?SPLIT *|ALAMEDA *|FRESCO *|HOMESTEAD *|LEXINGTON *|GALENA *|HAMPTON *|STRAIGHT *)?(RETAINING )?WALL ?$",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"(CHISELED *|COB+LE *|BULLET *|HOMESTEAD *|GEOMETRI?C *|SCALLOP *|TREE RING *|LOG *)?EDGE?R ?$",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"(TUMBLED( MISSION)? *|MISSION *)?PAVER$",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"(SPLASH *|BELGIUM *|WALL *)?BLO?C?K$",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"(LEXINGTON *|HOMESTEAD *)?CO?R?N?E?R(/SQ)?$",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"HOLLAND( 60MM)?$",
						Expansion = "Holland Paver"
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"MARQUETTE",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"(HOMESTEAD *|RANDOM *)?COBBLE ?$",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Category,
						SearchExpression = @"(SERENO *|RECTANGULAR *)?PATIO ?$",
						Expansion = ""
						},
						new AttributeFinder
						{
						Type = AttributeType.Unit,
						SearchExpression = @"IN",
						Expansion = "\""
						},
						new AttributeFinder
						{
						Type = AttributeType.Unit,
						SearchExpression = @"SQ ?FT",
						Expansion = " SqFt"
						},
						new AttributeFinder
							{
							Type = AttributeType.Color,
							SearchExpression = @"([^ ]+/[^ ]+)",
							Expansion = ""
							},
						new AttributeFinder
							{
							Type = AttributeType.Color,
							SearchExpression = @"/?(BROWN|\bTAN|CHAR(COAL)|GRAY|ASH(LAND|BERRY)|OAK( ?RUN)?|ADOBE|DESERT|SAND\b|COPPER|BUFF|EVERGLADE|ALLEGHENY|HARVEST|\bRED|DARK|BLACK|GOLD( ?RUSH)?|WHITE|ROSE|LIMESTONE|ARCADIAN|BRITT|CHANDLER|TERRACOTTA|WALNUT|CUMBERLAND|TRANQUIL|DUNCAN|SMOKE|PEACH|JAXON|FREDRICKSON|CAPPUCC?INO|VERANDA|SIERRA|BASALT|AUTUMN|PEYTON|SUNSET|SONOMA|GRIGIO)/?( BLE?N?D)?",
							Expansion = ""
							}
					};
			}
		}
	}

	public enum AttributeType
	{
		Vendor, Category, Unit, Color
	}
}
