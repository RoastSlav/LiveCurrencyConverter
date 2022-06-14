using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LiveCurrencyConverter.Library.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LiveCurrencyConverter.Library.Api;

public class CurrencyEndpoint
{
    private readonly ApiHelper _apiHelper;
    private const string apiKey = "apiKey=efa6e5f7ba3fe0809417";

    public CurrencyEndpoint(ApiHelper apiHelper)
    {
        _apiHelper = apiHelper;
    }

    public async Task<double> GetExchangeRateAsync(CurrencyModel from, CurrencyModel to)
    {
        string url = "/api/v7/convert?q=" + from.Id + "_" + to.Id + "&compact=ultra&";

        using HttpResponseMessage responseMessage = await _apiHelper.HttpClient.GetAsync(url + apiKey);
        if (responseMessage.IsSuccessStatusCode)
        {
            var result = await responseMessage.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(result);
            return data.First().Value;
        }
        else
        {
            throw new(responseMessage.ReasonPhrase);
        }
    }

    public async Task<List<CurrencyModel>> GetAllCurrenciesAsync()
    {
        using HttpResponseMessage responseMessage = await _apiHelper.HttpClient.GetAsync("/api/v7/currencies?" + apiKey);
        if (responseMessage.IsSuccessStatusCode)
        {
            var result = await responseMessage.Content.ReadAsStringAsync();
            var data = JObject.Parse(result)["results"].ToArray();
            return data.Select(item => item.First.ToObject<CurrencyModel>()).ToList();
        }
        else
        {
            throw new(responseMessage.ReasonPhrase);
        }
    }

    public async Task<List<CountriesModel>> GetAllCountriesAsync()
    {
        using HttpResponseMessage responseMessage = await _apiHelper.HttpClient.GetAsync("/api/v7/countries?" + apiKey);
        if (responseMessage.IsSuccessStatusCode)
        {
            var result = await responseMessage.Content.ReadAsAsync<List<CountriesModel>>();
            return result;
        }
        else
        {
            throw new(responseMessage.ReasonPhrase);
        }
    }
}