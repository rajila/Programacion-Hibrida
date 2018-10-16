using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Sensors;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace GridLayoutOrientation
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SimpleOrientationSensor _sensor;

        public MainPage()
        {
            this.InitializeComponent();
            _sensor = SimpleOrientationSensor.GetDefault();
            _sensor.OrientationChanged += Sensor_OrientationChanged;
        }
        
        private async void Sensor_OrientationChanged(SimpleOrientationSensor sender, SimpleOrientationSensorOrientationChangedEventArgs args)
        {
            SimpleOrientation orientation = args.Orientation;
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // Switch the placement of the buttons based on an orientation change.
                if (SimpleOrientation.NotRotated == orientation)
                {
                    Grid.SetRow(buttonList, 1);
                    Grid.SetColumn(buttonList, 0);
                }
                // If not in portrait, move buttonList content to visible row and column.
                else
                {
                    Grid.SetRow(buttonList, 0);
                    Grid.SetColumn(buttonList, 1);
                }

               
            });
        }
    }
}
