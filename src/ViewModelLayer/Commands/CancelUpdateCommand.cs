using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.Services;
using ViewModelLayer.ViewModels;

namespace ViewModelLayer.Commands
{
    public class CancelUpdateCommand : CommandBase
    {
        private readonly NavigationService<ListUsersViewModel> _navigationService;
        private readonly EditUserViewModel _editUserViewModel;

        public CancelUpdateCommand(NavigationService<ListUsersViewModel> navigationService, EditUserViewModel editUserViewModel)
        {
            _navigationService = navigationService;
            _editUserViewModel = editUserViewModel;
        }

        public override void Execute(object parameter)
        {
            _editUserViewModel.CurrentUser.name = _editUserViewModel.CurrentUserCopy.name;
            _editUserViewModel.CurrentUser.surename = _editUserViewModel.CurrentUserCopy.surename;
            _editUserViewModel.CurrentUser.email = _editUserViewModel.CurrentUserCopy.email;
            _editUserViewModel.CurrentUser.phone = _editUserViewModel.CurrentUserCopy.phone;
            _navigationService.Navigate();
        }
    }
}
