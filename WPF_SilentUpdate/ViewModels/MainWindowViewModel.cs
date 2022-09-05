using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_SilentUpdate.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            UpdateUptimeAsync();
        }

        public int ProcessId => Environment.ProcessId;
        public Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
        public string AssemblyLocation => ExecutingAssembly.Location;
        public string AssemblyVersion => ExecutingAssembly.GetName().Version?.ToString() ?? "Unknown";

        public ReactivePropertySlim<int> UptimeSeconds { get; set; } = new(0);

        public void UpdateUptimeAsync()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    UptimeSeconds.Value = Convert.ToInt32(DateTime.Now.Subtract(Process.GetCurrentProcess().StartTime).TotalSeconds);
                    Thread.Sleep(1 * 1000);
                }
            });
        }
    }
}
