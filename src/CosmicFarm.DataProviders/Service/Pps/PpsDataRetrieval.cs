using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using CosmicFarm.Domain.Models;

namespace CosmicFarm.DataProviders.Service.Pps;

public class PpsDataRetrieval : IDataRetrival<Precipitation>
{
    private const string _userName = "joaovicsrodrigues@gmail.com"; //The same as the password
    private readonly HttpClient _httpClient;

    public PpsDataRetrieval(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new("https://arthurhouhttps.pps.eosdis.nasa.gov");
        var basic = Encoding.ASCII.GetBytes($"{_userName}:{_userName}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(basic)
        );
    }

    public Task<Precipitation> GetDataFromSource(DateTime start)
    {
        // example: https://arthurhouhttps.pps.eosdis.nasa.gov/gpmdata/2024/10/05/1A/1A.GPM.GMI.COUNT2021.20241005-S150124-E163438.060215.V07B.HDF5
        StringBuilder bobTheBuilder = new StringBuilder("/gpmdata/");
        bobTheBuilder.AppendFormat(
            CultureInfo.InvariantCulture,
            "{0}/{1}/{2}",
            start.Year,
            start.Month,
            start.Day
        );
        string sufix = "1a?2B?";
    }
}
