using FatecSisMed.Web.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using FatecSisMed.Web.Services.Interfaces;

namespace FatecSisMed.Web.Services.Entities;

public class RemedioService : IRemedioService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/remedio/";
    private RemedioViewModel _remedioViewModel;
    private IEnumerable<RemedioViewModel> remedios;

    public RemedioService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions
        { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<RemedioViewModel>> GetAllRemedios(string token)
    {
        var client = _clientFactory.CreateClient("RemedioAPI");
        PutTokenInHeaderAutorization(token, client);

        var response = await client.GetAsync(apiEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            remedios = await JsonSerializer
                .DeserializeAsync<IEnumerable<RemedioViewModel>>(apiResponse, _options);

        }
        else
            return null;

        return remedios;
    }

    public async Task<RemedioViewModel> FindRemedioById(int id, string token)
    {
        var client = _clientFactory.CreateClient("RemedioAPI");

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _remedioViewModel = await JsonSerializer
                    .DeserializeAsync<RemedioViewModel>(apiResponse, _options);
            }
            else
                return null;
        }
        return _remedioViewModel;
    }

    public async Task<RemedioViewModel> CreateRemedio(RemedioViewModel remedioViewModel, string token)
    {
        var client = _clientFactory.CreateClient("RemedioAPI");

        StringContent content = new StringContent(
            JsonSerializer.Serialize(remedioViewModel),
                Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _remedioViewModel = await JsonSerializer
                    .DeserializeAsync<RemedioViewModel>(apiResponse, _options);
            }
            else
                return null;
        }
        return _remedioViewModel;
    }

    public async Task<RemedioViewModel> UpdateRemedio(RemedioViewModel remedioViewModel, string token)
    {
        var client = _clientFactory.CreateClient("RemedioAPI");

        RemedioViewModel remedio = new RemedioViewModel();

        using (var response = await client.PutAsJsonAsync(
            apiEndpoint, remedioViewModel))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                remedio = await JsonSerializer
                    .DeserializeAsync<RemedioViewModel>(apiResponse, _options);
            }
            else
                return null;
        }

        return remedio;
    }

    public async Task<bool> DeleteRemedioById(int id, string token)
    {
        var client = _clientFactory.CreateClient("RemedioAPI");
        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode) return true;
        }
        return false;
    }

    private static void PutTokenInHeaderAutorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
