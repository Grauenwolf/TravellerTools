using System.Collections.Immutable;

namespace Grauenwolf.TravellerTools;

public static class Tables
{
    public static ImmutableArray<EHex> AtmosphereCodes => BuildCodes('0', 'F');
    public static ImmutableArray<EHex> GovernmentCodes => BuildCodes('0', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'W', 'X');
    public static ImmutableArray<EHex> HydrographicsCodes => BuildCodes('0', 'A');
    public static ImmutableArray<EHex> LawLevelCodes => BuildCodes('0', 'L', 'S');
    public static ImmutableArray<EHex> PopulationCodes => BuildCodes('0', 'L');
    public static ImmutableArray<EHex> SizeCodes => BuildCodes('0', 'C');
    public static ImmutableArray<EHex> StarportCodes => ImmutableArray.Create<EHex>('A', 'B', 'C', 'D', 'E', 'X', 'F', 'G', 'H', 'Y');

    public static ImmutableArray<EHex> TechLevelCodes => BuildCodes('0', 'W');

    public static string Acceptance(EHex acceptanceCode)
    {
        return (acceptanceCode.ToChar()) switch
        {
            '0' => "N/A",
            '1' => "Extremely xenophobic",
            '2' => "Very xenophobic",
            '3' => "Xenophobic",
            '4' => "Extremely aloof",
            '5' => "Very aloof",
            '6' => "Aloof",
            '7' => "Aloof",
            '8' => "Friendly",
            '9' => "Friendly",
            'A' => "Very friendly",
            'B' => "Extremely friendly",
            'C' => "Xenophilic",
            'D' => "Very Xenophilic",
            'E' => "Extremely xenophilic",
            'F' => "Extremely xenophilic",
            _ => "Unknown"
        };
    }

