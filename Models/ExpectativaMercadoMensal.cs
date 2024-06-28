

using System.Text.Json.Serialization;

namespace Polo_Projeto_WPF.Models
{
    public class ExpectativaMercadoMensal
    {
        public string Indicador { get; set; }

        public DateOnly Data { get; set; }

        public string DataReferencia { get; set; }

        public double Media { get; set; }

        public double Mediana { get; set; }
        public double DesvioPadrao { get; set; }

        public double Minimo { get; set; }

        public double Maximo { get; set; }

        [JsonPropertyName("numeroRespondentes")]
        public int NumeroRespondentes { get; set; }

        [JsonPropertyName("baseCalculo")]
        public int BaseCalculo { get; set; }
    }
}
