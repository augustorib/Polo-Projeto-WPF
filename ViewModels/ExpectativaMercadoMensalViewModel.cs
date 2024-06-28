using Polo_Projeto_WPF.Models;
using Polo_Projeto_WPF.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Polo_Projeto_WPF.Data;


namespace Polo_Projeto_WPF.ViewModels
{
    public class ExpectativaMercadoMensalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly BacenApiClient _bacenApiClient;

        private readonly CsvExport _csvExport;

        private readonly PoloDbContext _context;
        public ObservableCollection<string> IndicadoresSelect { get; }
        public ObservableCollection<ExpectativaMercadoMensal> Expectativas { get; set; }

        public ICollectionView ExpectativasAgrupado { get; set; }
        public PlotModel GraficoLinear { get; private set; }

        public ICommand BuscarExpectativas { get; set; }
        public ICommand ExportarCsv { get; set; }
        public ICommand SalvarNoBd { get; set; }

        private string _cmbIndicador;
        public string CmbIndicador
        {
            get { return _cmbIndicador; }
            set
            {
                _cmbIndicador = value;
                NotifyPropertyChanged(CmbIndicador);
            }
        }


        private DateTime _dtpDataInicial = DateTime.Now;

        public DateTime DtpDataInicial
        {
            get { return _dtpDataInicial; }
            set
            {
                _dtpDataInicial = value;
                NotifyPropertyChanged(DtpDataInicial.ToString());
            }
        }

        private DateTime _dtpDataFinal = DateTime.Now;

        public DateTime DtpDataFinal
        {
            get { return _dtpDataFinal; }
            set
            {
                _dtpDataFinal = value;
                NotifyPropertyChanged(_dtpDataFinal.ToString());
            }
        }

        public ExpectativaMercadoMensalViewModel()
        {

           _bacenApiClient = new BacenApiClient();
           _csvExport = new CsvExport();
           _context = new PoloDbContext();

           IndicadoresSelect = new ObservableCollection<string> { "IPCA", "Câmbio", "Selic" };
           
           BuscarExpectativas = new RelayCommand(ObterIndicadoresFiltroComData);
           ExportarCsv = new RelayCommand(ExportarParaCSV);
           SalvarNoBd = new RelayCommand(SalvarExpectativasMercado);

        }


        public async void ObterIndicadoresFiltroComData(object obj)
        {
            var expectativasMercadoMensal = await _bacenApiClient.ObterExpectativasMercadoMensal(CmbIndicador, DtpDataInicial, DtpDataFinal);

            Expectativas = new ObservableCollection<ExpectativaMercadoMensal>(expectativasMercadoMensal.Expectativas);

            //Agrupamento da tabela
            ExpectativasAgrupado = CollectionViewSource.GetDefaultView(Expectativas);
            ExpectativasAgrupado.GroupDescriptions.Add(new PropertyGroupDescription("Indicador"));

            ConstruirGraficoLinear();

            NotifyPropertyChanged("Expectativas");
            NotifyPropertyChanged("ExpectativasAgrupado");

        }

        public async void ExportarParaCSV(object obj)
        {
            await _csvExport.ExportarParaCSV(Expectativas);
        } 

        public async void SalvarExpectativasMercado(object obj)
        {
            await _context.SalvarExpectativasMercado(Expectativas);
        }

        private void ConstruirGraficoLinear()
        {
            GraficoLinear = new PlotModel { Title = "Gráfico Linear " + Expectativas[0].Indicador };

            // Criação dos eixos
            var X = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Mínimo"
            };

            var Y = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Máximo"
            };

            GraficoLinear.Axes.Add(X);
            GraficoLinear.Axes.Add(Y);

            // Criação da série de dados
            var lineSeries = new LineSeries
            {
                Title = "Mínimo x Máximo ao Longo do Tempo",
                MarkerType = MarkerType.Circle

            };

            // Dados para o gráfico
            var data = Expectativas;

            // Adicionando pontos à série
            foreach (var item in data)
            {
                lineSeries.Points.Add(new DataPoint(item.Minimo, item.Maximo));
            }

            GraficoLinear.Series.Add(lineSeries);
            NotifyPropertyChanged("GraficoLinear");
        }

        public void NotifyPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