    public static string? Allegiance(string? allegianceCode)
    {
        return allegianceCode switch
        {
            "3EoG" or "Ga" => "Third Empire of Gashikan",
            "4Wor" or "Fw" => "Four Worlds",
            "AkUn" or "Ak" => "Akeena Union",
            "AlCo" or "Al" => "Altarean Confederation",
            "AnTC" or "Ac" => "Anubian Trade Coalition",
            "AsIf" or "As" => "Iyeaao'fte",
            "AsMw" or "As" => "Aslan Hierate, single multiple-world clan dominates",
            "AsOf" or "As" => "Oleaiy'fte",
            "AsSc" or "As" => "Aslan Hierate, multiple clans split control",
            "AsSF" or "As" => "Aslan Hierate, small facility",
            "AsT0" or "A0" => "Aslan Hierate, Tlaukhu control, Yerlyaruiwo (1), Hrawoao (13), Eisohiyw (14), Ferekhearl (19)",
            "AsT1" or "A1" => "Aslan Hierate, Tlaukhu control, Khauleairl (2), Estoieie' (16), Toaseilwi (22)",
            "AsT2" or "A2" => "Aslan Hierate, Tlaukhu control, Syoisuis (3)",
            "AsT3" or "A3" => "Aslan Hierate, Tlaukhu control, Tralyeaeawi (4), Yulraleh (12), Aiheilar (25), Riyhalaei (28)",
            "AsT4" or "A4" => "Aslan Hierate, Tlaukhu control, Eakhtiyho (5), Eteawyolei' (11), Fteweyeakh (23)",
            "AsT5" or "A5" => "Aslan Hierate, Tlaukhu control, Hlyueawi (6), Isoitiyro (15)",
            "AsT6" or "A6" => "Aslan Hierate, Tlaukhu control, Uiktawa (7), Iykyasea (17), Faowaou (27)",
            "AsT7" or "A7" => "Aslan Hierate, Tlaukhu control, Ikhtealyo (8), Tlerfearlyo (20), Yehtahikh (24)",
            "AsT8" or "A8" => "Aslan Hierate, Tlaukhu control, Seieakh (9), Akatoiloh (18), We'okunir (29)",
            "AsT9" or "A9" => "Aslan Hierate, Tlaukhu control, Aokhalte (10), Sahao' (21), Ouokhoi (26)",
            "AsTA" or "Ta" => "Tealou Arlaoh",
            "AsTv" or "As" => "Aslan Hierate, Tlaukhu vassal clan dominates",
            "AsTz" or "As" => "Aslan Hierate, Zodia clan",
            "AsVc" or "As" => "Aslan Hierate, vassal clan dominates",
            "AsWc" or "As" => "Aslan Hierate, single one-world clan dominates",
            "AsXX" or "As" => "Aslan Hierate, unknown",
            "AvCn" or "Ac" => "Avalar Consulate",
            "BaCl" or "Bc" => "Backman Cluster",
            "Bium" or "Bi" => "The Biumvirate",
            "BlSo" or "Bs" => "Belgardian Sojurnate",
            "BoWo" or "Bw" => "Border Worlds",
            "CaAs" or "Cb" => "Carrillian Assembly",
            "CaPr" or "Ca" => "Principality of Caledon",
            "CaTe" or "Ct" => "Carter Technocracy",
            "CoAl" or "Ca" => "Corsair Alliance",
            "CoBa" or "Ba" => "Confederation of Bammesuka",
            "CoLg" or "CL" => "Corellan League",
            "CoLp" or "Lp" => "Council of Leh Perash",
            "CRAk" or "CA" => "Anakudnu Cultural Region",
            "CRGe" or "CG" => "Geonee Cultural Region",
            "CRSu" or "CS" => "Suerrat Cultural Region",
            "CRVi" or "CV" => "Vilani Cultural Region",
            "CsCa" or "Ca" => "Client state, Principality of Caledon",
            "CsHv" or "Hc" => "Client state, Hive Federation",
            "CsIm" or "Cs" => "Client state, Third Imperium",
            "CsMo" or "Cm" => "Client state, Duchy of Mora",
            "CsPt" or "CP" => "Client state, The Protectorate",
            "CsRr" or "Cr" => "Client state, Republic of Regina",
            "CsTw" or "KC" => "Client state, Two Thousand Worlds",
            "CsZh" or "Cz" => "Client state, Zhodani Consulate",
            "CyUn" or "Cu" => "Cytralin Unity",
            "DaCf" or "Da" => "Darrian Confederation",
            "DeHg" or "Dh" => "Descarothe Hegemony",
            "DeNo" or "Dn" => "Demos of Nobles",
            "DiGr" or "Dg" => "Dienbach Grüpen",
            "DoAl" or "Az" => "Domain of Alntzar",
            "DuCf" or "Cd" => "Confederation of Duncinae",
            "DuMo" or "Mo" => "Duchy of Mora",
            "ECRp" or "EC" => "Eberhardt Corporate Republic",
            "EsMa" or "Es" => "Eslyat Magistracy",
            "FdAr" or "Fa" => "Federation of Arden",
            "FdDa" or "Fd" => "Federation of Daibei",
            "FdIl" or "Fi" => "Federation of Ilelish",
            "FeAl" or "Fa" => "Federation of Alsas",
            "FeAm" or "FA" => "Federation of Amil",
            "FeHe" or "Fh" => "Federation of Heron",
            "FlLe" or "Fl" => "Florian League",
            "GaFd" or "Ga" => "Galian Federation",
            "GaRp" or "Gr" => "Gamma Republic",
            "GdKa" or "Rm" => "Grand Duchy of Kalradin",
            "GdMh" or "Ma" => "Grand Duchy of Marlheim",
            "GdSt" or "Gs" => "Grand Duchy of Stoner",
            "GeOr" or "Go" => "Gerontocracy of Ormine",
            "GlEm" or "Gl" => "Glorious Empire",
            "GlFe" or "Gf" => "Glimmerdrift Federation",
            "GnCl" or "Gi" => "Gniivi Collective",
            "HaCo" or "Hc" => "Haladon Cooperative",
            "HeCo" or "HC" => "Hefrin Colony",
            "HoPA" or "Ho" => "Hochiken People's Assembly",
            "HvFd" or "Hv" => "Hive Federation",
            "HyLe" or "Hy" => "Hyperion League",
            "IHPr" or "IS" => "I'Sred*Ni Protectorate",
            "ImAp" or "Im" => "Third Imperium, Amec Protectorate",
            "ImDa" or "Im" => "Third Imperium, Domain of Antares",
            "ImDc" or "Im" => "Third Imperium, Domain of Sylea",
            "ImDd" or "Im" => "Third Imperium, Domain of Deneb",
            "ImDg" or "Im" => "Third Imperium, Domain of Gateway",
            "ImDi" or "Im" => "Third Imperium, Domain of Ilelish",
            "ImDs" or "Im" => "Third Imperium, Domain of Sol",
            "ImDv" or "Im" => "Third Imperium, Domain of Vland",
            "ImLa" or "Im" => "Third Imperium, League of Antares",
            "ImLc" or "Im" => "Third Imperium, Lancian Cultural Region",
            "ImLu" or "Im" => "Third Imperium, Luriani Cultural Association",
            "ImSy" or "Im" => "Third Imperium, Sylean Worlds",
            "ImVd" or "Ve" => "Third Imperium, Vegan Autonomous District",
            "InRp" or "Ir" => "Interstellar Republic",
            "IsDo" or "Id" => "Islaiat Dominate",
            "JAOz" or "Jo" => "Julian Protectorate, Alliance of Ozuvon",
            "JaPa" or "Ja" => "Jarnac Pashalic",
            "JAsi" or "Ja" => "Julian Protectorate, Asimikigir Confederation",
            "JCoK" or "Jc" => "Julian Protectorate, Constitution of Koekhon",
            "JHhk" or "Jh" => "Julian Protectorate, Hhkar Sphere",
            "JLum" or "Jd" => "Julian Protectorate, Lumda Dower",
            "JMen" or "Jm" => "Julian Protectorate, Commonwealth of Mendan",
            "JPSt" or "Jp" => "Julian Protectorate, Pirbarish Starlane",
            "JRar" or "Vw" => "Julian Protectorate, Rar Errall/Wolves Warren",
            "JuHl" or "Hl" => "Julian Protectorate, Hegemony of Lorean",
            "JUkh" or "Ju" => "Julian Protectorate, Ukhanzi Coordinate",
            "JuNa" or "Jn" => "Jurisdiction of Nadon",
            "JuPr" or "Jp" => "Julian Protectorate",
            "JuRu" or "Jr" => "Julian Protectorate, Rukadukaz Republic",
            "JVug" or "Jv" => "Julian Protectorate, Vugurar Dominion",
            "KaCo" or "KC" => "Katowice Conquest",
            "KaEm" or "KE" => "Katanga Empire",
            "KaTr" or "Kt" => "Kajaani Triumverate",
            "KaWo" or "KW" => "Karhyri Worlds",
            "KhLe" or "Kl" => "Khuur League",
            "KkTw" or "Kk" => "Two Thousand Worlds",
            "KoEm" or "Ko" => "Korsumug Empire",
            "KoPm" or "Pm" => "Percavid Marches",
            "KPel" or "Pe" => "Kingdom of Peladon",
            "KrPr" or "Kr" => "Krotan Primacy",
            "LeSu" or "Ls" => "League of Suns",
            "LnRp" or "Ln" => "Loyal Nineworlds Republic",
            "LuIm" or "Li" => "Lucan's Imperium",
            "LyCo" or "Ly" => "Lanyard Colonies",
            "MaCl" or "Ma" => "Mapepire Cluster",
            "MaEm" or "Mk" => "Maskai Empire",
            "MaSt" or "Ma" => "Maragaret's Domain",
            "MaUn" or "Mu" => "Malorn Union",
            "MeCo" or "Me" => "Megusard Corporate",
            "MiCo" or "Mi" => "Mische Conglomerate",
            "MnPr" or "Mn" => "Mnemosyne Principality",
            "MoLo" or "ML" => "Monarchy of Lod",
            "MrCo" or "MC" => "Mercantile Concord",
            "NaAs" or "As" => "Non-Aligned, Aslan-dominated",
            "NaCh" or "Na" => "Non-Aligned, TBD",
            "NaDr" or "Dr" => "Non-Aligned, Droyne-dominated",
            "NaHu" or "Na" => "Non-Aligned, Human-dominated",
            "NaVa" or "Va" => "Non-Aligned, Vargr-dominated",
            "NaXX" or "Na" => "Non-Aligned, unclaimed",
            "NkCo" or "NC" => "Nakris Confederation",
            "OcWs" or "Ow" => "Outcasts of the Whispering Sky",
            "OlWo" or "Ow" => "Old Worlds",
            "PlLe" or "Pl" => "Plavian League",
            "PrBr" or "PB" => "Principality of Bruhkarr",
            "Prot" or "Pt" => "The Protectorate",
            "RamW" or "RW" => "Rammak Worlds",
            "RaRa" or "Ra" => "Ral Ranta",
            "Reac" or "Rh" => "The Reach",
            "ReUn" or "Re" => "Renkard Union",
            "Rule" or "RM" => "Rule of Man",
            "SaCo" or "Sc" => "Salinaikin Concordance",
            "Sark" or "Sc" => "Sarkan Constellation",
            "SeFo" or "Sf" => "Senlis Foederate",
            "SELK" or "Lk" => "Sha Elden Lith Kindriu",
            "ShRp" or "SR" => "Stormhaven Republic",
            "SoBF" or "So" => "Solomani Confederation, Bootean Federation",
            "SoCf" or "So" => "Solomani Confederation",
            "SoCT" or "So" => "Solomani Confederation, Consolidation of Turin",
            "SoFr" or "Fr" => "Solomani Confederation, Third Reformed French Confederate Republic",
            "SoHn" or "Hn" => "Solomani Confederation, Hanuman Systems",
            "SoKE" or "So" => "Solomani Confederation, Kruse Enclave",
            "SoKv" or "Kv" => "Solomani Confederation, Kostov Confederate Republic",
            "SoLE" or "So" => "Solomani Confederation, Lubbock Enclave",
            "SoNS" or "So" => "Solomani Confederation, New Slavic Solidarity",
            "SoQu" or "Qu" => "Solomani Confederation, Grand United States of Quesada",
            "SoRD" or "So" => "Solomani Confederation, Reformed Dootchen Estates",
            "SoRz" or "So" => "Solomani Confederation, Restricted Zone",
            "Sovr" or "Sv" => "The Sovereignty",
            "SoWu" or "So" => "Solomani Confederation, Wuan Technology Association",
            "SoXE" or "So" => "Solomani Confederation, Xuanzang Enclave",
            "StCl" or "Sc" => "Strend Cluster",
            "StIm" or "St" => "Strephon's Worlds",
            "SwCf" or "Sw" => "Sword Worlds Confederation",
            "SwFW" or "Sw" => "Swanfei Free Worlds",
            "SyRe" or "Sy" => "Syzlin Republic",
            "TeCl" or "Tc" => "Tellerian Cluster",
            "TrBr" or "Tb" => "Trita Brotherhood",
            "TrCo" or "Tr" => "Trindel Confederacy",
            "TrDo" or "Td" => "Trelyn Domain",
            "TroC" or "Tr" => "Trooles Confederation",
            "UnGa" or "Ug" => "Union of Garth",
            "UnHa" or "Uh" => "Union of Harmony",
            "V17D" or "V7" => "17th Disjucture",
            "V40S" or "Ve" => "40th Squadron",
            "VA16" or "V6" => "Assemblage of 1116",
            "VAkh" or "VA" => "Akhstuti",
            "VAnP" or "Vx" => "Antares Pact",
            "VARC" or "Vr" => "Anti-Rukh Coalition",
            "VAsP" or "Vx" => "Ascendancy Pact",
            "VAug" or "Vu" => "United Followers of Augurgh",
            "VBkA" or "Vb" => "Bakne Alliance",
            "VCKd" or "Vk" => "Commonality of Kedzudh",
            "VDeG" or "Vd" => "Democracy of Greats",
            "VDrN" or "VN" => "Drr'lana Network",
            "VDzF" or "Vf" => "Dzarrgh Federate",
            "VFFD" or "V1" => "First Fleet of Dzo",
            "VGoT" or "Vg" => "Glory of Taarskoerzn",
            "ViCo" or "Vi" => "Viyard Concourse",
            "VInL" or "V9" => "Infinity League",
            "VIrM" or "Vh" => "Irrgh Manifest",
            "VJoF" or "Vj" => "Jihad of Faarzgaen",
            "VKfu" or "Vk" => "Kfue",
            "VLIn" or "Vi" => "Llaeghskath Interacterate",
            "VLPr" or "Vl" => "Lair Protectorate",
            "VNgC" or "Vn" => "Ngath Confederation",
            "VNoe" or "VN" => "Noefa",
            "VOpA" or "Vo" => "Opposition Alliance",
            "VOpp" or "Vo" => "Opposition Alliance",
            "VOuz" or "VO" => "Ouzvothon",
            "VPGa" or "Vg" => "Pact of Gaerr",
            "VRo5" or "V5" => "Ruler of Five",
            "VRrS" or "VW" => "Rranglloez Stronghold",
            "VRuk" or "Vn" => "Worlds of Leader Rukh",
            "VSDp" or "Vs" => "Saeknouth Dependency",
            "VSEq" or "Vd" => "Society of Equals",
            "VThE" or "Vt" => "Thoengling Empire",
            "VTrA" or "VT" => "Trae Aggregation",
            "VTzE" or "Vp" => "Thirz Empire",
            "VUru" or "Vu" => "Urukhu",
            "VVar" or "Ve" => "Empire of Varroerth",
            "VVoS" or "Vv" => "Voekhaeb Society",
            "VWan" or "Vw" => "People of Wanz",
            "VWP2" or "V2" => "Windhorn Pact of Two",
            "VYoe" or "VQ" => "Union of Yoetyqq",
            "WiDe" or "Wd" => "Winston Democracy",
            "Wild" or "Wi" => "Wilds",
            "XXXX" or "Xx" => "Unknown",
            "ZePr" or "Zp" => "Zelphic Primacy",
            "ZhAx" or "Ax" => "Zhodani Consulate, Addaxur Reserve",
            "ZhCa" or "Ca" => "Zhodani Consulate, Colonnade Province",
            "ZhCh" or "Zh" => "Zhodani Consulate, Chtierabl Province",
            "ZhCo" or "Zh" => "Zhodani Consulate",
            "ZhIa" or "Zh" => "Zhodani Consulate, Iabrensh Province",
            "ZhIN" or "Zh" => "Zhodani Consulate, Iadr Nsobl Province",
            "ZhJp" or "Zh" => "Zhodani Consulate, Jadlapriants Province",
            "ZhMe" or "Zh" => "Zhodani Consulate, Meqlemianz Province",
            "ZhOb" or "Zh" => "Zhodani Consulate, Obrefripl Province",
            "ZhSh" or "Zh" => "Zhodani Consulate, Shtochiadr Province",
            "ZhVQ" or "Zh" => "Zhodani Consulate, Vlanchiets Qlom Province",
            "ZiSi" or "Rv" => "Restored Vilani Imperium",
            "Zuug" or "Zu" => "Zuugabish Tripartite",
            "ZyCo" or "Zc" => "Zydarian Codominium",
            _ => null
        };
    }

