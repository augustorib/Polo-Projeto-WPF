using Microsoft.Data.SqlClient;
using Polo_Projeto_WPF.Models;
using System.Collections.ObjectModel;


namespace Polo_Projeto_WPF.Data
{
    public class PoloDbContext
    {
        private readonly string stringConexao = "Server=.;Database=WPF-Polo;Trusted_Connection=True;TrustServerCertificate=True";


        public async Task SalvarExpectativasMercado(ObservableCollection<ExpectativaMercadoMensal> expectativas)
        {

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                await con.OpenAsync();


                var query = "Insert Into ExpectativaMercadoMensal (Indicador, Data, DataReferencia, Media, Mediana, DesvioPadrao, Minimo, Maximo, NumeroRespondentes, BaseCalculo) Values " +
                     "(@Indicador, @Data, @DataReferencia, @Media, @Mediana, @DesvioPadrao, @Minimo, @Maximo, @NumeroRespondentes, @BaseCalculo)";

                foreach (var item in expectativas)
                {

                    using (SqlCommand command = new SqlCommand(query, con))
                    {

                        command.Parameters.AddWithValue("@Indicador", item.Indicador);
                        command.Parameters.AddWithValue("@Data", item.Data.ToDateTime(TimeOnly.MinValue));
                        command.Parameters.AddWithValue("@DataReferencia", item.DataReferencia);
                        command.Parameters.AddWithValue("@Media", item.Media);
                        command.Parameters.AddWithValue("@Mediana", item.Mediana);
                        command.Parameters.AddWithValue("@DesvioPadrao", item.DesvioPadrao);
                        command.Parameters.AddWithValue("@Minimo", item.Minimo);
                        command.Parameters.AddWithValue("@Maximo", item.Maximo);
                        command.Parameters.AddWithValue("@NumeroRespondentes", item.NumeroRespondentes);
                        command.Parameters.AddWithValue("@BaseCalculo", item.BaseCalculo);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
