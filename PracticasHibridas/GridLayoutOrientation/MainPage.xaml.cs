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
                    //Grid.SetRow(buttonList, 1);
                    //Grid.SetColumn(buttonList, 0);
                    Grid.SetColumn(porc, 0);
                    Grid.SetRow(porc, 0);
                    this.porc.Width = 86;
                    this.porc.Height = 76;
                    Grid.SetColumn(root, 1);
                    Grid.SetRow(root, 0);
                    Grid.SetColumn(square, 2);
                    Grid.SetRow(square, 0);
                    Grid.SetColumn(byX, 3);
                    Grid.SetRow(byX, 0);
                    Grid.SetColumn(num7, 0);
                    Grid.SetColumn(num8, 1);
                    Grid.SetColumn(num9, 2);
                    Grid.SetColumn(num4, 0);
                    Grid.SetColumn(num5, 1);
                    Grid.SetColumn(num6, 2);
                    Grid.SetColumn(num1, 0);
                    Grid.SetColumn(num2, 1);
                    Grid.SetColumn(num3, 2);
                    Grid.SetColumn(num0, 1);
                    Grid.SetRowSpan(num0, 1);
                    Grid.SetColumn(dot, 2);
                    Grid.SetColumn(CE, 0);
                    Grid.SetRow(CE, 1);
                    this.CE.Width = 86;
                    this.CE.Height = 76;
                    Grid.SetColumn(C, 1);
                    Grid.SetRow(C, 1);
                    Grid.SetColumn(DEL, 2);
                    Grid.SetRow(DEL, 1);
                    Grid.SetColumn(neg, 0);
                    Grid.SetRow(neg, 5);
                    Grid.SetColumn(equals, 3);
                    Grid.SetRow(equals, 5);
                    this.equals.Width = Double.NaN;
                    this.equals.Height = Double.NaN;
                    Grid.SetColumn(div, 3);
                    Grid.SetRow(div, 1);
                    Grid.SetColumn(mult, 3);
                    Grid.SetRow(mult, 2);
                    Grid.SetColumn(minus, 3);
                    Grid.SetRow(minus, 3);
                    Grid.SetColumn(plus, 3);
                    Grid.SetRow(plus, 4);
                    this.plus.Width = Double.NaN;
                    this.plus.Height = Double.NaN;
                }
                // If not in portrait, move buttonList content to visible row and column.
                else
                {
                    //Grid.SetRow(buttonList, 0);
                    //Grid.SetColumn(buttonList, 1);
                    Grid.SetColumn(porc, 0);
                    Grid.SetRow(porc, 5);
                    this.porc.Width = Double.NaN;
                    this.porc.Height = Double.NaN;
                    Grid.SetColumn(root, 0);
                    Grid.SetRow(root, 4);
                    Grid.SetColumn(square, 0);
                    Grid.SetRow(square, 3);
                    Grid.SetColumn(byX, 0);
                    Grid.SetRow(byX, 2);
                    Grid.SetColumn(num7, 1);
                    Grid.SetColumn(num8, 2);
                    Grid.SetColumn(num9, 3);
                    Grid.SetColumn(num4, 1);
                    Grid.SetColumn(num5, 2);
                    Grid.SetColumn(num6, 3);
                    Grid.SetColumn(num1, 1);
                    Grid.SetColumn(num2, 2);
                    Grid.SetColumn(num3, 3);
                    Grid.SetColumn(num0, 2);
                    Grid.SetRowSpan(num0, 2);
                    Grid.SetColumn(dot, 3);
                    Grid.SetColumn(C, 5);
                    Grid.SetRow(C, 2);
                    Grid.SetColumn(CE, 5);
                    Grid.SetRow(CE, 3);
                    this.CE.Width = Double.NaN;
                    this.CE.Height = Double.NaN;
                    Grid.SetColumn(DEL, 5);
                    Grid.SetRow(DEL, 4);
                    Grid.SetColumn(neg, 1);
                    Grid.SetRow(neg, 5);
                    Grid.SetColumn(equals, 5);
                    Grid.SetRow(equals, 5);
                    this.equals.Width = 95;
                    this.equals.Height = 50;
                    Grid.SetColumn(div, 4);
                    Grid.SetRow(div, 2);
                    Grid.SetColumn(mult, 4);
                    Grid.SetRow(mult, 3);
                    Grid.SetColumn(minus, 4);
                    Grid.SetRow(minus, 4);
                    Grid.SetColumn(plus, 4);
                    Grid.SetRow(plus, 5);
                    this.plus.Width = 95;
                    this.plus.Height = 50;
                }

               
            });
        }
    }
}
