using CsvHelper;
using Polo_Projeto_WPF.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;

namespace Polo_Projeto_WPF.Services
{
    public class CsvExport 
    {
        public async Task ExportarParaCSV(ObservableCollection<ExpectativaMercadoMensal> expectativas)
        {
            var salvarArquivo = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = ".csv",
                FileName = "Expectativas-de-Mercado-" + expectativas[0].Indicador
            };

            if (salvarArquivo.ShowDialog() == true)
            {
                var filePath = salvarArquivo.FileName;

                var data = expectativas;

                if (data != null)
                {
                    using (var writer = new StreamWriter(filePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {

                        csv.WriteRecords(data);
                    }

                    MessageBox.Show("CSV Exportato com sucesso!");
                }
                else
                {
                    MessageBox.Show("Erro! Dados não podem ser exportados.");
                }
            }
        }
    }
}
