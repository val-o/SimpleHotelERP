namespace Main.View
{
    public static class InfoWindow
    {

        public static void Show(string text)
        {
            var window = new InfoWindowView {Info = {Text = text}};
            window.ShowDialog();
        }
    }
}
