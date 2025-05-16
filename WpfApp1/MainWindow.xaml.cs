﻿using KooliProjekt.WpfApp;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Api;

namespace WpfApp1;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        var viewModel = new MainWindowViewModel();

        viewModel.ConfirmDelete = _ =>
        {
            var result = MessageBox.Show(
                            "Are you sure you want to delete selected item?",
                            "Delete list",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Stop
                            );
            return (result == MessageBoxResult.Yes);
        };

        viewModel.OnError = (error) =>
        {
            MessageBox.Show(
                    (string)error,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
        };

        DataContext = viewModel;
        await viewModel.Load();
    }

}