    public static string Atmosphere(EHex atmosphereCode)
    {
        return (atmosphereCode.ToChar()) switch
        {
            '0' => "No atmosphere",
            '1' => "Trace",
            '2' => "Very thin. Tainted",
            '3' => "Very thin",
            '4' => "Thin. Tainted",
            '5' => "Thin. Breathable",
            '6' => "Standard. Breathable",
            '7' => "Standard. Tainted",
            '8' => "Dense. Breathable",
            '9' => "Dense. Tainted",
            'A' => "Exotic",
            'B' => "Corrosive",
            'C' => "Insidious",
            'D' => "Dense, high",
            'E' => "Ellipsoid",
            'F' => "Thin, low",
            _ => "",
        };
    }

    public static string AtmosphereDescription(EHex atmosphereCode)
    {
        return (atmosphereCode.ToChar()) switch
        {
            '0' => "No atmosphere. Requires vacc suit",
            '1' => "Trace. Requires vacc suit",
            '2' => "Very thin. Tainted. Requires combination respirator/filter",
            '3' => "Very thin. Requires respirator",
            '4' => "Thin. Tainted. Requires filter mask",
            '5' => "Thin. Breathable",
            '6' => "Standard. Breathable",
            '7' => "Standard. Tainted. Requires filter mask",
            '8' => "Dense. Breathable",
            '9' => "Dense. Tainted. Requires filter mask",
            'A' => "Exotic. Requires special protective equipment",
            'B' => "Corrosive. Requires protective suit",
            'C' => "Insidious. Requires protective suit",
            'D' => "Dense, high. Breathable above a minimum altitude",
            'E' => "Ellipsoid. Breathable at certain latitudes",
            'F' => "Thin, low. Breathable below certain altitudes",
            _ => "",
        };
    }

    public static string BlackMarketCategory(int category)
    {
        return category switch
        {
            1 => "Unrestricted",
            2 => "Civillian",
            3 => "Paramilitary",
            4 => "Military",
            5 => "Restricted Military",
            6 => "Prohibited",
            _ => ""
        };
    }

    public static string BlackMarketCategoryDescription(int category)
    {
        return category switch
        {
            1 => "Allowed for anyone except possibly felons.",
            2 => "Allowed with a permit or demonstrated need. Not allowed for felons.",
            3 => "Allowed for police and other similar paramilitaries.",
            4 => "Allowed for military and licensed mercenaries.",
            5 => "Allowed for military and licensed mercenaries under special permit.",
            6 => "Not allowed for anyone.",
            _ => ""
        };
    }

