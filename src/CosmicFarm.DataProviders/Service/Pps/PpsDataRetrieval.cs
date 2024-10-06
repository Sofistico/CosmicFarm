using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using CosmicFarm.Domain.Models;
using HDF5CSharp;
using Microsoft.Extensions.Logging;

namespace CosmicFarm.DataProviders.Service.Pps;

public class PpsDataRetrieval : IDataRetrival<Precipitation>
{
    private const string _userName = "joaovicsrodrigues@gmail.com"; //The same as the password
    private readonly HttpClient _httpClient;
    private readonly ILogger<PpsDataRetrieval> _log;

    public PpsDataRetrieval(HttpClient httpClient, ILogger<PpsDataRetrieval> log)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new("https://arthurhouhttps.pps.eosdis.nasa.gov/text");
        var basic = Encoding.ASCII.GetBytes($"{_userName}:{_userName}");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            "Basic",
            Convert.ToBase64String(basic)
        );
        _log = log;
    }

    public async Task<Precipitation> GetDataFromSource(DateTime start)
    {
        // example: https://arthurhouhttps.pps.eosdis.nasa.gov/gpmdata/2024/10/05/1A/1A.GPM.GMI.COUNT2021.20241005-S150124-E163438.060215.V07B.HDF5
        StringBuilder bobTheBuilder = new StringBuilder("/gpmdata/");
        bobTheBuilder.AppendFormat(
            CultureInfo.InvariantCulture,
            "{0}/{1}/{2}/1C",
            start.Year,
            start.Month,
            start.Day
        );
        var list = await GetFileList(bobTheBuilder.ToString());
        if(list == null)
            return null;

        var precipitacao = new Precipitation();
        foreach (var file in list)
        {
            await using var stream = await _httpClient.GetStreamAsync(file);
            var hd5 = Hdf5.
        }
    }

    private async Task<string[]?> GetFileList(string request)
    {
        var result = await _httpClient.GetAsync(request);
        var response = result.RequestMessage.Content.ToString();
        if(!result.IsSuccessStatusCode){
            _log.LogError("Houve um erro na response, Reponse: {Response}", response);
            return [];
        }
        return response?.Split("/n");
    }
}
