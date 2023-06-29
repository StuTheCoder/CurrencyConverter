using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RateData

{
    [JsonProperty("USD")]
    public double USD { get; set; }

    [JsonProperty("EUR")]
    public double EUR { get; set; }
}