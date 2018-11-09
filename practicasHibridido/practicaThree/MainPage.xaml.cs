using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace practicaThree
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void _btnReadFile_Click(object sender, RoutedEventArgs e)
        {
            int _totalLinesFile = File.ReadLines(@"Assets/data.txt").Count();
            StringBuilder _contenidoFile = new StringBuilder(); // Es mejor en rendimiento que un simple String
            int _numLineasProcesadas = 1;
            _btnReadFile.IsEnabled = false; // Deshabilitamos boton

            // https://docs.microsoft.com/en-us/uwp/api/windows.storage.storagefile.getfilefromapplicationuriasync
            StorageFile _file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/data.txt"));
            
            // https://social.msdn.microsoft.com/Forums/sqlserver/en-US/add089a4-a67e-4b98-91af-67d23189d5d4/uwp-how-to-read-a-text-file?forum=wpdevelop
            StreamReader _readFile = new StreamReader(await _file.OpenStreamForReadAsync());

            Windows.UI.Core.DispatchedHandler _actionReadFile = async () =>
            {
                while (_numLineasProcesadas <= _totalLinesFile)
                {
                    _txtPorcentaje.Text = Math.Round((_numLineasProcesadas * 100.0) / _totalLinesFile).ToString()  + " %";
                    _proArchivo.Value = (_numLineasProcesadas * 100) / _totalLinesFile;
                    _contenidoFile.AppendLine(_readFile.ReadLine().ToString() + "\n");
                    _numLineasProcesadas++;
                    await Task.Delay(10);
                }
                _btnReadFile.IsEnabled = true; // Habilitamos Boton
            };
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, _actionReadFile);
        }
    }
}
