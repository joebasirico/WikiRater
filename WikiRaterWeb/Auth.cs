using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using WikiRaterWeb;
using System.Text.RegularExpressions;
using System.Text;
using WikiRaterWeb.Properties;
using System.Configuration;
using System.Data;
using System.Linq;


/// <summary>
/// Summary description for Auth
/// </summary>
public static class Auth
{
	private static string saltyGoo = "NBFe4567uI654#2qwSDfVfrtyU*&^5$#wdFbvE$eRtUHNmIHYTe3@wsdCxsW2QasDFgHnI87^5$56&8ioKmn";

	public static int checkCredentials(string username, string password)
	{
		int count = 0;
		int userID = 0;
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
		{
			conn.Open();
			SqlCommand command = new SqlCommand("CheckUser", conn);
			command.Parameters.AddWithValue("@UserName", username);
			command.Parameters.AddWithValue("@PasswordHash", password);
			command.CommandType = CommandType.StoredProcedure;
			SqlDataReader reader = command.ExecuteReader();
			count = 0;
			userID = 0;
			while (reader.Read())
			{
				userID = reader.GetInt32(0);
				count++;
			}
		}

		if (count == 1)
			return userID;
		else
			return 0;
	}

	public static string ByteToHex(byte[] input)
	{
		string StringVersion = "";

		foreach (byte b in input)
		{
			string hexChar = b.ToString("x");
			if (hexChar.Length == 1)
				hexChar = "0" + hexChar;
			StringVersion += hexChar;
		}
		return StringVersion;
	}

