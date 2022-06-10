using System.Text;
using System.Text.Json;
using System.Web;

namespace Utility;

public class ScanerUtility
{
    private static HttpClient _client = new HttpClient();
    public ScanerUtility()
    {
        _client.BaseAddress = new Uri("https://localhost:7259/");
        _client.DefaultRequestHeaders.Accept.Clear();
    }

    public string GetStatus(int id)
    {
        HttpResponseMessage responce = _client.GetAsync($"Searcher/status?id={id}").Result;
        if (responce.IsSuccessStatusCode)
        {
            var status = responce.Content.ReadAsStringAsync();
            Console.WriteLine(status.Result);
            return status.Result;
        }
        return "error";
    }

    public void ScanDirectory(string directory)
    {
        string requestData = $"directory={HttpUtility.UrlEncode(directory)}";
        var data = new StringContent(requestData, Encoding.UTF8, "text/plain");

        HttpResponseMessage responce = _client.PostAsync($"{_client.BaseAddress}Searcher/scan?{requestData}", data).Result;
        if (responce.IsSuccessStatusCode)
        {
            var status = responce.Content.ReadAsStringAsync();
            Console.WriteLine(status.Result);
        }
    }
    
}