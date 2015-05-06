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
    /// Interaction logic for PeekWishesView.xaml
    /// </summary>
    public partial class PeekWishesView : UserControl, IEditView
    {
        public PeekWishesView()
        {
            InitializeComponent();
            
        }

        public void Close(object sender, RoutedEventArgs e)
        {
            var storyboard = TryFindResource("ClosingStoryBoard") as Storyboard;
            storyboard.Completed += (o, args) => (DataContext as PeekWishesViewModel).Close();
            storyboard.Begin();
        }

        private void userControl_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as PeekWishesViewModel).ViewModelReturn += () =>
            {
                var storyboard = TryFindResource("ReturnStoryboard") as Storyboard;
                storyboard.Begin();
            };
        }


    }
}
