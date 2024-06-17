using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;

namespace FatecSisMed.Web.Services.Entities;

public class MedicoService : IMedicoService
{

    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/medico/";
    private MedicoViewModel _medicoViewModel;
    private IEnumerable<MedicoViewModel> medicos;

    public MedicoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions
        { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<MedicoViewModel>> GetAllMedicos(string token)
    {
        var client = _clientFactory.CreateClient("MedicoAPI");
        PutTokenInHeaderAutorization(token, client);

        var response = await client.GetAsync(apiEndpoint);

        if (response.IsSuccessStatusCode)
        {
            var apiResponse = await response.Content.ReadAsStreamAsync();
            medicos = await JsonSerializer
                .DeserializeAsync<IEnumerable<MedicoViewModel>>(apiResponse, _options);

        }
        else
            return null;

        return medicos;
    }

    public async Task<MedicoViewModel> FindMedicoById(int id, string token)
    {
        var client = _clientFactory.CreateClient("MedicoAPI");
        
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _medicoViewModel = await JsonSerializer
                    .DeserializeAsync<MedicoViewModel>(apiResponse, _options);
            }
            else
                return null;
        }
        return _medicoViewModel;
    }

    public async Task<MedicoViewModel> CreateMedico(MedicoViewModel medicoViewModel, string token)
    {
        var client = _clientFactory.CreateClient("MedicoAPI");
        
        StringContent content = new StringContent(
            JsonSerializer.Serialize(medicoViewModel),
                Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _medicoViewModel = await JsonSerializer
                    .DeserializeAsync<MedicoViewModel>(apiResponse, _options);
            }
            else
                return null;
        }
        return _medicoViewModel;
    }

    public async Task<MedicoViewModel> UpdateMedico(MedicoViewModel medicoViewModel, string token)
    {
        var client = _clientFactory.CreateClient("MedicoAPI");

        MedicoViewModel medico = new MedicoViewModel();

        using (var response = await client.PutAsJsonAsync(
            apiEndpoint, medicoViewModel))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                medico = await JsonSerializer
                    .DeserializeAsync<MedicoViewModel>(apiResponse, _options);
            }
            else
                return null;
        }

        return medico;
    }

    public async Task<bool> DeleteMedicoById(int id, string token)
    {
        var client = _clientFactory.CreateClient("MedicoAPI");
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

