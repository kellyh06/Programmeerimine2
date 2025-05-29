using System.Windows;
using KooliProjekt.WpfApp;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new MainWindowViewModel();

            vm.OnError = msg =>
            {
                MessageBox.Show(msg.ToString(), "Viga", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            vm.ConfirmDelete = _ =>
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete selected item?",
                    "Delete list",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );
                return result == MessageBoxResult.Yes;
            };

            DataContext = vm;
            _ = vm.Load(); // Lae andmed akna avamisel
        }
    }
}
