using Polo_Projeto_WPF.ViewModels;
using System.Windows.Controls;


namespace Polo_Projeto_WPF.Views
{
    /// <summary>
    /// Interação lógica para FiltrarExpectativaPage.xam
    /// </summary>
    public partial class FiltrarExpectativaPage : Page
    {
        public FiltrarExpectativaPage()
        {
            InitializeComponent();
            DataContext = new ExpectativaMercadoMensalViewModel();
        }
    }
}
