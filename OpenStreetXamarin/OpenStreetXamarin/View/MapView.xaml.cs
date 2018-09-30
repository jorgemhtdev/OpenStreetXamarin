using System.Collections.Generic;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Utilities;
using OpenStreetXamarin.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Mapsui.Styles.Color;
using Point = Mapsui.Geometries.Point;

namespace OpenStreetXamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapView : ContentPage
    {
        public MapView()
	    {
	        InitializeComponent();

	        var ubication = new Point(-3.7025600, 40.4165000);
	        var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(ubication.X, ubication.Y);

            var mapControl = new MapsUIView();


	        mapControl.NativeMap.NavigateTo(sphericalMercatorCoordinate);
	        mapControl.NativeMap.Layers.Add(OpenStreetMap.CreateTileLayer());
            mapControl.NativeMap.NavigateTo(mapControl.NativeMap.Resolutions[9]);




            var layer = GenerateIconLayer();
	        mapControl.NativeMap.Layers.Add(layer);
	        mapControl.NativeMap.InfoLayers.Add(layer);

	        ContentGrid.Children.Add(mapControl);

	        mapControl.NativeMap.Info += (sender, args) =>
	        {
	            var layername = args.MapInfo?.Layer.Name;
                var featureLabel = args.MapInfo.Feature?["Label"]?.ToString();
	            var featureType = args.MapInfo.Feature?["Type"]?.ToString();

	            if (featureType != null && featureType.Equals("Point"))
	            {
	                ShowPopup(featureLabel);
                }

            };

        }

        private ILayer GenerateIconLayer()
	    {
	        var layername = "My map";

	        return new Layer(layername)
	        {
	            Name = layername,
	            DataSource = new MemoryProvider(GetIconFeatures()),
	            Style = new SymbolStyle
	            {
	                SymbolScale = 0.5,
	                Fill = new Brush(Color.Red),
	                Outline = { Color = Color.Black, Width = 0.5 }
	            }
	        };
	    }

	    private Features GetIconFeatures()
	    {
            var features = new Features();

            // http://mapsui.github.io/Mapsui/api/Mapsui.Geometries.Point.html
            // http://kartoweb.itc.nl/geometrics/coordinate%20systems/body.htm

            var ubication = new Point(-3.7025600, 40.4165000);

            var feature = new Feature
	        {
	            Geometry = ubication,
                ["Label"] = "Pizza Express",
	            ["Type"] = "Point"
            };
	        

	        features.Add(feature);
	        return features;
	    }

	    public async void ShowPopup(string feature)
	    {
	        if (await this.DisplayAlert(
	            "You have clicked " + feature,
	            "Do you want to see details?",
	            "Yes",
	            "No"))
	        {
	            //Debug.WriteLine("User clicked OK");
	        }
	    }

    }
}