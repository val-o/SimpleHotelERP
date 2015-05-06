using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DataModel;

namespace Main.ViewModel.Helpers
{
    public class ActionCommand : ICommand
    {
        private Action _action;
        private Boolean _isExecutable;
        public Boolean IsExecutable
        {
            get { return _isExecutable; } 
            set
            {
                _isExecutable = value;
                if(CanExecuteChanged != null)
                    CanExecuteChanged(this, new EventArgs());
            }
        }

        public ActionCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _isExecutable;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }

}

