using FatecSisMed.Web.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using FatecSisMed.Web.Services.Interfaces;

namespace FatecSisMed.Web.Services.Entities;

public class MarcaService : IMarcaService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/marca/";
    private MarcaViewModel _marcaViewModel;
    private IEnumerable<MarcaViewModel> marcas;

    public MarcaService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions
        { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<MarcaViewModel>> GetAllMarcas(string token)
    {
        var client = _clientFactory.CreateClient("MarcaAPI");
        PutTokenInHeaderAutorization(token, client);

        var response = await client.GetAsync(apiEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            marcas = await JsonSerializer
                .DeserializeAsync<IEnumerable<MarcaViewModel>>(apiResponse, _options);

        }
        else
            return null;

        return marcas;
    }

    public async Task<MarcaViewModel> FindMarcaById(int id, string token)
    {
        var client = _clientFactory.CreateClient("MarcaAPI");

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _marcaViewModel = await JsonSerializer
                    .DeserializeAsync<MarcaViewModel>(apiResponse, _options);
            }
            else
                return null;
        }
        return _marcaViewModel;
    }

    public async Task<MarcaViewModel> CreateMarca(MarcaViewModel marcaViewModel, string token)
    {
        var client = _clientFactory.CreateClient("MarcaAPI");

        StringContent content = new StringContent(
            JsonSerializer.Serialize(marcaViewModel),
                Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _marcaViewModel = await JsonSerializer
                    .DeserializeAsync<MarcaViewModel>(apiResponse, _options);
            }
            else
                return null;
        }
        return _marcaViewModel;
    }

    public async Task<MarcaViewModel> UpdateMarca(MarcaViewModel marcaViewModel, string token)
    {
        var client = _clientFactory.CreateClient("MarcaAPI");

        MarcaViewModel marca = new MarcaViewModel();

        using (var response = await client.PutAsJsonAsync(
            apiEndpoint, marcaViewModel))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                marca = await JsonSerializer
                    .DeserializeAsync<MarcaViewModel>(apiResponse, _options);
            }
            else
                return null;
        }

        return marca;
    }

    public async Task<bool> DeleteMarcaById(int id, string token)
    {
        var client = _clientFactory.CreateClient("MarcaAPI");
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
