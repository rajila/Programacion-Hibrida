using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using System.Net.Http;
using Windows.Data.Json;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml.Controls.Maps;
using System.Diagnostics;
using Windows.Storage.Streams;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace appBingMaps
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public string _urlIni = "http://dev.virtualearth.net/REST/V1/Routes/Driving?wp.0=";
        public string _urlFin = "&wp.1=";
        public string _apiKey = "&key=pLBMDCehHf2wiv5WW6Qf~VfeFgVLIna9jeSX-XkSSXQ~AvePmOwVm79f4WMjlKa0p-Eedlai4MSQ-NeK5lIO7RuCGRh_zCiZPxyIQTUxKxdl";
        public static Uri _directionURI;
        public string _dirOrigen = "";
        public string _dirDestino = "";
        public string _urlBuscar = "";

        Geolocator GPS = new Geolocator { ReportInterval = 2000 };

        public MainPage()
        {
            this.InitializeComponent();
            mcMapa.MapServiceToken = "pLBMDCehHf2wiv5WW6Qf~VfeFgVLIna9jeSX-XkSSXQ~AvePmOwVm79f4WMjlKa0p-Eedlai4MSQ-NeK5lIO7RuCGRh_zCiZPxyIQTUxKxdl";

            GPS.DesiredAccuracy = PositionAccuracy.High;
            GPS.DesiredAccuracy = PositionAccuracy.High;
            GPS.MovementThreshold = 10;
            GPS.PositionChanged += GPS_PositionChanged;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            buscarRuta("&avoid=minimizeTolls&output=json", Colors.Green);
        }

        private void buscarRuta(String modo, Color color)
        {
            if (txtOrigen.Text.Trim() != "" && txtDestino.Text.Trim() != "")
            {
                mcMapa.Routes.Clear();
                _dirOrigen = txtOrigen.Text;
                _dirDestino = txtDestino.Text;
                _urlBuscar = _urlIni + _dirOrigen + _urlFin + _dirDestino + modo + _apiKey;
                callApi(color);
            }
        }

        private async void callApi(Color color)
        {
            var _httpClient = new HttpClient();
            _directionURI = new Uri(_urlBuscar);

            String _jsonString = await _httpClient.GetStringAsync(_directionURI);
            Debug.WriteLine(_jsonString);
            JsonValue _jsonValue = JsonValue.Parse(_jsonString);

            String _distanceUnit = _jsonValue.GetObject().GetNamedArray("resourceSets").GetObjectAt(0).GetNamedArray("resources").GetObjectAt(0).GetNamedString("distanceUnit");
            String _durationUnit = _jsonValue.GetObject().GetNamedArray("resourceSets").GetObjectAt(0).GetNamedArray("resources").GetObjectAt(0).GetNamedString("durationUnit");
            double _travelDistance = _jsonValue.GetObject().GetNamedArray("resourceSets").GetObjectAt(0).GetNamedArray("resources").GetObjectAt(0).GetNamedNumber("travelDistance");
            double _travelDuration = _jsonValue.GetObject().GetNamedArray("resourceSets").GetObjectAt(0).GetNamedArray("resources").GetObjectAt(0).GetNamedNumber("travelDuration");

            double _latStart = _jsonValue.GetObject().GetNamedArray("resourceSets").GetObjectAt(0).GetNamedArray("resources").GetObjectAt(0).GetNamedArray("routeLegs").GetObjectAt(0).GetNamedObject("actualStart").GetNamedArray("coordinates").GetNumberAt(0);
            double _lonStart = _jsonValue.GetObject().GetNamedArray("resourceSets").GetObjectAt(0).GetNamedArray("resources").GetObjectAt(0).GetNamedArray("routeLegs").GetObjectAt(0).GetNamedObject("actualStart").GetNamedArray("coordinates").GetNumberAt(1);

            double _latEnd = _jsonValue.GetObject().GetNamedArray("resourceSets").GetObjectAt(0).GetNamedArray("resources").GetObjectAt(0).GetNamedArray("routeLegs").GetObjectAt(0).GetNamedObject("actualEnd").GetNamedArray("coordinates").GetNumberAt(0);
            double _lonEnd = _jsonValue.GetObject().GetNamedArray("resourceSets").GetObjectAt(0).GetNamedArray("resources").GetObjectAt(0).GetNamedArray("routeLegs").GetObjectAt(0).GetNamedObject("actualEnd").GetNamedArray("coordinates").GetNumberAt(1);

            try
            {
                lblInfo.Text = "Tiempo: " + _travelDuration + "seg, Distancia: " + _travelDistance + " km";
                mostrarRutaEnMapa(_latStart, _lonStart, _latEnd, _lonEnd, color);
            }
            catch
            {

            }

        }

        private async void mostrarRutaEnMapa(double latIni, double longIni, double latFin, double longFin, Color color)
        {
            BasicGeoposition startLocation = new BasicGeoposition() { Latitude = latIni, Longitude = longIni };

            BasicGeoposition endLocation = new BasicGeoposition() { Latitude = latFin, Longitude = longFin };

            MapRouteFinderResult routeResult =
                  await MapRouteFinder.GetDrivingRouteAsync(
                  new Geopoint(startLocation),
                  new Geopoint(endLocation),
                  MapRouteOptimization.Time,
                  MapRouteRestrictions.None);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {

                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = color;// Colors.Orange;
                viewOfRoute.OutlineColor = Colors.Black;

                mcMapa.Routes.Add(viewOfRoute);


                await mcMapa.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);

                BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = latIni, Longitude = longIni };
                Geopoint cityCenter = new Geopoint(cityPosition);

                Windows.UI.Core.DispatchedHandler Lectura_PosicionIni = () =>
                {
                    MapIcon myPOI = new MapIcon
                    {
                        Location = cityCenter,
                        NormalizedAnchorPoint = new Point(0.5, 1.0),
                        Title = txtOrigen.Text,
                        ZIndex = 0,
                        Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/ubicacion-start.png"))
                    };
                    mcMapa.MapElements.Add(myPOI);
                };
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, Lectura_PosicionIni);

                BasicGeoposition cityPositionEnd = new BasicGeoposition() { Latitude = latFin, Longitude = longFin };
                Geopoint cityCenterEnd = new Geopoint(cityPositionEnd);

                Windows.UI.Core.DispatchedHandler Lectura_PosicionEnd = () =>
                {
                    MapIcon myPOI = new MapIcon
                    {
                        Location = cityCenterEnd,
                        NormalizedAnchorPoint = new Point(0.5, 1.0),
                        Title = txtDestino.Text,
                        ZIndex = 0,
                        Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/ubicacion-end.png"))
                    };
                    mcMapa.MapElements.Add(myPOI);
                };
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, Lectura_PosicionEnd);
            }
        }

        async private void GPS_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            //Geoposition pos = args.Position;

            Geolocator geolocator = new Geolocator();
            Geoposition pos = await geolocator.GetGeopositionAsync();
            Geopoint myLocation = pos.Coordinate.Point;

            BasicGeoposition cityPosition = new BasicGeoposition() { Latitude = 40.390, Longitude = -3.628 };
            Geopoint cityCenter = new Geopoint(cityPosition);

            Windows.UI.Core.DispatchedHandler Lectura_Posicion = () =>
            {
                MapIcon myPOI = new MapIcon
                {
                    Location = cityCenter,
                    NormalizedAnchorPoint = new Point(0.5, 1.0),
                    Title = "Campus Sur-UPM",
                    ZIndex = 0,
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/ubicacion.png"))
                };
                mcMapa.MapElements.Add(myPOI);
                mcMapa.Center = cityCenter;
                mcMapa.ZoomLevel = 14;
                mcMapa.LandmarksVisible = true;
            };
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, Lectura_Posicion);
        }

        private void scZoom_ValueChanged(object sender, Windows.UI.Xaml.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {
            if (mcMapa != null)
            {
                mcMapa.ZoomLevel = e.NewValue;
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var locator = new Geolocator();
                locator.DesiredAccuracyInMeters = 50;
                var position = await locator.GetGeopositionAsync();

                await mcMapa.TrySetViewAsync(position.Coordinate.Point, 18D);

                scZoom.Value = mcMapa.ZoomLevel;
            }
            catch
            {

            }
        }
    }
}
