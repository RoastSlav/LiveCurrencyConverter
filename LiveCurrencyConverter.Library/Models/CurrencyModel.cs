using Newtonsoft.Json;

namespace LiveCurrencyConverter.Library.Models;

public class CurrencyModel
{
    [JsonProperty("currencyName")]
    public string CurrencyName { get; set; }

    [JsonProperty("currencySymbol")]
    public string CurrencySymbol { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }
}