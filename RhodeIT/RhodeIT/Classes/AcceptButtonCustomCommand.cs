using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RhodeIT.Classes
{
    public class AcceptButtonCustomCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return false;
        }

        public void Execute(object parameter)
        {
            var test = parameter;
            // You can write your set of codes that needs to be executed
        }
    }
}
