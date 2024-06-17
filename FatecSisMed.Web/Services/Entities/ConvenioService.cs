using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;

namespace FatecSisMed.Web.Services.Entities
{
    public class ConvenioService : IConvenioService
    {

        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;

        public ConvenioService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
        }

        private const string apiEndpoint = "/api/convenio/";
        private ConvenioViewModel _convenioViewModel;
        private IEnumerable<ConvenioViewModel> convenios;

        public async Task<IEnumerable<ConvenioViewModel>> GetAllConvenios(string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");
            PutTokenInHeaderAutorization(token, client);

            var response = await client.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                convenios = await JsonSerializer
                    .DeserializeAsync<IEnumerable
                    <ConvenioViewModel>>(apiResponse, _options);
            }
            else
                return null;

            return convenios;
        }

        public async Task<ConvenioViewModel> FindConvenioById(int id, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");

            using(var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _convenioViewModel = await JsonSerializer
                        .DeserializeAsync<ConvenioViewModel>
                        (apiResponse, _options);
                }
                else
                    return null;
            }
            return _convenioViewModel;
        }

        public async Task<ConvenioViewModel> CreateConvenio
            (ConvenioViewModel convenioViewModel, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");

            StringContent content =
                new StringContent(JsonSerializer.Serialize(convenioViewModel),
                Encoding.UTF8, "application/json");

            using(var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _convenioViewModel = await JsonSerializer
                        .DeserializeAsync<ConvenioViewModel>(apiResponse, _options);
                }
                else
                    return null;
            }
            return _convenioViewModel;
        }

        public async Task<ConvenioViewModel> UpdateConvenio(ConvenioViewModel convenioViewModel, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");

            ConvenioViewModel convenio = new ConvenioViewModel();

            using (var response = await client.PutAsJsonAsync(apiEndpoint, convenioViewModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    convenio = await JsonSerializer
                        .DeserializeAsync<ConvenioViewModel>(apiResponse, _options);
                }
                else
                    return null;
            }
            return convenio;
        }

        public async Task<bool> DeleteConvenioById(int id, string token)
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
}

