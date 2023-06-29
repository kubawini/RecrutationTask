using ModelLayer.Models;
using ModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.Commands
{
    public class UpdateUserCommand : CommandBase
    {
        private readonly UserModel _currentUser;
        private readonly IUsersRepository _usersRepository;
        private readonly NavigateCommand _navigateCommand;

        public UpdateUserCommand(UserModel currentUser, IUsersRepository usersRepository, NavigateCommand navigateCommand)
        {
            _currentUser = currentUser;
            _usersRepository = usersRepository;
            _navigateCommand = navigateCommand;
        }

        public override void Execute(object parameter)
        {
            _usersRepository.UpdateUser(_currentUser);
            _navigateCommand.Execute(null);
        }
    }
}