    public static int DMCalc(int? value) => value == null ? 0 : DMCalc(value.Value);

    public static int DMCalc(int value)
    {
        return value switch
        {
            <= 0 => -3,
            <= 2 => -2,
            <= 5 => -1,
            <= 8 => 0,
            <= 11 => 1,
            <= 14 => 2,
            <= 17 => 3,
            <= 20 => 4,
            <= 23 => 5,
            <= 26 => 6,
            <= 29 => 7,
            <= 32 => 8,
            <= 35 => 9,
            <= 38 => 10,
            _ => 11,
        };
    }

    public static int DMCalc(EHex characteristic) => DMCalc(characteristic.Value);

    public static string Efficiency(string? efficiencyCode)
    {
        return efficiencyCode switch
        {
            "-5" => "Extremely poor",
            "-4" => "Very poor",
            "-3" => "Poor",
            "-2" => "Fair",
            "-1" => "Average",
            "0" => "Average",
            "+1" => "Average",
            "+2" => "Good",
            "+3" => "Improved",
            "+4" => "Advanced",
            "+5" => "Very advanced",
            _ => "Unknown"
        };
    }

    public static List<string> GovernmentContraband(EHex governmentCode)
    {
        return governmentCode.ToChar() switch
        {
            '0' => new List<string>(),
            '1' => new List<string>() { "Weapons", "Drugs", "Technology", "Travellers", "Psionics" },
            '2' => new List<string>() { "Drugs" },
            '3' => new List<string>() { "Weapons", "Technology", "Travellers" },
            '4' => new List<string>() { "Weapons", "Drugs", "Psionics" },
            '5' => new List<string>() { "Weapons", "Information", "Technology" },
            '6' => new List<string>() { "Weapons", "Technology", "Travellers" },
            '7' => new List<string>() { "Varies" },
            '8' => new List<string>() { "Weapons", "Drugs" },
            '9' => new List<string>() { "Weapons", "Drugs", "Information", "Technology", "Travellers", "Psionics" },
            'A' => new List<string>(),
            'B' => new List<string>() { "Weapons", "Information", "Technology" },
            'C' => new List<string>() { "Weapons" },
            'D' => new List<string>() { "Varies" },
            'E' => new List<string>() { "Varies" },
            'F' => new List<string>() { "Varies" },
            _ => new List<string>() { },
        };
    }

    public static string GovernmentDescription(EHex governmentCode)
    {
        return (governmentCode.ToChar()) switch
        {
            '0' => "No Government Structure. In many cases, tribal, clan or family bonds predominate.",
            '1' => "Company/Corporation. Government by a company managerial elite, citizens are company employees.",
            '2' => "Participating Democracy. Government by advice and consent of the citizen.",
            '3' => "Self-Perpetuating Oligarchy. Government by a restricted minority, with little or no input from the masses.",
            '4' => "Representative Democracy. Government by elected representatives.",
            '5' => "Feudal Technocracy. Government by specific individuals for those who agree to be ruled. Relationships are based on the performance of technical activities which are mutually-beneficial.",
            '6' => "Captive Government/Colony. Government by a leadership answerable to an outside group, a colony or conquered area.",
            '7' => "Balkanization. No central ruling authority exists. Rival governments compete for control.",
            '8' => "Civil Service Bureaucracy. Government by agencies employing individuals selected for their expertise.",
            '9' => "Impersonal Bureaucracy. Government by agencies which are insulated from the governed.",
            'A' => "Charismatic Dictator. Government by a single leader enjoying the confidence of the citizens.",
            'B' => "Non-Charismatic Dictator. A previous charismatic dictator has been replaced by a leader through normal channels.",
            'C' => "Charismatic Oligarchy. Government by a select group, organization, or class enjoying overwhelming confidence of the citizenry.",
            'D' => "Religious Dictatorship. Government by a religious minority which has little regard for the needs of the citizenry.",
            'E' => "Religious Autocracy. Government by a single religious leader having absolute power over the citizenry.",
            'F' => "Totalitarian Oligarchy. Government by an all-powerful minority which maintains absolute control through widespread coercion and oppression.",
            'G' => "Small Station or Facility (Aslan).",
            'H' => "Split Clan Control (Aslan).",
            'J' => "Single On-world Clan Control (Aslan).",
            'K' => "Single Multi-world Clan Control (Aslan).",
            'L' => "Major Clan Control (Aslan).",
            'M' => "Vassal Clan Control (Aslan).",
            'N' => "Major Vassal Clan Control (Aslan).",
            'P' => "Small Station or Facility (K'kree).",
            'Q' => "Krurruna or Krumanak Rule for Off-world Steppelord (K'kree).",
            'R' => "Steppelord On-world Rule (K'kree).",
            'S' => "Sept (Hiver).",
            'T' => "Unsupervised Anarchy (Hiver).",
            'U' => "Supervised Anarchy (Hiver).",
            'W' => "Committee (Hiver).",
            'X' => "Droyne Hierarchy (Droyne).",
            _ => "",
        };
    }

    public static string GovernmentDescriptionWithContraband(EHex governmentCode)
    {
        var contraband = GovernmentContraband(governmentCode);
        if (contraband.Count > 0)
            return GovernmentDescription(governmentCode) + " Restricts: " + string.Join(", ", contraband);
        else
            return GovernmentDescription(governmentCode);
    }

    public static string GovernmentType(EHex governmentCode)
    {
        return (governmentCode.ToChar()) switch
        {
            '0' => "No Government Structure",
            '1' => "Company/Corporation",
            '2' => "Participating Democracy",
            '3' => "Self-Perpetuating Oligarchy",
            '4' => "Representative Democracy",
            '5' => "Feudal Technocracy",
            '6' => "Captive Government/Colony",
            '7' => "Balkanization",
            '8' => "Civil Service Bureaucracy",
            '9' => "Impersonal Bureaucracy",
            'A' => "Charismatic Dictator",
            'B' => "Non-Charismatic Dictator",
            'C' => "Charismatic Oligarchy",
            'D' => "Religious Dictatorship",
            'E' => "Religious Autocracy",
            'F' => "Totalitarian Oligarchy",
            'G' => "Small Station or Facility (Aslan)",
            'H' => "Split Clan Control (Aslan)",
            'J' => "Single On-world Clan Control (Aslan)",
            'K' => "Single Multi-world Clan Control (Aslan)",
            'L' => "Major Clan Control (Aslan)",
            'M' => "Vassal Clan Control (Aslan)",
            'N' => "Major Vassal Clan Control (Aslan)",
            'P' => "Small Station or Facility (K'kree)",
            'Q' => "Krurruna or Krumanak Rule for Off-world Steppelord (K'kree)",
            'R' => "Steppelord On-world Rule (K'kree)",
            'S' => "Sept (Hiver)",
            'T' => "Unsupervised Anarchy (Hiver)",
            'U' => "Supervised Anarchy (Hiver)",
            'W' => "Committee (Hiver)",
            'X' => "Droyne Hierarchy (Droyne)",
            _ => "",
        };
    }

