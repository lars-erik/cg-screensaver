using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

namespace CG.ScreenSaver.WinWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.None;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var stream = GetType().Assembly.GetManifestResourceStream("CG.ScreenSaver.WinWpf.embedcontainer.html");
            await Browser.EnsureCoreWebView2Async();
            Browser.NavigateToString(await new StreamReader(stream!).ReadToEndAsync());
        }
    }
}
