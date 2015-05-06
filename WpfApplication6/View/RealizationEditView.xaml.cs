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
using Main.ViewModel.EditViewModels;

namespace Main.View
{
    /// <summary>
    /// Interaction logic for RealizationEditView.xaml
    /// </summary>
    public partial class RealizationEditView : UserControl
    {
        public RealizationEditView()
        {
            InitializeComponent();
        }
        public void Close(object sender, RoutedEventArgs e)
        {
            var storyboard = TryFindResource("ClosingStoryBoard") as Storyboard;
            storyboard.Completed += (o, args) => (DataContext as RealizationEditViewModel).Close();
            storyboard.Begin();
        }
    }
}
