using System;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FatecSisMed.Web.Models;
using FatecSisMed.Web.Services.Interfaces;

namespace FatecSisMed.Web.Services.Entities
{
    public class EspecialidadeService : IEspecialidadeService
    {

        private readonly IHttpClientFactory _clientFactory;

        public EspecialidadeService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions
            { PropertyNameCaseInsensitive = true };
        }

        private readonly JsonSerializerOptions _options;
        private const string apiEndpoint = "/api/especialidade/";
        private EspecialidadeViewModel _especialidadeViewModel;
        private IEnumerable<EspecialidadeViewModel> especialidades;

        public async Task<IEnumerable<EspecialidadeViewModel>> GetAllEspecialidades(string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");
            PutTokenInHeaderAutorization(token, client);

            var response = await client.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                especialidades = await JsonSerializer
                    .DeserializeAsync<IEnumerable
                    <EspecialidadeViewModel>>(apiResponse, _options);
            }
            else
                return Enumerable.Empty<EspecialidadeViewModel>();

            return especialidades;
        }

        public async Task<EspecialidadeViewModel> FindEspecialidadeById(int id, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");

            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode && response.Content is not null)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _especialidadeViewModel = await JsonSerializer
                        .DeserializeAsync<EspecialidadeViewModel>(apiResponse, _options);
                }
                else
                    return null;
            }
            return _especialidadeViewModel;
        }

        public async Task<EspecialidadeViewModel>
            CreateEspecialidade(EspecialidadeViewModel especialidadeViewModel, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");

            StringContent content =
                new StringContent(JsonSerializer.Serialize(especialidadeViewModel),
                Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _especialidadeViewModel = await JsonSerializer
                        .DeserializeAsync<EspecialidadeViewModel>(apiResponse, _options);
                }
                else
                    return null;
            }
            return _especialidadeViewModel;
        }

        public async Task<EspecialidadeViewModel>
            UpdateEspecialidade(EspecialidadeViewModel especialidadeViewModel, string token)
        {
            var client = _clientFactory.CreateClient("MedicoAPI");

            EspecialidadeViewModel especialidade = new EspecialidadeViewModel();

            using (var response = await
                client.PutAsJsonAsync(apiEndpoint, especialidadeViewModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    especialidade = await JsonSerializer
                        .DeserializeAsync<EspecialidadeViewModel>(apiResponse, _options);
                }
                else
                    return null;
            }
            return especialidade;
        }

        public async Task<bool> DeleteEspecialidadeById(int id, string token)
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

