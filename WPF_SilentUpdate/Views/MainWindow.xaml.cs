using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPF_SilentUpdate.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // メッセージループをフックするメソッドを登録
            var hWnd = new WindowInteropHelper(this).EnsureHandle();
            HwndSource source = HwndSource.FromHwnd(hWnd);
            source.AddHook(new HwndSourceHook(WndProc));
        }

        public const int WM_QUERYENDSESSION = 0x11;
        public const int WM_ENDSESSION = 0x16;

        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_QUERYENDSESSION:
                    Console.WriteLine("WM_QUERYENDSESSION");
                    break;
                case WM_ENDSESSION:
                    Console.WriteLine("WM_ENDSESSION");
                    break;
                default:
                    break;
            }
 
            return IntPtr.Zero;
        }
    }
}
