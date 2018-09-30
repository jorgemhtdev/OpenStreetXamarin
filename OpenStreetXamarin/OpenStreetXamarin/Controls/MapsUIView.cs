namespace OpenStreetXamarin.Controls
{
    using Xamarin.Forms;
    using Color = Mapsui.Styles.Color;

    public class MapsUIView : View
    {
        public Mapsui.Map NativeMap { get; }

        protected internal MapsUIView()
        {
            NativeMap = new Mapsui.Map();
            NativeMap.BackColor = Color.Black;
        }
    }
}
