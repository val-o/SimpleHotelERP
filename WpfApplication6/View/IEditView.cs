using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Main.View
{
    public interface IEditView
    {
        void Close(object sender, RoutedEventArgs e);
    }
}