    public static string Gravity(EHex sizeCode)
    {
        return (sizeCode.ToChar()) switch
        {
            '0' => "Microgravity (0.01 G or less)",
            '1' => "Very Low Gravity (0.05g - 0.09g)",
            '2' => "Low Gravity (0.10g - 0.17g)",
            '3' => "Low Gravity (0.24g - 0.34g)",
            '4' => "Low Gravity (0.32g - 0.46g)",
            '5' => "Standard Gravity (0.40g - 0.57g)",
            '6' => "Standard Gravity (0.60g - 0.81g)",
            '7' => "Standard Gravity (0.70g - 0.94g)",
            '8' => "Standard Gravity (0.80g - 1.08g)",
            '9' => "Standard Gravity (1.03g - 1.33g)",
            'A' => "Standard Gravity (1.14g - 1.48g)",
            'B' => "High Gravity (1.49g - 1.89g)",
            'C' => "High Gravity (1.9g - 2.0g)",
            _ => "",
        };
    }

    public static string Heterogeneity(EHex heterogeneityCode)
    {
        return (heterogeneityCode.ToChar()) switch
        {
            '0' => "N/A",
            '1' => "Monolithic",
            '2' => "Monolithic",
            '3' => "Monolithic",
            '4' => "Harmonious",
            '5' => "Harmonious",
            '6' => "Harmonious",
            '7' => "Discordant",
            '8' => "Discordant",
            '9' => "Discordant",
            'A' => "Discordant",
            'B' => "Discordant",
            'C' => "Fragmented",
            'D' => "Fragmented",
            'E' => "Fragmented",
            'F' => "Fragmented",
            'G' => "Fragmented",
            _ => "Unknown"
        };
    }

    public static string Hydrographics(EHex hydrographicsCode)
    {
        return (hydrographicsCode.ToChar()) switch
        {
            '0' => "No water",
            '1' => "10% water",
            '2' => "20% water",
            '3' => "30% water",
            '4' => "40% water",
            '5' => "50% water",
            '6' => "60% water",
            '7' => "70% water",
            '8' => "80% water",
            '9' => "90% water",
            'A' => "100% water",
            _ => "",
        };
    }

    public static string HydrographicsDescription(EHex hydrographicsCode)
    {
        return (hydrographicsCode.ToChar()) switch
        {
            '0' => "No water. Desert World",
            '1' => "10% water",
            '2' => "20% water",
            '3' => "30% water",
            '4' => "40% water",
            '5' => "50% water",
            '6' => "60% water",
            '7' => "70% water. Equivalent to Terra or Vland",
            '8' => "80% water",
            '9' => "90% water",
            'A' => "100% water. Water World",
            _ => "",
        };
    }

    public static string Importance(string? importanceCode)
    {
        return importanceCode switch
        {
            "-3" => "Very unimportant",
            "-2" => "Very unimportant",
            "-1" => "Unimportant",
            "0" => "Unimportant",
            "1" => "Ordinary",
            "2" => "Ordinary",
            "3" => "Ordinary",
            "4" => "Important",
            "5" => "Very important",
            _ => "Unknown"
        };
    }

    public static string Infrastructure(EHex infrastructureCode)
    {
        return infrastructureCode.ToChar() switch
        {
            '0' => "Non-existent",
            '1' => "Extremely limited",
            '2' => "Extremely limited",
            '3' => "Very limited",
            '4' => "Very limited",
            '5' => "Limited",
            '6' => "Limited",
            '7' => "Generally available",
            '8' => "Generally available",
            '9' => "Extensive",
            'A' => "Extensive",
            'B' => "Very extensive",
            'C' => "Very extensive",
            'D' => "Comprehensive",
            'E' => "Comprehensive",
            'F' => "Very comprehensive",
            'G' => "Very comprehensive",
            'H' => "Very comprehensive",
            _ => "Unknown"
        };
    }

    public static string Labor(EHex laborCode)
    {
        return laborCode.ToChar() switch
        {
            '0' => "Unpopulated",
            '1' => "Tens",
            '2' => "Hundreds",
            '3' => "Thousands",
            '4' => "Tens of thousands",
            '5' => "Hundreds of thousands",
            '6' => "Millions",
            '7' => "Tens of millions",
            '8' => "Hundreds of millions",
            '9' => "Billions",
            'A' => "Tens of billions",
            'B' => "Hundreds of billions",
            'C' => "Trillions",
            'D' => "Tens of trillions",
            'E' => "Hundreds of tillions",
            'F' => "Quadrillions",
            'X' => "Unknown",
            _ => "Unknown"
        };
    }

    public static string LawLevel(EHex lawCode)
    {
        return (lawCode.ToChar()) switch
        {
            '0' => "No Law",
            '1' or '2' or '3' => "Low Law",
            '4' or '5' or '6' or '7' => "Moderate Law",
            '8' or '9' => "High Law",
            'A' or 'B' or 'C' or 'D' or 'E' or 'F' or 'G' or 'H' or 'J' or 'K' or 'L' => "Extreme Law",
            'S' => "Special/Variable situation",
            _ => "",
        };
    }

    public static string LawLevelDescription(EHex lawCode)
    {
        return (lawCode.ToChar()) switch
        {
            '0' => "No prohibitions.",
            '1' => "Body pistols, explosives, and poison gas prohibited.",
            '2' => "Portable energy weapons prohibited.",
            '3' => "Machine guns, automatic rifles prohibited.",
            '4' => "Light assault weapons prohibited.",
            '5' => "Personal concealable weapons prohibited.",
            '6' => "All firearms except shotguns prohibited.",
            '7' => "Shotguns prohibited.",
            '8' => "Long bladed weapons controlled; open possession prohibited.",
            '9' => "Possession of weapons outside the home prohibited.",
            'A' => "Weapon possession prohibited.",
            'B' => "Rigid control of civilian movement.",
            'C' => "Unrestricted invasion of privacy.",
            'D' => "Paramilitary law enforcement.",
            'E' => "Full-fledged police state.",
            'F' => "All facets of daily life regularly legislated and controlled.",
            'G' => "Severe punishment for petty infractions.",
            'H' => "Legalized oppressive practices.",
            'J' => "Routinely oppressive and restrictive.",
            'K' => "Excessively oppressive and restrictive.",
            'L' => "Totally oppressive and restrictive.",
            'S' => "Special/Variable situation.",
            _ => "",
        };
    }

    public static string LawLevelDrugsRestricted(EHex lawCode)
    {
        return (lawCode.ToChar()) switch
        {
            '0' => "",
            '1' => "Highly addictive and dangerous narcotics.",
            '2' => "Highly addictive narcotics.",
            '3' => "Combat drugs.",
            '4' => "Addictive narcotics.",
            '5' => "Anagathics.",
            '6' => "Fast and Slow drugs.",
            '7' => "All narcotics.",
            '8' => "Medicinal drugs.",
            _ => "All drugs.",
        };
    }

