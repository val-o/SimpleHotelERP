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
    /// Логика взаимодействия для GuestsEditView.xaml
    /// </summary>
    public partial class GuestsEditView : UserControl, IEditView
    {
        public GuestsEditView()
        {
            InitializeComponent();
        }
        public void Close(object sender, RoutedEventArgs e)
        {
            var storyboard = TryFindResource("ClosingStoryBoard") as Storyboard;
            storyboard.Completed += (o, args) => (DataContext as GuestsEditViewModel).Close();
            storyboard.Begin();
        }
    }
}
