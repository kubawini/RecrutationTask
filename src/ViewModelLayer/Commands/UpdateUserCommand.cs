using ModelLayer.Models;
using ModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModelLayer.ViewModels;

namespace ViewModelLayer.Commands
{
    public class UpdateUserCommand : CommandBase
    {
        private readonly UserModel _currentUser;
        private readonly IUsersRepository _usersRepository;
        private readonly NavigateCommand<ListUsersViewModel> _navigateCommand;

        public UpdateUserCommand(UserModel currentUser, IUsersRepository usersRepository, NavigateCommand<ListUsersViewModel> navigateCommand)
        {
            _currentUser = currentUser;
            _usersRepository = usersRepository;
            _navigateCommand = navigateCommand;
        }

        public override void Execute(object parameter)
        {
            try
            {
                _usersRepository.UpdateUser(_currentUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save user into database", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _navigateCommand.Execute(null);
        }
    }
}
