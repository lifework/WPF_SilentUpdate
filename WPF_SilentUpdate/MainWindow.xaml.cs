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
            uint res = RelaunchHelper.RegisterApplicationRestart(null, RelaunchHelper.RestartFlags.NONE);
            await Task.Run(() => Thread.Sleep(65 * 1000));
            var x = 1;
            var y = 0;
            var z = x / y;
            System.Environment.Exit(1);
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

    // アプリケーションの再起動の登録
    // https://docs.microsoft.com/ja-jp/windows/win32/recovery/registering-for-application-restart
    internal class RelaunchHelper
    {
        #region Restart Manager Methods
        /// <summary>
        /// Registers the active instance of an application for restart.
        /// </summary>
        /// <param name="pwzCommandLine">
        /// A pointer to a Unicode string that specifies the command-line arguments for the application when it is restarted.
        /// The maximum size of the command line that you can specify is RESTART_MAX_CMD_LINE characters. Do not include the name of the executable
        /// in the command line; this function adds it for you.
        /// If this parameter is NULL or an empty string, the previously registered command line is removed. If the argument contains spaces,
        /// use quotes around the argument.
        /// </param>
        /// <param name="dwFlags">One of the options specified in RestartFlags</param>
        /// <returns>
        /// This function returns S_OK on success or one of the following error codes:
        /// E_FAIL for internal error.
        /// E_INVALIDARG if rhe specified command line is too long.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint RegisterApplicationRestart(string pwzCommandLine, RestartFlags dwFlags);
        #endregion Restart Manager Methods

        #region Restart Manager Enums
        /// <summary>
        /// Flags for the RegisterApplicationRestart function
        /// </summary>
        [Flags]
        internal enum RestartFlags
        {
            /// <summary>None of the options below.</summary>
            NONE = 0,

            /// <summary>Do not restart the process if it terminates due to an unhandled exception.</summary>
            RESTART_NO_CRASH = 1,
            /// <summary>Do not restart the process if it terminates due to the application not responding.</summary>
            RESTART_NO_HANG = 2,
            /// <summary>Do not restart the process if it terminates due to the installation of an update.</summary>
            RESTART_NO_PATCH = 4,
            /// <summary>Do not restart the process if the computer is restarted as the result of an update.</summary>
            RESTART_NO_REBOOT = 8
        }
        #endregion Restart Manager Enums

    }
}
