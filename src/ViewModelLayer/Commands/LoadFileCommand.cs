using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using ViewModelLayer.ViewModels;

namespace ViewModelLayer.Commands
{
    public class LoadFileCommand : CommandBase
    {
        private readonly LoadUsersViewModel _viewModel;

        public LoadFileCommand(LoadUsersViewModel loadUsersViewModel)
        {
            _viewModel = loadUsersViewModel;
        }

        public override void Execute(object parameter)
        {
            // TODO
            var dialog = new Microsoft.Win32.OpenFileDialog();

        }
    }
}
