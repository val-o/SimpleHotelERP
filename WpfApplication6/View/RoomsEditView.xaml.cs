using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Main.ViewModel.EditViewModels;

namespace Main.View
{
    /// <summary>
    /// Логика взаимодействия для RoomsEditView.xaml
    /// </summary>
    public partial class RoomsEditView : UserControl, IEditView
    {
        public RoomsEditView()
        {
            InitializeComponent();
            
        }
        public void Close(object sender, RoutedEventArgs e)
        {
            var storyboard = TryFindResource("ClosingStoryBoard") as Storyboard;
            storyboard.Completed += (o, args) => (DataContext as RoomsEditViewModel).Close();
            storyboard.Begin();
        }

        private void NumberOfPlaces_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char temp = e.Text[e.Text.Length - 1];
            if (!char.IsDigit(temp) && temp != '.')
            {
                e.Handled = true;
            }
            if ((sender as TextBox).Text.Length > 1)
                e.Handled = true;
        }

        private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char temp = e.Text[e.Text.Length - 1];
            if (!char.IsDigit(temp) && temp != '.')
            {
                e.Handled = true;
            }
            if ((sender as TextBox).Text.Length > 5)
                e.Handled = true;
        }
    }
}
