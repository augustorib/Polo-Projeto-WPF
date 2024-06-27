using Polo_Projeto_WPF.Models;
using Polo_Projeto_WPF.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;


namespace Polo_Projeto_WPF.ViewModels
{
    public class ExpectativaMercadoMensalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly BacenApiClient _bacenApiClient;
        public ObservableCollection<string> IndicadoresSelect { get; }
        public ObservableCollection<ExpectativaMercadoMensal> Expectativas { get; set; }

        public ICommand BuscarExpectativas { get; set; }

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

           IndicadoresSelect = new ObservableCollection<string> { "IPCA", "Câmbio", "Selic" };
           
           BuscarExpectativas = new RelayCommand(ObterIndicadoresFiltroComData);

        }


        public async void ObterIndicadoresFiltroComData(object obj)
        {
            var expectativasMercadoMensal = await _bacenApiClient.ObterExpectativasMercadoMensal(CmbIndicador, DtpDataInicial, DtpDataFinal);

            Expectativas = new ObservableCollection<ExpectativaMercadoMensal>(expectativasMercadoMensal.Expectativas);

            NotifyPropertyChanged("Expectativas");

        }


        public void NotifyPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
