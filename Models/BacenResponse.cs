using System.Text.Json.Serialization;


namespace Polo_Projeto_WPF.Models
{
    public class BacenResponse
    {
        [JsonPropertyName("@odata.context")]
        public string OdataContext { get; set; }

        [JsonPropertyName("value")]
        public List<ExpectativaMercadoMensal> Expectativas { get; set; }
    }
}
