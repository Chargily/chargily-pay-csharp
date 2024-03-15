using System.Text.Json.Serialization;

namespace Chargily.Pay.Models;

/// <summary>
/// ISO 4217 3-letter currency codes
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Currency
{
    /// <summary>
    /// United Arab Emirates Dirham <b>(Dirham)</b>
    /// </summary>
    AED,

    /// <summary>
    /// Afghan Afghani <b>(Afghani)</b>
    /// </summary>
    AFN,

    /// <summary>
    /// Albanian Lek <b>(Lek)</b>
    /// </summary>
    ALL,

    /// <summary>
    /// Armenian Dram <b>(Dram)</b>
    /// </summary>
    AMD,

    /// <summary>
    /// Netherlands Antillean Guilder <b>(Guilder)</b>
    /// </summary>
    ANG,

    /// <summary>
    /// Angolan Kwanza <b>(Kwanza)</b>
    /// </summary>
    AOA,

    /// <summary>
    /// Argentine Peso <b>(Peso)</b>
    /// </summary>
    ARS,

    /// <summary>
    /// Australian Dollar <b>(Dollar)</b>
    /// </summary>
    AUD,

    /// <summary>
    /// Aruban Florin <b>(Florin)</b>
    /// </summary>
    AWG,

    /// <summary>
    /// Azerbaijani Manat <b>(Manat)</b>
    /// </summary>
    AZN,

    /// <summary>
    /// Bosnia and Herzegovina Convertible Mark <b>(Convertible Mark)</b>
    /// </summary>
    BAM,

    /// <summary>
    /// Barbadian Dollar <b>(Dollar)</b>
    /// </summary>
    BBD,

    /// <summary>
    /// Bangladeshi Taka <b>(Taka)</b>
    /// </summary>
    BDT,

    /// <summary>
    /// Bulgarian Lev <b>(Lev)</b>
    /// </summary>
    BGN,

    /// <summary>
    /// Bahraini Dinar <b>(Dinar)</b>
    /// </summary>
    BHD,

    /// <summary>
    /// Burundian Franc <b>(Franc)</b>
    /// </summary>
    BIF,

    /// <summary>
    /// Bermudian Dollar <b>(Dollar)</b>
    /// </summary>
    BMD,

    /// <summary>
    /// Brunei Dollar <b>(Dollar)</b>
    /// </summary>
    BND,

    /// <summary>
    /// Bolivian Boliviano <b>(Boliviano)</b>
    /// </summary>
    BOB,

    /// <summary>
    /// Brazilian Real <b>(Real)</b>
    /// </summary>
    BRL,

    /// <summary>
    /// Bahamian Dollar <b>(Dollar)</b>
    /// </summary>
    BSD,

    /// <summary>
    /// Bhutanese Ngultrum <b>(Ngultrum)</b>
    /// </summary>
    BTN,

    /// <summary>
    /// Botswana Pula <b>(Pula)</b>
    /// </summary>
    BWP,

    /// <summary>
    /// Belarusian Ruble <b>(Ruble)</b>
    /// </summary>
    BYN,

    /// <summary>
    /// Belize Dollar <b>(Dollar)</b>
    /// </summary>
    BZD,

    /// <summary>
    /// Canadian Dollar <b>(Dollar)</b>
    /// </summary>
    CAD,

    /// <summary>
    /// Congolese Franc <b>(Franc)</b>
    /// </summary>
    CDF,

    /// <summary>
    /// Swiss Franc <b>(Franc)</b>
    /// </summary>
    CHF,

    /// <summary>
    /// Cook Islands Dollar <b>(Dollar)</b>
    /// </summary>
    CKD,

    /// <summary>
    /// Chilean Peso <b>(Peso)</b>
    /// </summary>
    CLP,

    /// <summary>
    /// Chinese Yuan <b>(Yuan)</b>
    /// </summary>
    CNY,

    /// <summary>
    /// Colombian Peso <b>(Peso)</b>
    /// </summary>
    COP,

    /// <summary>
    /// Costa Rican Colon <b>(Colón)</b>
    /// </summary>
    CRC,

    /// <summary>
    /// Cuban convertible Peso <b>(Peso)</b>
    /// </summary>
    CUC,

    /// <summary>
    /// Cuban Peso <b>(Peso)</b>
    /// </summary>
    CUP,

    /// <summary>
    /// Cabo Verdean Escudo <b>(Escudo)</b>
    /// </summary>
    CVE,

    /// <summary>
    /// Czech Koruna <b>(Koruna)</b>
    /// </summary>
    CZK,

    /// <summary>
    /// Djiboutian Franc <b>(Franc)</b>
    /// </summary>
    DJF,

    /// <summary>
    /// Danish Krone <b>(Krone)</b>
    /// </summary>
    DKK,

    /// <summary>
    /// Dominican Peso <b>(Peso)</b>
    /// </summary>
    DOP,

    /// <summary>
    /// Algerian Dinar <b>(Dinar)</b>
    /// </summary>
    DZD,

    /// <summary>
    /// Egyptian Pound <b>(Pound)</b>
    /// </summary>
    EGP,

    /// <summary>
    /// Sahrawi Peseta <b>(Peseta)</b>
    /// </summary>
    EHP,

    /// <summary>
    /// Eritrean Nakfa <b>(Nakfa)</b>
    /// </summary>
    ERN,

    /// <summary>
    /// Ethiopian Birr <b>(Birr)</b>
    /// </summary>
    ETB,

    /// <summary>
    /// Euro <b>(Euro)</b>
    /// </summary>
    EUR,

    /// <summary>
    /// Fijian Dollar <b>(Dollar)</b>
    /// </summary>
    FJD,

    /// <summary>
    /// Falkland Islands Pound <b>(Pound)</b>
    /// </summary>
    FKP,

    /// <summary>
    /// Faroese Króna <b>(Króna)</b>
    /// </summary>
    FOK,

    /// <summary>
    /// Pound Sterling <b>(Pound)</b>
    /// </summary>
    GBP,

    /// <summary>
    /// Georgian Lari <b>(Lari)</b>
    /// </summary>
    GEL,

    /// <summary>
    /// Guernsey Pound <b>(Pound)</b>
    /// </summary>
    GGP,

    /// <summary>
    /// Ghanaian Cedi <b>(Cedi)</b>
    /// </summary>
    GHS,

    /// <summary>
    /// Gibraltar Pound <b>(Pound)</b>
    /// </summary>
    GIP,

    /// <summary>
    /// Gambian Dalasi <b>(Dalasi)</b>
    /// </summary>
    GMD,

    /// <summary>
    /// Guinean Franc <b>(Franc)</b>
    /// </summary>
    GNF,

    /// <summary>
    /// Guatemalan Quetzal <b>(Quetzal)</b>
    /// </summary>
    GTQ,

    /// <summary>
    /// Guyanese Dollar <b>(Dollar)</b>
    /// </summary>
    GYD,

    /// <summary>
    /// Hong Kong Dollar <b>(Dollar)</b>
    /// </summary>
    HKD,

    /// <summary>
    /// Honduran Lempira <b>(Lempira)</b>
    /// </summary>
    HNL,

    /// <summary>
    /// Croatian Kuna <b>(Kuna)</b>
    /// </summary>
    HRK,

    /// <summary>
    /// Haitian Gourde <b>(Gourde)</b>
    /// </summary>
    HTG,

    /// <summary>
    /// Hungarian Forint <b>(Forint)</b>
    /// </summary>
    HUF,

    /// <summary>
    /// Indonesian Rupiah <b>(Rupiah)</b>
    /// </summary>
    IDR,

    /// <summary>
    /// Israeli new Shekel <b>(Shekel)</b>
    /// </summary>
    ILS,

    /// <summary>
    /// Manx Pound <b>(Pound)</b>
    /// </summary>
    IMP,

    /// <summary>
    /// Indian Rupee <b>(Rupee)</b>
    /// </summary>
    INR,

    /// <summary>
    /// Iraqi Dinar <b>(Dinar)</b>
    /// </summary>
    IQD,

    /// <summary>
    /// Iranian Rial <b>(Rial)</b>
    /// </summary>
    IRR,

    /// <summary>
    /// Icelandic Krona <b>(Krona)</b>
    /// </summary>
    ISK,

    /// <summary>
    /// Jersey Pound <b>(Pound)</b>
    /// </summary>
    JEP,

    /// <summary>
    /// Jamaican Dollar <b>(Dollar)</b>
    /// </summary>
    JMD,

    /// <summary>
    /// Jordanian Dinar <b>(Dinar)</b>
    /// </summary>
    JOD,

    /// <summary>
    /// Japanese Yen <b>(Yen)</b>
    /// </summary>
    JPY,

    /// <summary>
    /// Kenyan Shilling <b>(Shilling)</b>
    /// </summary>
    KES,

    /// <summary>
    /// Kyrgyzstani Som <b>(Som)</b>
    /// </summary>
    KGS,

    /// <summary>
    /// Cambodian Riel <b>(Riel)</b>
    /// </summary>
    KHR,

    /// <summary>
    /// Kiribati Dollar <b>(Dollar)</b>
    /// </summary>
    KID,

    /// <summary>
    /// Comorian Franc <b>(Franc)</b>
    /// </summary>
    KMF,

    /// <summary>
    /// North Korean Won <b>(Won)</b>
    /// </summary>
    KPW,

    /// <summary>
    /// South Korean Won <b>(Won)</b>
    /// </summary>
    KRW,

    /// <summary>
    /// Kuwaiti Dinar <b>(Dinar)</b>
    /// </summary>
    KWD,

    /// <summary>
    /// Cayman Islands Dollar <b>(Dollar)</b>
    /// </summary>
    KYD,

    /// <summary>
    /// Kazakhstani Tenge <b>(Tenge)</b>
    /// </summary>
    KZT,

    /// <summary>
    /// Lao Kip <b>(Kip)</b>
    /// </summary>
    LAK,

    /// <summary>
    /// Lebanese Pound <b>(Pound)</b>
    /// </summary>
    LBP,

    /// <summary>
    /// Sri Lankan Rupee <b>(Rupee)</b>
    /// </summary>
    LKR,

    /// <summary>
    /// Liberian Dollar <b>(Dollar)</b>
    /// </summary>
    LRD,

    /// <summary>
    /// Lesotho Loti <b>(Loti)</b>
    /// </summary>
    LSL,

    /// <summary>
    /// Libyan Dinar <b>(Dinar)</b>
    /// </summary>
    LYD,

    /// <summary>
    /// Moroccan Dirham <b>(Dirham)</b>
    /// </summary>
    MAD,

    /// <summary>
    /// Moldovan Leu <b>(Leu)</b>
    /// </summary>
    MDL,

    /// <summary>
    /// Malagasy Ariary <b>(Ariary)</b>
    /// </summary>
    MGA,

    /// <summary>
    /// Macedonian Denar <b>(Denar)</b>
    /// </summary>
    MKD,

    /// <summary>
    /// Myanmar Kyat <b>(Kyat)</b>
    /// </summary>
    MMK,

    /// <summary>
    /// Mongolian Tögrög <b>(Tögrög)</b>
    /// </summary>
    MNT,

    /// <summary>
    /// Macanese Pataca <b>(Pataca)</b>
    /// </summary>
    MOP,

    /// <summary>
    /// Mauritanian Ouguiya <b>(Ouguiya)</b>
    /// </summary>
    MRU,

    /// <summary>
    /// Mauritian Rupee <b>(Rupee)</b>
    /// </summary>
    MUR,

    /// <summary>
    /// Maldivian Rufiyaa <b>(Rufiyaa)</b>
    /// </summary>
    MVR,

    /// <summary>
    /// Malawian Kwacha <b>(Kwacha)</b>
    /// </summary>
    MWK,

    /// <summary>
    /// Mexican Peso <b>(Peso)</b>
    /// </summary>
    MXN,

    /// <summary>
    /// Malaysian Ringgit <b>(Ringgit)</b>
    /// </summary>
    MYR,

    /// <summary>
    /// Mozambican Metical <b>(Metical)</b>
    /// </summary>
    MZN,

    /// <summary>
    /// Namibian Dollar <b>(Dollar)</b>
    /// </summary>
    NAD,

    /// <summary>
    /// Nigerian Naira <b>(Naira)</b>
    /// </summary>
    NGN,

    /// <summary>
    /// Nicaraguan Córdoba <b>(Córdoba Oro)</b>
    /// </summary>
    NIO,

    /// <summary>
    /// Norwegian Krone <b>(Krone)</b>
    /// </summary>
    NOK,

    /// <summary>
    /// Nepalese Rupee <b>(Rupee)</b>
    /// </summary>
    NPR,

    /// <summary>
    /// New Zealand Dollar <b>(Dollar)</b>
    /// </summary>
    NZD,

    /// <summary>
    /// Omani Rial <b>(Rial)</b>
    /// </summary>
    OMR,

    /// <summary>
    /// Panamanian Balboa <b>(Balboa)</b>
    /// </summary>
    PAB,

    /// <summary>
    /// Peruvian Sol <b>(Sol)</b>
    /// </summary>
    PEN,

    /// <summary>
    /// Papua New Guinean Kina <b>(Kina)</b>
    /// </summary>
    PGK,

    /// <summary>
    /// Philippine Peso <b>(Peso)</b>
    /// </summary>
    PHP,

    /// <summary>
    /// Pakistani Rupee <b>(Rupee)</b>
    /// </summary>
    PKR,

    /// <summary>
    /// Polish Zloty <b>(Zloty)</b>
    /// </summary>
    PLN,

    /// <summary>
    /// Pitcairn Islands Dollar <b>(Dollar)</b>
    /// </summary>
    PND,

    /// <summary>
    /// Transnistrian Ruble <b>(Ruble)</b>
    /// </summary>
    PRB,

    /// <summary>
    /// Paraguayan Guaraní <b>(Guaraní)</b>
    /// </summary>
    PYG,

    /// <summary>
    /// Qatari Riyal <b>(Riyal)</b>
    /// </summary>
    QAR,

    /// <summary>
    /// Romanian Leu <b>(Leu)</b>
    /// </summary>
    RON,

    /// <summary>
    /// Serbian Dinar <b>(Dinar)</b>
    /// </summary>
    RSD,

    /// <summary>
    /// Russian Ruble <b>(Ruble)</b>
    /// </summary>
    RUB,

    /// <summary>
    /// Rwandan Franc <b>(Franc)</b>
    /// </summary>
    RWF,

    /// <summary>
    /// Saudi Riyal <b>(Riyal)</b>
    /// </summary>
    SAR,

    /// <summary>
    /// Solomon Islands Dollar <b>(Dollar)</b>
    /// </summary>
    SBD,

    /// <summary>
    /// Seychellois Rupee <b>(Rupee)</b>
    /// </summary>
    SCR,

    /// <summary>
    /// Sudanese Pound <b>(Pound)</b>
    /// </summary>
    SDG,

    /// <summary>
    /// Swedish Krona <b>(Krona)</b>
    /// </summary>
    SEK,

    /// <summary>
    /// Singapore Dollar <b>(Dollar)</b>
    /// </summary>
    SGD,

    /// <summary>
    /// Saint Helena Pound <b>(Pound)</b>
    /// </summary>
    SHP,

    /// <summary>
    /// Sierra Leonean Leone <b>(Leone)</b>
    /// </summary>
    SLL,

    /// <summary>
    /// Somaliland Shilling <b>(Shilling)</b>
    /// </summary>
    SLS,

    /// <summary>
    /// Somali Shilling <b>(Shilling)</b>
    /// </summary>
    SOS,

    /// <summary>
    /// Surinamese Dollar <b>(Dollar)</b>
    /// </summary>
    SRD,

    /// <summary>
    /// South Sudanese Pound <b>(Pound)</b>
    /// </summary>
    SSP,

    /// <summary>
    /// Sao Tome and Príncipe Dobra <b>(Dobra)</b>
    /// </summary>
    STN,

    /// <summary>
    /// Salvadoran Colón <b>(Colón)</b>
    /// </summary>
    SVC,

    /// <summary>
    /// Syrian Pound <b>(Pound)</b>
    /// </summary>
    SYP,

    /// <summary>
    /// Swazi Lilangeni <b>(Lilangeni)</b>
    /// </summary>
    SZL,

    /// <summary>
    /// Thai Baht <b>(Baht)</b>
    /// </summary>
    THB,

    /// <summary>
    /// Tajikistani Somoni <b>(Somoni)</b>
    /// </summary>
    TJS,

    /// <summary>
    /// Turkmenistan Manat <b>(Manat)</b>
    /// </summary>
    TMT,

    /// <summary>
    /// Tunisian Dinar <b>(Dinar)</b>
    /// </summary>
    TND,

    /// <summary>
    /// Tongan Paʻanga <b>(Pa'anga)</b>
    /// </summary>
    TOP,

    /// <summary>
    /// Turkish Lira <b>(Lira)</b>
    /// </summary>
    TRY,

    /// <summary>
    /// Trinidad and Tobago Dollar <b>(Dollar)</b>
    /// </summary>
    TTD,

    /// <summary>
    /// Tuvaluan Dollar <b>(Dollar)</b>
    /// </summary>
    TVD,

    /// <summary>
    /// New Taiwan Dollar <b>(Dollar)</b>
    /// </summary>
    TWD,

    /// <summary>
    /// Tanzanian Shilling <b>(Shilling)</b>
    /// </summary>
    TZS,

    /// <summary>
    /// Ukrainian Hryvnia <b>(Hryvnia)</b>
    /// </summary>
    UAH,

    /// <summary>
    /// Ugandan Shilling <b>(Shilling)</b>
    /// </summary>
    UGX,

    /// <summary>
    /// United States Dollar <b>(Dollar)</b>
    /// </summary>
    USD,

    /// <summary>
    /// Uruguayan Peso <b>(Peso)</b>
    /// </summary>
    UYU,

    /// <summary>
    /// Uzbekistani Som <b>(Som)</b>
    /// </summary>
    UZS,

    /// <summary>
    /// Venezuelan bolívar digital <b>(Bolívar Digital)</b>
    /// </summary>
    VED,

    /// <summary>
    /// Venezuelan Bolívar Soberano <b>(Bolívar)</b>
    /// </summary>
    VES,

    /// <summary>
    /// Vietnamese Dong <b>(Dong)</b>
    /// </summary>
    VND,

    /// <summary>
    /// Vanuatu Vatu <b>(Vatu)</b>
    /// </summary>
    VUV,

    /// <summary>
    /// Samoan Tala <b>(Tala)</b>
    /// </summary>
    WST,

    /// <summary>
    /// Central African CFA Franc BEAC <b>(Franc)</b>
    /// </summary>
    XAF,

    /// <summary>
    /// East Caribbean Dollar <b>(Dollar)</b>
    /// </summary>
    XCD,

    /// <summary>
    /// West African CFA Franc BCEAO <b>(Franc)</b>
    /// </summary>
    XOF,

    /// <summary>
    /// CFP Franc (Franc Pacifique) <b>(Franc)</b>
    /// </summary>
    XPF,

    /// <summary>
    /// Yemeni Rial <b>(Rial)</b>
    /// </summary>
    YER,

    /// <summary>
    /// South African Rand <b>(Rand)</b>
    /// </summary>
    ZAR,

    /// <summary>
    /// Zambian Kwacha <b>(Kwacha)</b>
    /// </summary>
    ZMW,

    /// <summary>
    /// RTGS Dollar <b>(Dollar)</b>
    /// </summary>
    ZWB,

    /// <summary>
    /// Zimbabwean Dollar <b>(Dollar)</b>
    /// </summary>
    ZWL,

    /// <summary>
    /// Abkhazian Apsar <b>(Apsar)</b>
    /// </summary>
    ABKHAZIA,

    /// <summary>
    /// Artsakh Dram <b>(Dram)</b>
    /// </summary>
    ARTSAKH
};