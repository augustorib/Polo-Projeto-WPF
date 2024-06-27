using Polo_Projeto_WPF.Views;
using System.Windows;


namespace Polo_Projeto_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            main.Content = new FiltrarExpectativaPage();
        }
    }
}