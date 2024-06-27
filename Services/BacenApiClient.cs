
using Polo_Projeto_WPF.Models;
using System.Net.Http;
using System.Text.Json;

namespace Polo_Projeto_WPF.Services
{
    public class BacenApiClient
    {
        private readonly HttpClient _httpClient;

        public BacenApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<BacenResponse> ObterExpectativasMercadoMensal(string cmbIndicador, DateTime dataInicial, DateTime dataFinal)
        {

            var dataInicialString = dataInicial.ToString("yyyy-MM-dd");
            var dataFinalString = dataFinal.ToString("yyyy-MM-dd");

            var apiUrlFormatada = $"https://olinda.bcb.gov.br/olinda/servico/Expectativas/versao/v1/odata/ExpectativaMercadoMensais?%24format=json&%24orderby=Data%20asc&%24filter=Indicador%20eq%20%27{cmbIndicador}%27%20and%20Data%20ge%20%27{dataInicialString}%27%20and%20Data%20le%20%27{dataFinalString}%27";

            var response = await _httpClient.GetAsync(apiUrlFormatada);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<BacenResponse>(jsonResponse);

            return result;

        }
    }
}
