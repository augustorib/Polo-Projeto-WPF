using Polo_Projeto_WPF.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Polo_Projeto_WPF.ViewModels
{
    public class ExpectativaMercadoMensalViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<string> IndicadoresSelect { get; }
        public ObservableCollection<ExpectativaMercadoMensal> Expectativa { get; set; }

        
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

           IndicadoresSelect = new ObservableCollection<string> { "IPCA", "Câmbio", "Selic" };

        }


        public void NotifyPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
