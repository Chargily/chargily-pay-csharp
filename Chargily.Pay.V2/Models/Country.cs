using System.Text.Json.Serialization;

namespace Chargily.Pay.V2.Models;

/// <summary>
/// ISO 3166-1 alpha-2 country codes
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Country
{
    /// <summary>
    /// Afghanistan <b>(AF)</b>
    /// </summary>
    Afghanistan,

    /// <summary>
    /// Aland Islands <b>(AX)</b>
    /// </summary>
    AlandIslands,

    /// <summary>
    /// Albania <b>(AL)</b>
    /// </summary>
    Albania,

    /// <summary>
    /// Algeria <b>(DZ)</b>
    /// </summary>
    Algeria,

    /// <summary>
    /// American Samoa <b>(AS)</b>
    /// </summary>
    AmericanSamoa,

    /// <summary>
    /// Andorra <b>(AD)</b>
    /// </summary>
    Andorra,

    /// <summary>
    /// Angola <b>(AO)</b>
    /// </summary>
    Angola,

    /// <summary>
    /// Anguilla <b>(AI)</b>
    /// </summary>
    Anguilla,

    /// <summary>
    /// Antarctica <b>(AQ)</b>
    /// </summary>
    Antarctica,

    /// <summary>
    /// Antigua and Barbuda <b>(AG)</b>
    /// </summary>
    AntiguaAndBarbuda,

    /// <summary>
    /// Argentina <b>(AR)</b>
    /// </summary>
    Argentina,

    /// <summary>
    /// Armenia <b>(AM)</b>
    /// </summary>
    Armenia,

    /// <summary>
    /// Aruba <b>(AW)</b>
    /// </summary>
    Aruba,

    /// <summary>
    /// Australia <b>(AU)</b>
    /// </summary>
    Australia,

    /// <summary>
    /// Austria <b>(AT)</b>
    /// </summary>
    Austria,

    /// <summary>
    /// Azerbaijan <b>(AZ)</b>
    /// </summary>
    Azerbaijan,

    /// <summary>
    /// Bahamas <b>(BS)</b>
    /// </summary>
    Bahamas,

    /// <summary>
    /// Bahrain <b>(BH)</b>
    /// </summary>
    Bahrain,

    /// <summary>
    /// Bangladesh <b>(BD)</b>
    /// </summary>
    Bangladesh,

    /// <summary>
    /// Barbados <b>(BB)</b>
    /// </summary>
    Barbados,

    /// <summary>
    /// Belarus <b>(BY)</b>
    /// </summary>
    Belarus,

    /// <summary>
    /// Belgium <b>(BE)</b>
    /// </summary>
    Belgium,

    /// <summary>
    /// Belize <b>(BZ)</b>
    /// </summary>
    Belize,

    /// <summary>
    /// Benin <b>(BJ)</b>
    /// </summary>
    Benin,

    /// <summary>
    /// Bermuda <b>(BM)</b>
    /// </summary>
    Bermuda,

    /// <summary>
    /// Bhutan <b>(BT)</b>
    /// </summary>
    Bhutan,

    /// <summary>
    /// Plurinational State of Bolivia <b>(BO)</b>
    /// </summary>
    Bolivia,

    /// <summary>
    /// Sint Eustatius and Saba Bonaire <b>(BQ)</b>
    /// </summary>
    Bonaire,

    /// <summary>
    /// Bosnia and Herzegovina <b>(BA)</b>
    /// </summary>
    BosniaAndHerzegovina,

    /// <summary>
    /// Botswana <b>(BW)</b>
    /// </summary>
    Botswana,

    /// <summary>
    /// Bouvet Island <b>(BV)</b>
    /// </summary>
    BouvetIsland,

    /// <summary>
    /// Brazil <b>(BR)</b>
    /// </summary>
    Brazil,

    /// <summary>
    /// British Indian Ocean Territory <b>(IO)</b>
    /// </summary>
    BritishIndianOceanTerritory,

    /// <summary>
    /// Brunei Darussalam <b>(BN)</b>
    /// </summary>
    BruneiDarussalam,

    /// <summary>
    /// Bulgaria <b>(BG)</b>
    /// </summary>
    Bulgaria,

    /// <summary>
    /// Burkina Faso <b>(BF)</b>
    /// </summary>
    BurkinaFaso,

    /// <summary>
    /// Burundi <b>(BI)</b>
    /// </summary>
    Burundi,

    /// <summary>
    /// Cambodia <b>(KH)</b>
    /// </summary>
    Cambodia,

    /// <summary>
    /// Cameroon <b>(CM)</b>
    /// </summary>
    Cameroon,

    /// <summary>
    /// Canada <b>(CA)</b>
    /// </summary>
    Canada,

    /// <summary>
    /// Cape Verde <b>(CV)</b>
    /// </summary>
    CapeVerde,

    /// <summary>
    /// Cayman Islands <b>(KY)</b>
    /// </summary>
    CaymanIslands,

    /// <summary>
    /// Central African Republic <b>(CF)</b>
    /// </summary>
    CentralAfricanRepublic,

    /// <summary>
    /// Chad <b>(TD)</b>
    /// </summary>
    Chad,

    /// <summary>
    /// Chile <b>(CL)</b>
    /// </summary>
    Chile,

    /// <summary>
    /// China <b>(CN)</b>
    /// </summary>
    China,

    /// <summary>
    /// Christmas Island <b>(CX)</b>
    /// </summary>
    ChristmasIsland,

    /// <summary>
    /// Cocos (Keeling) Islands <b>(CC)</b>
    /// </summary>
    CocosKeelingIslands,

    /// <summary>
    /// Colombia <b>(CO)</b>
    /// </summary>
    Colombia,

    /// <summary>
    /// Comoros <b>(KM)</b>
    /// </summary>
    Comoros,

    /// <summary>
    /// Republic Of Congo <b>(CG)</b>
    /// </summary>
    RepublicOfCongo,

    /// <summary>
    /// Democratic Republic Of Congo <b>(CD)</b>
    /// </summary>
    DemocraticRepublicOfCongo,

    /// <summary>
    /// Cook Islands <b>(CK)</b>
    /// </summary>
    CookIslands,

    /// <summary>
    /// Costa Rica <b>(CR)</b>
    /// </summary>
    CostaRica,

    /// <summary>
    /// Cote d'Ivoire <b>(CI)</b>
    /// </summary>
    CotedIvoire,

    /// <summary>
    /// Croatia <b>(HR)</b>
    /// </summary>
    Croatia,

    /// <summary>
    /// Cuba <b>(CU)</b>
    /// </summary>
    Cuba,

    /// <summary>
    /// Curacao <b>(CW)</b>
    /// </summary>
    Curacao,

    /// <summary>
    /// Cyprus <b>(CY)</b>
    /// </summary>
    Cyprus,

    /// <summary>
    /// Czech Republic <b>(CZ)</b>
    /// </summary>
    CzechRepublic,

    /// <summary>
    /// Denmark <b>(DK)</b>
    /// </summary>
    Denmark,

    /// <summary>
    /// Djibouti <b>(DJ)</b>
    /// </summary>
    Djibouti,

    /// <summary>
    /// Dominica <b>(DM)</b>
    /// </summary>
    Dominica,

    /// <summary>
    /// Dominican Republic <b>(DO)</b>
    /// </summary>
    DominicanRepublic,

    /// <summary>
    /// Ecuador <b>(EC)</b>
    /// </summary>
    Ecuador,

    /// <summary>
    /// Egypt <b>(EG)</b>
    /// </summary>
    Egypt,

    /// <summary>
    /// El Salvador <b>(SV)</b>
    /// </summary>
    ElSalvador,

    /// <summary>
    /// Equatorial Guinea <b>(GQ)</b>
    /// </summary>
    EquatorialGuinea,

    /// <summary>
    /// Eritrea <b>(ER)</b>
    /// </summary>
    Eritrea,

    /// <summary>
    /// Estonia <b>(EE)</b>
    /// </summary>
    Estonia,

    /// <summary>
    /// Ethiopia <b>(ET)</b>
    /// </summary>
    Ethiopia,

    /// <summary>
    /// Falkland Islands (Malvinas) <b>(FK)</b>
    /// </summary>
    FalklandIslandsMalvinas,

    /// <summary>
    /// Faroe Islands <b>(FO)</b>
    /// </summary>
    FaroeIslands,

    /// <summary>
    /// Fiji <b>(FJ)</b>
    /// </summary>
    Fiji,

    /// <summary>
    /// Finland <b>(FI)</b>
    /// </summary>
    Finland,

    /// <summary>
    /// France <b>(FR)</b>
    /// </summary>
    France,

    /// <summary>
    /// French Guiana <b>(GF)</b>
    /// </summary>
    FrenchGuiana,

    /// <summary>
    /// French Polynesia <b>(PF)</b>
    /// </summary>
    FrenchPolynesia,

    /// <summary>
    /// French Southern Territories <b>(TF)</b>
    /// </summary>
    FrenchSouthernTerritories,

    /// <summary>
    /// Gabon <b>(GA)</b>
    /// </summary>
    Gabon,

    /// <summary>
    /// Gambia <b>(GM)</b>
    /// </summary>
    Gambia,

    /// <summary>
    /// Georgia <b>(GE)</b>
    /// </summary>
    Georgia,

    /// <summary>
    /// Germany <b>(DE)</b>
    /// </summary>
    Germany,

    /// <summary>
    /// Ghana <b>(GH)</b>
    /// </summary>
    Ghana,

    /// <summary>
    /// Gibraltar <b>(GI)</b>
    /// </summary>
    Gibraltar,

    /// <summary>
    /// Greece <b>(GR)</b>
    /// </summary>
    Greece,

    /// <summary>
    /// Greenland <b>(GL)</b>
    /// </summary>
    Greenland,

    /// <summary>
    /// Grenada <b>(GD)</b>
    /// </summary>
    Grenada,

    /// <summary>
    /// Guadeloupe <b>(GP)</b>
    /// </summary>
    Guadeloupe,

    /// <summary>
    /// Guam <b>(GU)</b>
    /// </summary>
    Guam,

    /// <summary>
    /// Guatemala <b>(GT)</b>
    /// </summary>
    Guatemala,

    /// <summary>
    /// Guernsey <b>(GG)</b>
    /// </summary>
    Guernsey,

    /// <summary>
    /// Guinea <b>(GN)</b>
    /// </summary>
    Guinea,

    /// <summary>
    /// Guinea-Bissau <b>(GW)</b>
    /// </summary>
    GuineaBissau,

    /// <summary>
    /// Guyana <b>(GY)</b>
    /// </summary>
    Guyana,

    /// <summary>
    /// Haiti <b>(HT)</b>
    /// </summary>
    Haiti,

    /// <summary>
    /// Heard Island and McDonald Islands <b>(HM)</b>
    /// </summary>
    HeardIslandAndMcDonaldIslands,

    /// <summary>
    /// Holy See (Vatican City State) <b>(VA)</b>
    /// </summary>
    HolySeeVaticanCityState,

    /// <summary>
    /// Honduras <b>(HN)</b>
    /// </summary>
    Honduras,

    /// <summary>
    /// Hong Kong <b>(HK)</b>
    /// </summary>
    HongKong,

    /// <summary>
    /// Hungary <b>(HU)</b>
    /// </summary>
    Hungary,

    /// <summary>
    /// Iceland <b>(IS)</b>
    /// </summary>
    Iceland,

    /// <summary>
    /// India <b>(IN)</b>
    /// </summary>
    India,

    /// <summary>
    /// Indonesia <b>(ID)</b>
    /// </summary>
    Indonesia,

    /// <summary>
    /// Islamic Republic of Iran <b>(IR)</b>
    /// </summary>
    Iran,

    /// <summary>
    /// Iraq <b>(IQ)</b>
    /// </summary>
    Iraq,

    /// <summary>
    /// Ireland <b>(IE)</b>
    /// </summary>
    Ireland,

    /// <summary>
    /// Isle of Man <b>(IM)</b>
    /// </summary>
    IsleofMan,

    /// <summary>
    /// Israel <b>(IL)</b>
    /// </summary>
    Israel,

    /// <summary>
    /// Italy <b>(IT)</b>
    /// </summary>
    Italy,

    /// <summary>
    /// Jamaica <b>(JM)</b>
    /// </summary>
    Jamaica,

    /// <summary>
    /// Japan <b>(JP)</b>
    /// </summary>
    Japan,

    /// <summary>
    /// Jersey <b>(JE)</b>
    /// </summary>
    Jersey,

    /// <summary>
    /// Jordan <b>(JO)</b>
    /// </summary>
    Jordan,

    /// <summary>
    /// Kazakhstan <b>(KZ)</b>
    /// </summary>
    Kazakhstan,

    /// <summary>
    /// Kenya <b>(KE)</b>
    /// </summary>
    Kenya,

    /// <summary>
    /// Kiribati <b>(KI)</b>
    /// </summary>
    Kiribati,

    /// <summary>
    /// North Korea <b>(KP)</b>
    /// </summary>
    NorthKorea,

    /// <summary>
    /// South Korea <b>(KR)</b>
    /// </summary>
    SouthKorea,

    /// <summary>
    /// Kuwait <b>(KW)</b>
    /// </summary>
    Kuwait,

    /// <summary>
    /// Kyrgyzstan <b>(KG)</b>
    /// </summary>
    Kyrgyzstan,

    /// <summary>
    /// Lao People's Democratic Republic <b>(LA)</b>
    /// </summary>
    LaoPeoplesDemocraticRepublic,

    /// <summary>
    /// Latvia <b>(LV)</b>
    /// </summary>
    Latvia,

    /// <summary>
    /// Lebanon <b>(LB)</b>
    /// </summary>
    Lebanon,

    /// <summary>
    /// Lesotho <b>(LS)</b>
    /// </summary>
    Lesotho,

    /// <summary>
    /// Liberia <b>(LR)</b>
    /// </summary>
    Liberia,

    /// <summary>
    /// Libya <b>(LY)</b>
    /// </summary>
    Libya,

    /// <summary>
    /// Liechtenstein <b>(LI)</b>
    /// </summary>
    Liechtenstein,

    /// <summary>
    /// Lithuania <b>(LT)</b>
    /// </summary>
    Lithuania,

    /// <summary>
    /// Luxembourg <b>(LU)</b>
    /// </summary>
    Luxembourg,

    /// <summary>
    /// Macao <b>(MO)</b>
    /// </summary>
    Macao,

    /// <summary>
    /// the Former Yugoslav Republic of Macedonia <b>(MK)</b>
    /// </summary>
    Macedonia,

    /// <summary>
    /// Madagascar <b>(MG)</b>
    /// </summary>
    Madagascar,

    /// <summary>
    /// Malawi <b>(MW)</b>
    /// </summary>
    Malawi,

    /// <summary>
    /// Malaysia <b>(MY)</b>
    /// </summary>
    Malaysia,

    /// <summary>
    /// Maldives <b>(MV)</b>
    /// </summary>
    Maldives,

    /// <summary>
    /// Mali <b>(ML)</b>
    /// </summary>
    Mali,

    /// <summary>
    /// Malta <b>(MT)</b>
    /// </summary>
    Malta,

    /// <summary>
    /// Marshall Islands <b>(MH)</b>
    /// </summary>
    MarshallIslands,

    /// <summary>
    /// Martinique <b>(MQ)</b>
    /// </summary>
    Martinique,

    /// <summary>
    /// Mauritania <b>(MR)</b>
    /// </summary>
    Mauritania,

    /// <summary>
    /// Mauritius <b>(MU)</b>
    /// </summary>
    Mauritius,

    /// <summary>
    /// Mayotte <b>(YT)</b>
    /// </summary>
    Mayotte,

    /// <summary>
    /// Mexico <b>(MX)</b>
    /// </summary>
    Mexico,

    /// <summary>
    /// Federated States of Micronesia <b>(FM)</b>
    /// </summary>
    Micronesia,

    /// <summary>
    /// Republic of Moldova <b>(MD)</b>
    /// </summary>
    Moldova,

    /// <summary>
    /// Monaco <b>(MC)</b>
    /// </summary>
    Monaco,

    /// <summary>
    /// Mongolia <b>(MN)</b>
    /// </summary>
    Mongolia,

    /// <summary>
    /// Montenegro <b>(ME)</b>
    /// </summary>
    Montenegro,

    /// <summary>
    /// Montserrat <b>(MS)</b>
    /// </summary>
    Montserrat,

    /// <summary>
    /// Morocco <b>(MA)</b>
    /// </summary>
    Morocco,

    /// <summary>
    /// Mozambique <b>(MZ)</b>
    /// </summary>
    Mozambique,

    /// <summary>
    /// Myanmar <b>(MM)</b>
    /// </summary>
    Myanmar,

    /// <summary>
    /// Namibia <b>(NA)</b>
    /// </summary>
    Namibia,

    /// <summary>
    /// Nauru <b>(NR)</b>
    /// </summary>
    Nauru,

    /// <summary>
    /// Nepal <b>(NP)</b>
    /// </summary>
    Nepal,

    /// <summary>
    /// Netherlands <b>(NL)</b>
    /// </summary>
    Netherlands,

    /// <summary>
    /// New Caledonia <b>(NC)</b>
    /// </summary>
    NewCaledonia,

    /// <summary>
    /// New Zealand <b>(NZ)</b>
    /// </summary>
    NewZealand,

    /// <summary>
    /// Nicaragua <b>(NI)</b>
    /// </summary>
    Nicaragua,

    /// <summary>
    /// Niger <b>(NE)</b>
    /// </summary>
    Niger,

    /// <summary>
    /// Nigeria <b>(NG)</b>
    /// </summary>
    Nigeria,

    /// <summary>
    /// Niue <b>(NU)</b>
    /// </summary>
    Niue,

    /// <summary>
    /// Norfolk Island <b>(NF)</b>
    /// </summary>
    NorfolkIsland,

    /// <summary>
    /// Northern Mariana Islands <b>(MP)</b>
    /// </summary>
    NorthernMarianaIslands,

    /// <summary>
    /// Norway <b>(NO)</b>
    /// </summary>
    Norway,

    /// <summary>
    /// Oman <b>(OM)</b>
    /// </summary>
    Oman,

    /// <summary>
    /// Pakistan <b>(PK)</b>
    /// </summary>
    Pakistan,

    /// <summary>
    /// Palau <b>(PW)</b>
    /// </summary>
    Palau,

    /// <summary>
    /// State of Palestine <b>(PS)</b>
    /// </summary>
    Palestine,

    /// <summary>
    /// Panama <b>(PA)</b>
    /// </summary>
    Panama,

    /// <summary>
    /// Papua New Guinea <b>(PG)</b>
    /// </summary>
    PapuaNewGuinea,

    /// <summary>
    /// Paraguay <b>(PY)</b>
    /// </summary>
    Paraguay,

    /// <summary>
    /// Peru <b>(PE)</b>
    /// </summary>
    Peru,

    /// <summary>
    /// Philippines <b>(PH)</b>
    /// </summary>
    Philippines,

    /// <summary>
    /// Pitcairn <b>(PN)</b>
    /// </summary>
    Pitcairn,

    /// <summary>
    /// Poland <b>(PL)</b>
    /// </summary>
    Poland,

    /// <summary>
    /// Portugal <b>(PT)</b>
    /// </summary>
    Portugal,

    /// <summary>
    /// Puerto Rico <b>(PR)</b>
    /// </summary>
    PuertoRico,

    /// <summary>
    /// Qatar <b>(QA)</b>
    /// </summary>
    Qatar,

    /// <summary>
    /// Reunion <b>(RE)</b>
    /// </summary>
    Reunion,

    /// <summary>
    /// Romania <b>(RO)</b>
    /// </summary>
    Romania,

    /// <summary>
    /// Russian Federation <b>(RU)</b>
    /// </summary>
    RussianFederation,

    /// <summary>
    /// Rwanda <b>(RW)</b>
    /// </summary>
    Rwanda,

    /// <summary>
    /// Saint Barthelemy <b>(BL)</b>
    /// </summary>
    SaintBarthelemy,

    /// <summary>
    /// Ascension and Tristan da Cunha Saint Helena <b>(SH)</b>
    /// </summary>
    SaintHelena,

    /// <summary>
    /// Saint Kitts and Nevis <b>(KN)</b>
    /// </summary>
    SaintKittsAndNevis,

    /// <summary>
    /// Saint Lucia <b>(LC)</b>
    /// </summary>
    SaintLucia,

    /// <summary>
    /// Saint Martin (French part) <b>(MF)</b>
    /// </summary>
    SaintMartinFrenchpart,

    /// <summary>
    /// Saint Pierre and Miquelon <b>(PM)</b>
    /// </summary>
    SaintPierreAndMiquelon,

    /// <summary>
    /// Saint Vincent and the Grenadines <b>(VC)</b>
    /// </summary>
    SaintVincentAndtheGrenadines,

    /// <summary>
    /// Samoa <b>(WS)</b>
    /// </summary>
    Samoa,

    /// <summary>
    /// San Marino <b>(SM)</b>
    /// </summary>
    SanMarino,

    /// <summary>
    /// Sao Tome and Principe <b>(ST)</b>
    /// </summary>
    SaoTomeAndPrincipe,

    /// <summary>
    /// Saudi Arabia <b>(SA)</b>
    /// </summary>
    SaudiArabia,

    /// <summary>
    /// Senegal <b>(SN)</b>
    /// </summary>
    Senegal,

    /// <summary>
    /// Serbia <b>(RS)</b>
    /// </summary>
    Serbia,

    /// <summary>
    /// Seychelles <b>(SC)</b>
    /// </summary>
    Seychelles,

    /// <summary>
    /// Sierra Leone <b>(SL)</b>
    /// </summary>
    SierraLeone,

    /// <summary>
    /// Singapore <b>(SG)</b>
    /// </summary>
    Singapore,

    /// <summary>
    /// Sint Maarten (Dutch part) <b>(SX)</b>
    /// </summary>
    SintMaartenDutchpart,

    /// <summary>
    /// Slovakia <b>(SK)</b>
    /// </summary>
    Slovakia,

    /// <summary>
    /// Slovenia <b>(SI)</b>
    /// </summary>
    Slovenia,

    /// <summary>
    /// Solomon Islands <b>(SB)</b>
    /// </summary>
    SolomonIslands,

    /// <summary>
    /// Somalia <b>(SO)</b>
    /// </summary>
    Somalia,

    /// <summary>
    /// South Africa <b>(ZA)</b>
    /// </summary>
    SouthAfrica,

    /// <summary>
    /// South Georgia and the South Sandwich Islands <b>(GS)</b>
    /// </summary>
    SouthGeorgiaAndtheSouthSandwichIslands,

    /// <summary>
    /// South Sudan <b>(SS)</b>
    /// </summary>
    SouthSudan,

    /// <summary>
    /// Spain <b>(ES)</b>
    /// </summary>
    Spain,

    /// <summary>
    /// Sri Lanka <b>(LK)</b>
    /// </summary>
    SriLanka,

    /// <summary>
    /// Sudan <b>(SD)</b>
    /// </summary>
    Sudan,

    /// <summary>
    /// Suriname <b>(SR)</b>
    /// </summary>
    Suriname,

    /// <summary>
    /// Svalbard and Jan Mayen <b>(SJ)</b>
    /// </summary>
    SvalbardAndJanMayen,

    /// <summary>
    /// Swaziland <b>(SZ)</b>
    /// </summary>
    Swaziland,

    /// <summary>
    /// Sweden <b>(SE)</b>
    /// </summary>
    Sweden,

    /// <summary>
    /// Switzerland <b>(CH)</b>
    /// </summary>
    Switzerland,

    /// <summary>
    /// Syrian Arab Republic <b>(SY)</b>
    /// </summary>
    SyrianArabRepublic,

    /// <summary>
    /// Province of China Taiwan <b>(TW)</b>
    /// </summary>
    Taiwan,

    /// <summary>
    /// Tajikistan <b>(TJ)</b>
    /// </summary>
    Tajikistan,

    /// <summary>
    /// United Republic of Tanzania <b>(TZ)</b>
    /// </summary>
    Tanzania,

    /// <summary>
    /// Thailand <b>(TH)</b>
    /// </summary>
    Thailand,

    /// <summary>
    /// Timor-Leste <b>(TL)</b>
    /// </summary>
    TimorLeste,

    /// <summary>
    /// Togo <b>(TG)</b>
    /// </summary>
    Togo,

    /// <summary>
    /// Tokelau <b>(TK)</b>
    /// </summary>
    Tokelau,

    /// <summary>
    /// Tonga <b>(TO)</b>
    /// </summary>
    Tonga,

    /// <summary>
    /// Trinidad and Tobago <b>(TT)</b>
    /// </summary>
    TrinidadAndTobago,

    /// <summary>
    /// Tunisia <b>(TN)</b>
    /// </summary>
    Tunisia,

    /// <summary>
    /// Turkey <b>(TR)</b>
    /// </summary>
    Turkey,

    /// <summary>
    /// Turkmenistan <b>(TM)</b>
    /// </summary>
    Turkmenistan,

    /// <summary>
    /// Turks and Caicos Islands <b>(TC)</b>
    /// </summary>
    TurksAndCaicosIslands,

    /// <summary>
    /// Tuvalu <b>(TV)</b>
    /// </summary>
    Tuvalu,

    /// <summary>
    /// Uganda <b>(UG)</b>
    /// </summary>
    Uganda,

    /// <summary>
    /// Ukraine <b>(UA)</b>
    /// </summary>
    Ukraine,

    /// <summary>
    /// United Arab Emirates <b>(AE)</b>
    /// </summary>
    UnitedArabEmirates,

    /// <summary>
    /// United Kingdom <b>(GB)</b>
    /// </summary>
    UnitedKingdom,

    /// <summary>
    /// United States <b>(US)</b>
    /// </summary>
    UnitedStates,

    /// <summary>
    /// United States Minor Outlying Islands <b>(UM)</b>
    /// </summary>
    UnitedStatesMinorOutlyingIslands,

    /// <summary>
    /// Uruguay <b>(UY)</b>
    /// </summary>
    Uruguay,

    /// <summary>
    /// Uzbekistan <b>(UZ)</b>
    /// </summary>
    Uzbekistan,

    /// <summary>
    /// Vanuatu <b>(VU)</b>
    /// </summary>
    Vanuatu,

    /// <summary>
    /// Bolivarian Republic of Venezuela <b>(VE)</b>
    /// </summary>
    Venezuela,

    /// <summary>
    /// Viet Nam <b>(VN)</b>
    /// </summary>
    VietNam,

    /// <summary>
    /// Virgin Islands British <b>(VG)</b>
    /// </summary>
    VirginIslandsBritish,

    /// <summary>
    /// Virgin Islands US <b>(VI)</b>
    /// </summary>
    VirginIslandsUS,

    /// <summary>
    /// Wallis and Futuna <b>(WF)</b>
    /// </summary>
    WallisAndFutuna,

    /// <summary>
    /// Western Sahara <b>(EH)</b>
    /// </summary>
    WesternSahara,

    /// <summary>
    /// Yemen <b>(YE)</b>
    /// </summary>
    Yemen,

    /// <summary>
    /// Zambia <b>(ZM)</b>
    /// </summary>
    Zambia,

    /// <summary>
    /// Zimbabwe <b>(ZW)</b>
    /// </summary>
    Zimbabwe
}