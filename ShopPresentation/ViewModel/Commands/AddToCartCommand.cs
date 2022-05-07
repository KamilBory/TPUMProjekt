using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Text;

namespace ShopPresentation.ViewModel.Commands
{
    class AddToCartCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
