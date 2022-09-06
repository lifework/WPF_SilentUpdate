using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using WPF_SilentUpdate.Views;
using WPF_SilentUpdate.ViewModels;
using System.Diagnostics;

namespace WPF_SilentUpdate
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            uint hresult = RelaunchHelper.RegisterApplicationRestart();
            Console.WriteLine($"RelaunchHelper.RegisterApplicationRestart: {hresult}");

            StoreManager.CheckUpdateAndInstall();
        }
    }
}