	public static void UpdatePassword(int userID, string password)
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		dc.UpdatePassword(userID, password);
	}

	public static bool UserExists(string username)
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		var user = dc.CheckUserExists(username);
		bool foundUser = false;
		foreach (var id in user)
			foundUser = true;
		return foundUser;
	}

	public static void registerUser(string username, string password, string email)
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		dc.CreateUser(username, password, DateTime.Now, true, email);
	}

	public static void createSession(int userID, Guid session)
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		dc.CreateSession(userID, session, DateTime.Now);
	}

	public static int checkSession(Guid session)
	{
		int userID = 0;
		int count = 0;
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
		{
			conn.Open();
			SqlCommand command = new SqlCommand("GetUserIDBySession", conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@SessionID", session);
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				userID = reader.GetInt32(0);
				count++;
			}
		}

		if (count == 1)
			return userID;
		else
			return 0;
	}

	public static void destroySession(Guid session)
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		dc.DestroySession(session);
	}

	public static string getSaltyGoo()
	{
		return saltyGoo;
	}

	public static void CreateEvent(string title, string body, string source)
	{
		DataClassesDataContext dc = new DataClassesDataContext();
		dc.CreateEvent(title, DateTime.Now, source, body);
	}

	public static string LookupUserName(int userID)
	{
		string username = "";
		int count = 0;
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
		{
			conn.Open();
			SqlCommand command = new SqlCommand("GetUserByID", conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@UserID", userID);
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				username = reader.GetString(0);
				count++;
			}
		}

		if (count == 1)
			return username;
		else
			return "";
	}

	public static void CreateResetValue(User currentUser, Guid resetValue)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
		{
			conn.Open();		
			SqlCommand command = new SqlCommand("AddPasswordReset", conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@UserID", currentUser.UserID);
			command.Parameters.AddWithValue("@ResetValue", resetValue);
			command.Parameters.AddWithValue("@TimeCreated", System.DateTime.Now);

			command.ExecuteNonQuery();
		}
	}

	public static int CheckResetValue(Guid resetValue)
	{
		int userID = 0;
		int count = 0;
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
		{
			conn.Open();
			SqlCommand command = new SqlCommand("LookupResetValue", conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@ResetValue", resetValue);
			command.Parameters.AddWithValue("@TimeOut", DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0)));
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				userID = reader.GetInt32(0);
				count++;
			}
		}

		if (count == 1)
			return userID;
		else
			return 0;
	}

	public static void DeleteResetValue(Guid resetValue)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
		{
			conn.Open();
			SqlCommand command = new SqlCommand("DeleteResetValue", conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@ResetValue", resetValue);
			command.ExecuteNonQuery();
		}
	}

	internal static void UpdateEmail(int userID, string newEmail)
	{
		using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["WikiVoterConnectionString"].ConnectionString))
		{
			conn.Open();
			SqlCommand command = new SqlCommand("UpdateEmail", conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("@UserID", userID);
			command.Parameters.AddWithValue("@NewEmail", newEmail);

			command.ExecuteNonQuery();
		}
	}

	public static string GenerateRandomUserName()
	{
		string[] wordlist = { "proofe", "respuesta", "restricts", "saken", "sanitarium", "schoolhouse", 
								"schreckliche", "sentia", "siamo", "siguiendo", "sires", "stede", "testes",
								"timides", "tirs", "toisesta", "troupe", "tue", "unscrupulously", "vaeret", 
								"voided", "wallets", "wyll", "zwoelf", "uns", "Akenside", "Alcides", "BEMIS",
								"Bagration", "Bulger", "Caelius", "Carys", "Choisy", "Conquerors", "Corner",
								"Defeated", "EBERS", "Ernanton", "Fairlie", "Fame", "Ft", "GABRIEL", "Gandharva",
								"Glow", "Goddesses", "Grinder", "Guarini", "Hade", "Juin", "Krafft", "Marshalsea", 
								"Mathurin", "Mauprat", "Mayors", "Morna", "NON", "Penance", "Petrea", "Produkt",
								"RAILWAY", "SPAULDING", "Schemes", "Schuhe", "Shasta", "Shuttleworth", "Stanshy", 
								"TWENTY", "Tranmore", "UNEXPECTED", "VINCENT", "Vantine", "Verse", "WORDSWORTH",
								"Whig", "Wolseys", "advertises", "agere", "amateurs", "appellant", "arquebuse",
								"ascription", "balm", "bandits", "begrijpt", "beholdeth", "benzene", "bookcase", 
								"boxwood", "candidly", "caraway", "caresser", "chilliness", "coercion", 
								"compositors", "concepto", "cowman", "crestfallen", "cuddle", "decreased",
								"doubtingly", "Elder", "FRUITS", "Folding", "Freien", "Gisors", "Hertz", 
								"Hollys", "INDIANS", "Icarus", "Impressed", "Inshallah", "Istria", "Jacquemin",
								"Kranke", "Lamon", "Lashmar", "Lau", "Liberian", "Lieb", "Lippincotts", "Locked",
								"MORLEY", "Margery", "Melmottes", "Merced", "Mortsauf", "Myn", "Ormskirk", "Osages",
								"Poles", "REFORM", "ROSS", "Rosamund", "SPECIES", "STRUGGLE", "Speedwell", "Strongs",
								"Theydon", "Todd", "VV", "Vicar", "Weatherbee", "Wohnungen", "abstains", "affirmation", 
								"amaze", "apologue", "aristocrats", "ayes", "beyng", "brained", "chapelet", "comitia",
								"commodiously", "confiscating", "contr", "cuncta", "curtseying", "deserveth", "disjunction",
								"dissociation", "dogmatical", "domicile", "drager", "dredged", "fatigue", "fijn",
								"franzsische", "frosting", "glucose", "haps", "hideuse", "illustrious", "inept", 
								"instilling", "intensified", "jargon", "jis", "kaiser", "kiun", "lastre", "limpossibilit"
								, "legations", "loam", "lukewarmness", "middlemen", "mihinkn", "moonrise", "ms", 
								"nearest", "nukkui", "off", "overcometh", "painaa", "paralysed", "pardessus", 
								"persiste", "popularized", "primavera", "prompted", "aridity", "avengers", "befriedigt",
								"beschaving", "bist", "brassy", "bubble", "canvass", "carmen", "cas", "coffret", "coverage",
								"cot", "crimped", "crudeness", "debates", "distrusts", "dogmas", "drueben", "eagles",
								"enlisted", "extravagances", "fender", "fiera", "fliegt", "gegangen", "gehoerte", "gelezen",
								"gevoelens", "gobbling", "hinan", "hom", "idee", "immerhin", "impiously", "imposante", "jignore", 
								"kaupungissa", "knsi", "laria", "leaguer", "leprosy", "lmmin", "matka", "memorizing", "moechten", 
								"muriatic", "neve", "nobilis", "nomme", "oaten", "occupier", "pastorate", "perpetrated", "personified",
								"philologists", "placa", "poorly", "porringer", "praten", "processed", "quartermasters", "quedar", 
								"rideth", "ruhiger", "sequin", "sheepskins", "soldaat", "spinner", "strangled", "suffira", "tallow", 
								"thoust", "tipo", "toiselta", "transported", "viisas", "voluntarily", "wahi", "wheatmeal", "ardly", 
								"AINT", "Amaziah", "Bristow", "Brunton", "Buttons", "Carrol", "Cesar", "Chalcis", "Chemie", "Conor", 
								"Crusoe", "Cyc", "Cyclopean", "DEVIL", "DUBLIN", "Darry", "Diet", "Dinges", "Einst", "lassistance", 
								"lemporter", "linvasion", "lorries", "manquerait", "monogamy", "mriter", "nasty", "pickin", "potestad",
								"provincie", "pullin", "quadrille", "quadrupled", "railings", "reanimate", "recruit", "regnait", "revenons",
								"rodillas", "sacrificio", "sample", "secrtement", "sensitiveness", "shocks", "shoots", "simplifying", "skam",
								"sloughs", "stane", "steadiest", "stok", "tape", "tarkasti", "thumbed", "thynge", "triplets", "troth", "us", 
								"valles", "winne", "wuten", "AUTUMN", "Adelas", "Anscombe", "Aufgaben", "BELL", "Bali", "Barbaro", "Berts", 
								"Boulton", "Brookss", "Brower", "COKESON", "Cocos", "Daca", "Eldest", "Fahrenheit", "Folly", "Gandiva", "Gaol", 
								"Gesandten", "Gravy", "HAIR", "Hab", "Headlong", "Hollandsche", "Kingsleys", "Kueste", "Kpfe", "LEAGUE", "LUCY",
								"Malherbe", "Marstons", "Mess", "Morbihan", "Nasty", "Neforis", "Nothings", "Nyassa", "Offa", "Ohnmacht", "Patsy",
								"Pendyces", "Riatt", "Rizzio", "Sanction", "Secunda", "Semites", "Sprang", "Suidas", "Teilung", "Thanking", "Thurnall",
								"Vermandois", "Volumes", "Werff", "Zodiac", "alumni", "apparaissaient", "Eens", "Faugh", "Genve", "Gettysburg", "Heaton", 
								"Helots", "Holts", "Honfleur", "INCIDENT", "IPHIGENIA", "Iberians", "Iune", "Judar", "Kamchatka", "Knot", "Koffer", "Kuchen",
								"Landen", "Lena", "Livingstone", "Matthieu", "Merkmale", "Nap", "Navajos", "Noting", "Orazio", "Per", "Piedmont", "Prizes",
								"RACHEL", "Regnault", "Rhodians", "Rollins", "Seeming", "Seitz", "Selbstbewusstsein", "Selma", "Seule", "Summon", "Totty",
								"Treasures", "Waverleys", "Werte", "Woodcourt", "aggregates", "alcaldes", "aliquando", "anachronisms", "andando", "appliquer",
								"aufeinander", "bandeau", "bekannt", "bezitten", "chantry", "chri", "circulated", "ciudad", "confiscation", "consulship", "cookie", 
								"coordination", "crude", "cubs", "curieusement", "daccusation", "debtors", "decades", "demoralising", "destinees", "detrs", "devastation",
								"devoue", "dislodging", "disported", "distemper", "donnerez", "dressy", "einzelner", "empirisch", "empche", "entrez", "erforderlich",
								"erleben", "etc", "evaluation", "exuded", "forming", "freundlicher", "frutos", "gesunden", "habitent", "harangue", "imperturbability",
								"impose", "indecently", "inhuman", "intestines", "introduire", "iterum", "accusait", "agin", "aimed", "approcha", "ardours", "arrowes",
								"arial", "bals", "banco", "boezem", "bowls", "buono", "clacking", "classing", "clomb", "confrere", "considerables", "countersigned",
								"cures", "dacheter", "danimaux", "doublier", "disconsolate", "dramatiques", "edeln", "embodiments", "emellan", "entraron", "equerries",
								"gestand", "gyrations", "haenelle", "harmful", "horrifying", "hould", "huevos", "impressionist", "inducted", "insulting", "intervalle",
								"inviolably", "manikin", "nationalist", "nord", "observait", "outsider", "pag", "pelas", "personable", "phantoms", "pondering", "procul",
								"purses", "quartos", "quelconque", "reade", "renewable", "reviews", "revolutionnaires", "rifleman", "rips", "rouleau", "ruine", "sapless",
								"seconda", "sereine", "seuls", "sia", "skulls", "slab", "smearing", "sozialen", "ty", "tekisi", "thrall", "trilogy", "ungallant", "unofficially",
								"wanderd", "brige", "Open", "People", "Abandon", "Ashbourne", "Assist", "Australasian", "Balearic", "Barral", "Beamte", "Benedicts", "Bodin", 
								"Brewsters", "Brice", "Chronicle", "Cointet", "Colossians", "Corsair", "Cram", "Darrell", "Dual", "pandanus", "permets", "permits", "playfully",
								"plebiscite", "plunger", "poetas", "ponders", "pouco", "probation", "pullets", "recording", "recueillis", "regardeth", "rudiment", "scheu",
								"seale", "seisomaan", "shamelessness", "sufrir", "sumus", "superstitiously", "tarpeeksi", "terwyl", "undesired", "unmentionable", "vaimonsa",
								"vasty", "voer", "voted", "wattles", "weasels", "wetter", "wizard", "woodmen", "Jos", "tablissement", "Alexanders", "Anc", "Apprentice",
								"Aragon", "Aser", "Ashantee", "BECOME", "Bactria", "Bahr", "Barmecide", "Berenguer", "Blackett", "Bourgogne", "Cadogan", "Camperdown", 
								"DHerbelot", "Ehrgeiz", "Emo", "Fans", "Frida", "Friedens", "Fulk", "GERVASE", "Haggerty", "Handing", "Harleian", "INDUSTRY", "Individuen",
								"Judean", "Largo", "Lebeau", "Leddy", "Leontine", "Lindas", "Lippi", "Louve", "Macintosh", "Mordaunt", "Munday", "Narses", "Neighbourhood",
								"Nerves", "Nevitt", "Nulle", "PAY", "PLOT", "Private", "RECOLLECTIONS", "STEAM", "Sarudine", "Scrub", "TIMES", "TOMB", "Tapferkeit", "Terreur",
								"Translate", "Trelyon", "Viceroys", "Vill", "Vrede", "Wenlock", "Wyatt", "abondante", "Blowing", "Blushing", "CONSOLIDATED", "Cantor", "Catalogues",
								"Caton", "Clap", "Daddys", "Daw", "Druidical", "Faellen", "Groen", "Hartman", "Hay", "Hew", "Hussein", "Insolent", "Kat", "Lacroix", "Mahaffy", 
								"Manna", "Marliani", "Maynard", "Meares", "Nain", "Neffen", "Nooit", "Obwohl", "Orientalist", "Parkhurst", "Passau", "Paynim", "Pickles", 
								"Picturesque", "Pimlico", "Pledge", "Pomponia", "Protected", "QUEENS", "ROGER", "ROPER", "Raff", "Registers", "Roxanne", "SCOTLAND", "Sergeants",
								"Shorthouse", "Souls", "Students", "TESMAN", "Teacups", "Thanet", "Tods", "Wiser", "Wohlan", "abaht", "afternoone", "aiguille", "angewiesen",
								"aversions", "awaken", "bedre", "bewaard", "blieb", "blues", "boasting", "buyin", "careening", "celluloid", "chantent", "coulaient", "cultivate",
								"delineating", "dulness", "dunkler", "durcheinander", "electromotive", "engang", "excretion", "farouches", "foremans", "forrard", "heeded", 
								"indentured", "insurmountable", "japanned", "journaliste", "kaupunkiin", "kenned", "lignite", "local", "maskers", "maxim", "merito", "meseems",
								"middleman", "mompelde", "oppia", "organe", "paiement", "Rutton", "Selfish", "Seppi", "Souvenir", "Stage", "Swansea", "TOUR", "Theobald", 
								"Thorough", "Tickets", "Tonson", "Tripeaud", "Watertown", "Wolfert", "accuss", "almeno", "armfuls", "bankers", "bjudit", "calabashes", 
								"carrieth", "chronologically", "cockswain", "coming", "cruciform", "crurent", "crypts", "curable", "cursorily", "dardeur", "dandled", 
								"delire", "disguises", "dishearten", "dispiriting", "distresses", "divines", "ergab", "etymologies", "fossiliferous", "furnaces", 
								"gemeinsam", "germinated", "hallowing", "happiness", "haveing", "heartiness", "idealizing", "idiom", "inconnu", "klopfte", "lassaut",
								"liess", "litigation", "ly", "lgumes", "malachite", "mourned", "mujer", "noeuds", "pecheurs", "pedals", "person", "plunged", "poniendo", 
								"prognostic", "progressing", "puhu", "pupa", "quaffing", "ravisher", "rectory", "rescinded", "retient", "revel", "revelry", "sake", 
								"sardonically", "seald", "secondhand", "succumb", "tenuity", "tiffin", "tobacconists", "tusks", "understand", "upbuilding", "valuation",
								"war", "fter", "Adonis", "Adventure", "Amroth", "Anecdote", "Ao", "Ascension", "Astyages", "BRUNO", "Babie", "Bel" };
		Random rand = new Random();
		string username = "";
		for (int i = 0; i < 5; i++)
		{
			username += wordlist[rand.Next(wordlist.Length)];
		}
		return username;
	}
}