    public static IEnumerable<string> LawLevelDrugsRestrictedList(EHex lawCode)
    {
        switch (lawCode.Value)
        {
            case 1:
            case 2:
                yield return LawLevelDrugsRestricted(lawCode);
                break;

            case 3:
                yield return LawLevelDrugsRestricted(3);
                yield return LawLevelDrugsRestricted(2);
                break;

            case 4:
                yield return LawLevelDrugsRestricted(3);
                yield return LawLevelDrugsRestricted(4);
                break;

            case 5:
                yield return LawLevelDrugsRestricted(3);
                yield return LawLevelDrugsRestricted(4);
                yield return LawLevelDrugsRestricted(5);
                break;

            case 6:
                yield return LawLevelDrugsRestricted(3);
                yield return LawLevelDrugsRestricted(4);
                yield return LawLevelDrugsRestricted(5);
                yield return LawLevelDrugsRestricted(6);
                break;

            case 7:
                yield return LawLevelDrugsRestricted(3);
                yield return LawLevelDrugsRestricted(5);
                yield return LawLevelDrugsRestricted(6);
                yield return LawLevelDrugsRestricted(7);
                break;

            case 8:
                yield return LawLevelDrugsRestricted(3);
                yield return LawLevelDrugsRestricted(5);
                yield return LawLevelDrugsRestricted(6);
                yield return LawLevelDrugsRestricted(7);
                yield return LawLevelDrugsRestricted(8);
                break;

            case > 8:
                yield return LawLevelDrugsRestricted(lawCode);
                break;
        }
    }

    public static string LawLevelInformationRestricted(EHex lawCode)
    {
        return (lawCode.ToChar()) switch
        {
            '0' => "",
            '1' => "Intellect programs.",
            '2' => "Agent programs.",
            '3' => "Intrusion programs.",
            '4' => "Security programs.",
            '5' => "Expert programs.",
            '6' => "Recent news from offworld.",
            '7' => "Library programs, unfiltered data about other worlds. Free speech curtailed.",
            '8' => "Information technology, any non-critical data from offworld, personal media.",
            _ => "Any data from offworld. No free press.",
        };
    }

    public static IEnumerable<string> LawLevelInformationRestrictedList(EHex lawCode)
    {
        if (lawCode.Value >= 1)
        {
            for (var i = 1; i <= Math.Min(9, lawCode.Value); i++)
                yield return LawLevelInformationRestricted(i);
        }
    }

    public static string LawLevelPsionicsRestricted(EHex lawCode)
    {
        return (lawCode.ToChar()) switch
        {
            '0' => "",
            '1' => "Dangerous talents must be registered.",
            '2' => "All psionic powers must be registered; use of dangerous powers forbidden.",
            '3' => "Use of telepathy restricted to government approved telepaths.",
            '4' => "Use of teleportation and clairvoyance restricted.",
            '5' => "Use of all psionic, powers restricted to government psionicists.",
            '6' => "Possession of psionic drugs banned.",
            '7' => "Use of psionics forbiden.",
            '8' => "Psionic-related technology banned.",
            _ => "All psionics.",
        };
    }

    public static IEnumerable<string> LawLevelPsionicsRestrictedList(EHex lawCode)
    {
        switch (lawCode.Value)
        {
            case 1:
            case 2:
            case 5:
                yield return LawLevelPsionicsRestricted(lawCode);
                break;

            case 3:
                yield return LawLevelPsionicsRestricted(2);
                yield return LawLevelPsionicsRestricted(3);
                break;

            case 4:
                yield return LawLevelPsionicsRestricted(2);
                yield return LawLevelPsionicsRestricted(3);
                yield return LawLevelPsionicsRestricted(4);
                break;

            case 6:
                yield return LawLevelPsionicsRestricted(5);
                yield return LawLevelPsionicsRestricted(6);
                break;

            case 7:
                yield return LawLevelPsionicsRestricted(6);
                yield return LawLevelPsionicsRestricted(7);
                break;

            case 8:
                yield return LawLevelPsionicsRestricted(6);
                yield return LawLevelPsionicsRestricted(7);
                yield return LawLevelPsionicsRestricted(8);
                break;

            case > 8:
                yield return LawLevelPsionicsRestricted(lawCode);
                break;
        }
    }

    public static string LawLevelTechnolgyRestricted(EHex lawCode)
    {
        return (lawCode.ToChar()) switch
        {
            '0' => "",
            '1' => "Dangerous technologies such as nanotechnology",
            '2' => "Alien Technology",
            '3' => "TL 15+",
            '4' => "TL 13+",
            '5' => "TL 11+",
            '6' => "TL 9+",
            '7' => "TL 7+",
            '8' => "TL 5+",
            _ => "TL 3+",
        };
    }

    public static IEnumerable<string> LawLevelTechnolgyRestrictedList(EHex lawCode)
    {
        if (lawCode.Value >= 1)
            yield return LawLevelTechnolgyRestricted(1);

        if (lawCode.Value >= 2)
            yield return LawLevelTechnolgyRestricted(2);

        if (lawCode.Value >= 3)
            yield return LawLevelTechnolgyRestricted(lawCode);
    }

    public static string LawLevelTravellersRestricted(EHex lawCode)
    {
        return (lawCode.ToChar()) switch
        {
            '0' => "",
            '1' => "Visitors must contact planetary authorities by radio, landing is permitted anywhere.",
            '2' => "Visitors must report passenger manifest, landing is permitted anywhere.",
            '3' => "Landing only at starport or other authorised sites.",
            '4' => "Landing only at starport.",
            '5' => "Citizens must register offworld travel; visitors mustt register all business.",
            '6' => "Visits discouraged; excessive contact with citizens forbidden.",
            '7' => "Citizens may not leave planet; ,visitors may not leave starport. ",
            '8' => "Landing permitted only to imperial agents.",
            _ => "No offworlders permitted.",
        };
    }

    public static string LawLevelWeaponsRestricted(EHex lawCode)
    {
        return (lawCode.ToChar()) switch
        {
            '0' => "",
            '1' => "Poison gas, explosives, undetectable weapons, WMD.",
            '2' => "Portable energy weapons (except ship-mounted weapons).",
            '3' => "Heavy weapons.",
            '4' => "Light assault weapons and submachine guns.",
            '5' => "Personal concealable weapons.",
            '6' => "All firearms except shotguns and stunners; carrying weapons discouraged.",
            '7' => "All firearms except stunners; carrying weapons discouraged.",
            '8' => "All firearms and bladed weapons.",
            _ => "Any weapons",
        };
    }

    public static IEnumerable<string> LawLevelWeaponsRestrictedList(EHex lawCode)
    {
        switch (lawCode.Value)
        {
            case >= 6:
                yield return LawLevelWeaponsRestricted(lawCode);
                break;

            case >= 0:
                for (var i = 1; i <= lawCode.Value; i++)
                    yield return LawLevelWeaponsRestricted(i);
                break;
        }
    }

