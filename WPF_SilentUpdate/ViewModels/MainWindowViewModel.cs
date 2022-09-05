using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SilentUpdate.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public int ProcessId => Environment.ProcessId;
        public Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();
        public string AssemblyLocation => ExecutingAssembly.Location;
        public string AssemblyVersion => ExecutingAssembly.GetName().Version?.ToString() ?? "Unknown";
    }
}
