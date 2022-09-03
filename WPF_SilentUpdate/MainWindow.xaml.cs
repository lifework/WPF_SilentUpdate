using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_SilentUpdate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        public async void OnRestartButtonClicked(object sender, RoutedEventArgs e)
        {
            RelaunchHelper.RegisterApplicationRestart();
            await Task.Run(() => Thread.Sleep(65 * 1000));
            System.Environment.Exit(1);
        }

        public async void OnUpdateApplicationButtonClicked(object sender, RoutedEventArgs e)
        {
            RelaunchHelper.RegisterApplicationRestart();
            await StoreManager.SilentDownloadAndInstallUpdatesAsync();
        }
    }

    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public int ProcessId => Environment.ProcessId;
        public Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
        public string AssemblyLocation => ExecutingAssembly.Location;
        public string AssemblyVersion => ExecutingAssembly.GetName().Version?.ToString() ?? "Unknown";
    }
}
