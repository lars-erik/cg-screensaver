using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using WpfScreenHelper;

namespace CG.ScreenSaver.WinWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var args = e.Args;

            try
            {
                Execute(args);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }

        }

        private void Execute(string[] args)
        {
            if (args.Length == 0 || args[0].ToLower().StartsWith("/s"))
            {
                foreach (var screen in Screen.AllScreens)
                {
                    var win = new MainWindow
                    {
                        Left = screen.WorkingArea.Left,
                        Top = screen.WorkingArea.Top,
                        Width = screen.WorkingArea.Width,
                        Height = screen.WorkingArea.Height,
                        WindowStyle = WindowStyle.None
                    };
                    win.Show();
                    win.WindowState = WindowState.Maximized;
                }
            }
            else if (args[0].ToLower().StartsWith("/c"))
            {
                MessageBox.Show("CG Screensaver! 🥳");
                Shutdown();
            }
            else if (args[0].ToLower().StartsWith("/p"))
            {
                // Doesn't die and has wrong size. :/

                Shutdown();
                return;

                if (int.TryParse(args[1], out var hwnd))
                {
                    var win = new MainWindow
                    {
                        WindowStyle = WindowStyle.None,
                    };
                    var helper = new WindowInteropHelper(win)
                    {
                        Owner = new IntPtr(hwnd)
                    };
                    helper.EnsureHandle();
                    win.Show();
                }
                else
                {
                    MessageBox.Show("Invalid hwnd " + args.Length);
                }
            }
            else
            {
                MessageBox.Show("Something completely different happened. :O\r\n" + args.Length + " arguments, starting with " + args[0], "Unknown event", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Shutdown();
            }
        }
    }
}