    public static MilitaryBase MilitaryBase(char code)
    {
        return code switch
        {
            'C' => new MilitaryBase(code, "Corsair Base", "Vargr"),
            'D' => new MilitaryBase(code, "Naval Depot", null),
            'E' => new MilitaryBase(code, "Embassy", "Hiver"),
            'K' => new MilitaryBase(code, "Naval Base", null),
            'M' => new MilitaryBase(code, "Military Base", null),
            'N' => new MilitaryBase(code, "Naval Base", "Imperial"),
            'R' => new MilitaryBase(code, "Clan Base", "Aslan"),
            'S' => new MilitaryBase(code, "Scout Base", "Imperial"),
            'T' => new MilitaryBase(code, "Tlaukhu Base", "Aslan"),
            'V' => new MilitaryBase(code, "Exploration Base", null),
            'W' => new MilitaryBase(code, "Way Station", null),
            'A' => new MilitaryBase(code, "Naval Base and Scout Base", "Imperial"),
            'B' => new MilitaryBase(code, "Naval Base and Way Station", "Imperial"),
            //'C' => new MilitaryBase(code, "Corsair Base", "Vargr"),
            //'D' => new MilitaryBase(code, "Depot", "Imperial"),
            //'E' => new MilitaryBase(code, "Embassy Center", "Hiver"),
            //'F' => new MilitaryBase(code, "Military and Naval Base", ""),
            'G' => new MilitaryBase(code, "Naval Base", "Vargr"),
            'H' => new MilitaryBase(code, "Naval Base and Corsair Base", "Vargr"),
            'J' => new MilitaryBase(code, "Naval Base", null),
            //'K' => new MilitaryBase(code, "Naval Base", "K'kre"),
            'L' => new MilitaryBase(code, "Naval Base", "Hiver"),
            //'M' => new MilitaryBase(code, "Military Base", ""),
            //'N' => new MilitaryBase(code, "Naval Base", "Imperial"),
            'O' => new MilitaryBase(code, "Naval Outpost", "K'kre"),
            'P' => new MilitaryBase(code, "Naval Base", "Droyne"),
            'Q' => new MilitaryBase(code, "Military Garrison", "Droyne"),
            //'R' => new MilitaryBase(code, "Clan Base", "Aslan"),
            //'S' => new MilitaryBase(code, "Scout Base", "Imperial"),
            //'T' => new MilitaryBase(code, "Tlauku Base", "Aslan"),
            'U' => new MilitaryBase(code, "Tlauku and Clan Base", "Aslan"),
            //'V' => new MilitaryBase(code, "Scout/Exploration Base", null),
            //'W' => new MilitaryBase(code, "Way Station", "Imperial"),
            'X' => new MilitaryBase(code, "Relay Station", "Zhodani"),
            'Y' => new MilitaryBase(code, "Depot", "Zhodani"),
            'Z' => new MilitaryBase(code, "Naval/Military Base", "Zhodani"),
            _ => new MilitaryBase(code, "<Unknown>", null)
        };
    }

    public static int OddsOfSuccess(int dm, int target)
    {
        return OddsOfSuccess(target - dm);
    }

    public static int OddsOfSuccess(int target)
    {
        if (target <= 2) return 100;
        if (target == 3) return 97;
        if (target == 4) return 92;
        if (target == 5) return 83;
        if (target == 6) return 72;
        if (target == 7) return 58;
        if (target == 8) return 42;
        if (target == 9) return 28;
        if (target == 10) return 17;
        if (target == 11) return 8;
        if (target == 12) return 3;
        return 0;
    }

    public static double PopulationExponent(EHex populationCode) => Math.Pow(10, populationCode.Value);

    public static string PortEnforcement(int level)
    {
        return (level) switch
        {
            <= 0 => "Lawless and violent",
            <= 2 => "Lawless and harmonious",
            <= 4 => "Minimal security force",
            <= 6 => "Small security force",
            <= 8 => "Average security force",
            <= 10 => "Well-equipped security force",
            <= 12 => "Paramilitary security service",
            <= 14 => "Military security service",
            _ => "Private army",
        };
    }

    public static string PortEnforcementDetails(int level)
    {
        return (level) switch
        {
            <= 0 => "Essentially a violent anarchy, with gangs, private guards, or security selfishly protecting influential individuals’ property.",
            <= 2 => "No formal law enforcement but armed individuals cooperate against threats on a common-interest basis.",
            <= 4 => "A sheriff and deputies, armed with civilian firearms, possibly part-time.",
            <= 6 => "Small professional security force with civilian weapons.",
            <= 8 => "Adequate security force with paramilitary equipment including automatic weapons.",
            <= 10 => "Adequate security force with paramilitary equipment including support weapons.",
            <= 12 => "Large professional security force. Some personnel have access to heavy military weapons.",
            <= 14 => "Large professional security service organised along military lines, with heavy weapons. Possibly a mercenary formation.",
            _ => "Excessive military-style security service with armed vehicles and support platforms",
        };
    }

    public static string Resources(EHex resourcesCode)
    {
        return resourcesCode.ToChar() switch
        {
            '2' => "Very scarce",
            '3' => "Very scarce",
            '4' => "Scarce",
            '5' => "Scarce",
            '6' => "Few",
            '7' => "Few",
            '8' => "Moderate",
            '9' => "Moderate",
            'A' => "Abundant",
            'B' => "Abundant",
            'C' => "Very abundant",
            'D' => "Very abundant",
            'E' => "Extremely abundant",
            'F' => "Extremely abundant",
            'G' => "Extremely abundant",
            'H' => "Extremely abundant",
            'J' => "Extremely abundant",
            _ => "Unknown"
        };
    }

    public static EHex? RestrictedTechLevel(EHex lawCode)
    {
        return (lawCode.ToChar()) switch
        {
            '0' => null,
            '1' => null,
            '2' => null,
            '3' => 15,
            '4' => 13,
            '5' => 11,
            '6' => 9,
            '7' => 7,
            '8' => 5,
            _ => 3,
        };
    }

    public static List<(string, string)> RestrictionsByGovernmentAndLaw(EHex governmentCode, EHex lawCode)
    {
        var result = new List<(string, string)>();
        if (lawCode == 0)
            return result; //no restrictions apply

        var contraband = GovernmentContraband(governmentCode);
        foreach (var item in contraband)
            switch (item)
            {
                case "Weapons":
                    foreach (var rule in LawLevelWeaponsRestrictedList(lawCode))
                        result.Add((item, rule));
                    break;

                case "Drugs":
                    foreach (var rule in LawLevelDrugsRestrictedList(lawCode))
                        result.Add((item, rule));
                    break;

                case "Travellers": result.Add((item, LawLevelTravellersRestricted(lawCode))); break;
                case "Technology":
                    foreach (var rule in LawLevelTechnolgyRestrictedList(lawCode))
                        result.Add((item, rule));
                    break;

                case "Psionics":
                    foreach (var rule in LawLevelPsionicsRestrictedList(lawCode))
                        result.Add((item, rule));
                    break;

                case "Information":
                    foreach (var rule in LawLevelInformationRestrictedList(lawCode))
                        result.Add((item, rule));
                    break;

                case "None": result.Add((item, "No restrictions.")); break;
                case "Varies":
                    result.Add(("Varies", "One of more of the following may apply."));
                    result.Add(("Weapons", LawLevelWeaponsRestricted(lawCode)));
                    result.Add(("Drugs", LawLevelDrugsRestricted(lawCode)));
                    result.Add(("Information", LawLevelInformationRestricted(lawCode)));
                    result.Add(("Technology", LawLevelTechnolgyRestricted(lawCode)));
                    result.Add(("Travellers", LawLevelTravellersRestricted(lawCode)));
                    result.Add(("Psionics", LawLevelPsionicsRestricted(lawCode)));
                    break;
            }
        return result;
    }

