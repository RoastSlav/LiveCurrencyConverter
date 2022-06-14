using System.Net.Http;

namespace LiveCurrencyConverter.Library.Api;

public class ApiHelper
{
    private HttpClient _httpClient;
    private const string apiBaseAddress = "https://free.currconv.com";

    public ApiHelper()
    {
        InitializeClient();
    }

    public HttpClient HttpClient
    {
        get { return _httpClient; }
    }

    private void InitializeClient()
    {
        _httpClient = new();
        _httpClient.BaseAddress = new(apiBaseAddress);
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
    }
}