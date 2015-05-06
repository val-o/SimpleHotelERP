using System;
using System.Windows;
using System.Windows.Media;
using Main.View;
using Main.ViewModel.Helpers;

namespace Main.ViewModel
{
    public class ErrorViewModel : ViewModelBase
    {
        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set
            {
                _errorText = value;
                RaisePropertyChanged("ErrorText");
            }
        }
        public ErrorViewModel(string errorText)
        {
            ErrorText = errorText;
        }

        public void Close()
        {
            ClosingEvent();
        }

        public event Action ClosingEvent;
    }
}
