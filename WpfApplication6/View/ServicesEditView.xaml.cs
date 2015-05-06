using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Main.ViewModel;
using Main.ViewModel.EditViewModels;

namespace Main.View
{
    /// <summary>
    /// Interaction logic for ServicesEditView.xaml
    /// </summary>
    public partial class ServicesEditView : UserControl
    {
        public ServicesEditView()
        {
            InitializeComponent();
        }

        public void Close(object sender, RoutedEventArgs e)
        {
            var storyboard = TryFindResource("ClosingStoryBoard") as Storyboard;
            storyboard.Completed += (o, args) => (DataContext as ServicesEditViewModel).Close();
            storyboard.Begin();
        }
     
        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
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
