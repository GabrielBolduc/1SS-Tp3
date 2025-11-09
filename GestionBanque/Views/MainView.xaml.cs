using GestionBanque.ViewModels; // Assurez-vous d'avoir ce 'using'
using System.Windows;

namespace GestionBanque.Views
{
    public partial class MainView : Window
    {
        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();
            // assigne DataContext au ViewModel inject par Autofac
            DataContext = viewModel;
        }
    }
}