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
                    this._btnMC.Width = 86;
                    this._btnMR.Width = 86;
                    this._btnMMas.Width = 86;
                    this._btnMMenos.Width = 86;
                    this._btnMS.Width = 86;
                    this._btnSin.Width = 0;
                    this._btnCos.Width = 0;
                    this._btnTan.Width = 0;
                    this._btnLog.Width = 0;
                    this._btnIn.Width = 0;
                    Grid.SetColumn(_btnPorc, 0);
                    Grid.SetRow(_btnPorc, 0);
                    this._btnPorc.Width = 86;
                    this._btnPorc.Height = 76;
                    Grid.SetColumn(_btnRoot, 1);
                    Grid.SetRow(_btnRoot, 0);
                    Grid.SetColumn(_btnSquare, 2);
                    Grid.SetRow(_btnSquare, 0);
                    Grid.SetColumn(_btnByX, 3);
                    Grid.SetRow(_btnByX, 0);
                    Grid.SetColumn(_numSeven, 0);
                    Grid.SetColumn(_numEight, 1);
                    Grid.SetColumn(_numNine, 2);
                    Grid.SetColumn(_numFour, 0);
                    Grid.SetColumn(_numFive, 1);
                    Grid.SetColumn(_numSix, 2);
                    Grid.SetColumn(_numOne, 0);
                    Grid.SetColumn(_numTwo, 1);
                    Grid.SetColumn(_numThree, 2);
                    Grid.SetColumn(_numZero, 1);
                    Grid.SetRowSpan(_numZero, 1);
                    Grid.SetColumn(_btnDot, 2);
                    Grid.SetColumn(_btnCE, 0);
                    Grid.SetRow(_btnCE, 1);
                    this._btnCE.Width = 86;
                    this._btnCE.Height = 76;
                    Grid.SetColumn(_btnC, 1);
                    Grid.SetRow(_btnC, 1);
                    Grid.SetColumn(_btnDEL, 2);
                    Grid.SetRow(_btnDEL, 1);
                    Grid.SetColumn(_btnNeg, 0);
                    Grid.SetRow(_btnNeg, 5);
                    Grid.SetColumn(_btnEquals, 3);
                    Grid.SetRow(_btnEquals, 5);
                    this._btnEquals.Width = Double.NaN;
                    this._btnEquals.Height = Double.NaN;
                    Grid.SetColumn(_btnDiv, 3);
                    Grid.SetRow(_btnDiv, 1);
                    Grid.SetColumn(_btnMult, 3);
                    Grid.SetRow(_btnMult, 2);
                    Grid.SetColumn(_btnMinus, 3);
                    Grid.SetRow(_btnMinus, 3);
                    Grid.SetColumn(_btnPlus, 3);
                    Grid.SetRow(_btnPlus, 4);
                    this._btnPlus.Width = Double.NaN;
                    this._btnPlus.Height = Double.NaN;
                }
                // If not in portrait, move buttonList content to visible row and column.
                else
                {
                    //Grid.SetRow(buttonList, 0);
                    //Grid.SetColumn(buttonList, 1);
                    this._btnMC.Width = 0;
                    this._btnMR.Width = 0;
                    this._btnMMas.Width = 0;
                    this._btnMMenos.Width = 0;
                    this._btnMS.Width = 0;
                    this._btnSin.Width = 86;
                    this._btnCos.Width = 86;
                    this._btnTan.Width = 86;
                    this._btnLog.Width = 86;
                    this._btnIn.Width = 86;
                    Grid.SetColumn(_btnPorc, 0);
                    Grid.SetRow(_btnPorc, 5);
                    this._btnPorc.Width = Double.NaN;
                    this._btnPorc.Height = Double.NaN;
                    Grid.SetColumn(_btnRoot, 0);
                    Grid.SetRow(_btnRoot, 4);
                    Grid.SetColumn(_btnSquare, 0);
                    Grid.SetRow(_btnSquare, 3);
                    Grid.SetColumn(_btnByX, 0);
                    Grid.SetRow(_btnByX, 2);
                    Grid.SetColumn(_numSeven, 1);
                    Grid.SetColumn(_numEight, 2);
                    Grid.SetColumn(_numNine, 3);
                    Grid.SetColumn(_numFour, 1);
                    Grid.SetColumn(_numFive, 2);
                    Grid.SetColumn(_numSix, 3);
                    Grid.SetColumn(_numOne, 1);
                    Grid.SetColumn(_numTwo, 2);
                    Grid.SetColumn(_numThree, 3);
                    Grid.SetColumn(_numZero, 2);
                    Grid.SetRowSpan(_numZero, 2);
                    Grid.SetColumn(_btnDot, 3);
                    Grid.SetColumn(_btnC, 5);
                    Grid.SetRow(_btnC, 2);
                    Grid.SetColumn(_btnCE, 5);
                    Grid.SetRow(_btnCE, 3);
                    this._btnCE.Width = Double.NaN;
                    this._btnCE.Height = Double.NaN;
                    Grid.SetColumn(_btnDEL, 5);
                    Grid.SetRow(_btnDEL, 4);
                    Grid.SetColumn(_btnNeg, 1);
                    Grid.SetRow(_btnNeg, 5);
                    Grid.SetColumn(_btnEquals, 5);
                    Grid.SetRow(_btnEquals, 5);
                    this._btnEquals.Width = Double.NaN;
                    this._btnEquals.Height = Double.NaN;
                    Grid.SetColumn(_btnDiv, 4);
                    Grid.SetRow(_btnDiv, 2);
                    Grid.SetColumn(_btnMult, 4);
                    Grid.SetRow(_btnMult, 3);
                    Grid.SetColumn(_btnMinus, 4);
                    Grid.SetRow(_btnMinus, 4);
                    Grid.SetColumn(_btnPlus, 4);
                    Grid.SetRow(_btnPlus, 5);
                    this._btnPlus.Width = 95;
                    this._btnPlus.Height = Double.NaN;
                }
            });
        }
    }
}
