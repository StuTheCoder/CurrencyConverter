using System;
using System.Net;
using Newtonsoft.Json;

public class ExchangeRates
{
    [JsonProperty("base")]
    public string BaseCurrency { get; set; }

    [JsonProperty("rates")]
    public RateData Rates { get; set; }
}