    public static int SizeKM(EHex sizeCode)
    {
        return (sizeCode.ToChar()) switch
        {
            '0' => 800,
            '1' => 1600,
            '2' => 3200,
            '3' => 4800,
            '4' => 6400,
            '5' => 8000,
            '6' => 9600,
            '7' => 11200,
            '8' => 12800,
            '9' => 14400,
            'A' => 16000,
            'B' => 17600,
            'C' => 19400,
            _ => 0,
        };
    }

    public static string Starport(EHex starportCode)
    {
        return (starportCode.ToChar()) switch
        {
            'A' => "Excellent Quality",
            'B' => "Good Quality",
            'C' => "Routine Quality",
            'D' => "Poor Quality",
            'E' => "Frontier Installation",
            'X' => "No Starport",
            'F' => "Good Quality",
            'G' => "Poor Quality",
            'H' => "Primitive Quality",
            'Y' => "None",
            _ => "",
        };
    }

    public static string StarportCargoStorageSecurity(EHex starportCode)
    {
        return starportCode.ToChar() switch
        {
            'A' => "Biometric scanner, drone, and permanent guard.",
            'B' or 'F' => "Biometric scanner, patrols every 15 minutes, CCTV.",
            'C' or 'G' => "Electric keypad, patrols every half-hour, CCTV.",
            'D' or 'H' => "Electric keypad, hourly patrols.",
            'E' => "Padlock, possibly a guard dog.",
            _ => "",
        };
    }

    public static string StarportDescription(EHex starportCode)
    {
        return starportCode.ToChar() switch
        {
            'A' => "Excellent Quality Starport. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing starships and non-starships present.",
            'B' => "Good Quality Starport. Refined fuel available. Annual maintenance overhaul available. Shipyard capable of constructing non-starships present.",
            'C' => "Routine Quality Starport. Only unrefined fuel available. Reasonable repair facilities present.",
            'D' => "Poor Quality Starport. Only unrefined fuel available. No repair facilities present.",
            'E' => "Frontier Installation. Essentially a marked spot of bedrock with no fuel or facilities present.",
            'X' => "No Starport. No provision is made for any ship landings",
            'F' => "Good Quality Spaceport. Minor damage repairable. Unrefined fuel available",
            'G' => "Poor Quality Spaceport. Superficial repairs possible. Unrefined fuel available",
            'H' => "Primitive Quality Spaceport. No repairs or fuel available",
            'J' => "Independent Frontier Installation. Essentially a marked spot of bedrock with no fuel or facilities present.",
            'Y' => "None",
            _ => "",
        };
    }

    public static string Strangeness(EHex strangenessCode)
    {
        return strangenessCode.ToChar() switch
        {
            '0' => "N/A",
            '1' => "Very typical",
            '2' => "Typical",
            '3' => "Somewhat typical",
            '4' => "Somewhat distinct",
            '5' => "Distinct",
            '6' => "Very distinct",
            '7' => "Confusing",
            '8' => "Very confusing",
            '9' => "Extremely confusing",
            'A' => "Incomprehensible",
            _ => "Unknown"
        };
    }

    public static string Symbols(EHex symbolsCode)
    {
        return symbolsCode.ToChar() switch
        {
            '0' => "Extremely concrete",
            '1' => "Extremely concrete",
            '2' => "Very concrete",
            '3' => "Very concrete",
            '4' => "Concrete",
            '5' => "Concrete",
            '6' => "Somewhat concrete",
            '7' => "Somewhat concrete",
            '8' => "Somewhat abstract",
            '9' => "Somewhat abstract",
            'A' => "Abstract",
            'B' => "Abstract",
            'C' => "Very abstract",
            'D' => "Very abstract",
            'E' => "Extremely abstract",
            'F' => "Extremely abstract",
            'G' => "Extremely abstract",
            'H' => "Incomprehensibly abstract",
            'J' => "Incomprehensibly abstract",
            'K' => "Incomprehensibly abstract",
            'L' => "Incomprehensibly abstract",
            _ => "Unknown"
        };
    }

    public static string TechLevel(EHex techCode)
    {
        return (techCode.ToChar()) switch
        {
            '0' => "Stone Age. Primitive",
            '1' => "Bronze, Iron. Bronze Age to Middle Ages",
            '2' => "Printing Press. circa 1400 to 1700",
            '3' => "Basic Science. circa 1700 to 1860",
            '4' => "External Combustion. circa 1860 to 1900",
            '5' => "Mass Production. circa 1900 to 1939",
            '6' => "Nuclear Power. circa 1940 to 1969",
            '7' => "Miniaturized Electronics. circa 1970 to 1979",
            '8' => "Quality Computers. circa 1980 to 1989",
            '9' => "Anti-Gravity. circa 1990 to 2000",
            'A' => "Interstellar community",
            'B' => "Lower Average Imperial",
            'C' => "Average Imperial",
            'D' => "Above Average Imperial",
            'E' => "Above Average Imperial",
            'F' => "Technical Imperial Maximum",
            'G' => "Robots",
            'H' => "Artificial Intelligence",
            'J' => "Personal Disintegrators",
            'K' => "Plastic Metals",
            'L' => "Comprehensible only as technological magic",
            _ => "",
        };
    }

    public static string TechLevelDescription(EHex techCode)
    {
        return (techCode.ToChar()) switch
        {
            '0' => "Primitive/Stone Age",
            '1' => "Bronze Age/Iron Age",
            '2' => "Age of Sail",
            '3' => "Industrial Revolution",
            '4' => "Mechanization",
            '5' => "Polymers",
            '6' => "Nuclear Age",
            '7' => "Semiconductors; Early space age",
            '8' => "Superconductors; Early communications",
            '9' => "Gravitics; First Jump Drives",
            'A' => "Practical Fusion power",
            'B' => "Fusion+; Imperial maximum year 0",
            'C' => "Sophisticated Robots",
            'D' => "Cloning; Imperial maximum year 550",
            'E' => "Geneering; Imperial maximum year 900",
            'F' => "Anagathics; Imperial maximum year 1105",
            'G' => "Artificial Persons; Black Globe Generators",
            'H' => "Hop Drive; Permanent Personality Transfer",
            'J' => "Disruptor and Stasis weapons",
            'K' => "Limited Matter Transport; Antimatter power",
            'L' => "Skip Drives; White Globe Generators",
            'M' => "System-wide matter transport; Relativity Rifle",
            'N' => "(Beyond current technology extrapolation.)",
            'P' => "Planetary core energy tap; Rapid terraforming",
            'Q' => "Portals; Rosettes",
            'R' => "Psionic engineering",
            'S' => "Stasis globe",
            'T' => "Ringworlds",
            'U' => "Reality drive",
            'V' => "Dyson spheres",
            'W' => "Pocket universes",
            _ => "",
        };
    }

    static ImmutableArray<EHex> BuildCodes(int min, int max, params EHex[] extras)
    {
        var temp = new List<EHex>();
        for (var i = min; i <= max; i++)
            temp.Add(i);
        temp.AddRange(extras);

        return ImmutableArray.CreateRange(temp);
    }

    static ImmutableArray<EHex> BuildCodes(EHex min, EHex max, params EHex[] extras)
    {
        return BuildCodes(min.Value, max.Value, extras);
    }

    static ImmutableArray<EHex> BuildCodes(char min, char max, params EHex[] extras)
    {
        return BuildCodes(new EHex(min).Value, new EHex(max).Value, extras);
    }
}
