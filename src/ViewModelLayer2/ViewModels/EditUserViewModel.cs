using ModelLayer.Models;
using ModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelLayer.Commands;
using ViewModelLayer.Stores;

namespace ViewModelLayer.ViewModels
{
    public class EditUserViewModel : ViewModelBase
    {
        private UsersStore _usersStore;
        public UserModel CurrentUser
        {
            get { return _usersStore.CurrentUser; }
            set { _usersStore.CurrentUser = value; }
        }

        public ICommand Cancel { get; }
        public ICommand Save { get; }

        public EditUserViewModel(NavigationStore navigationStore, UsersStore usersStore, IUsersRepository usersRepository)
        {
            _usersStore = usersStore;
            Cancel = new NavigateCommand(navigationStore, () => new ListUsersViewModel(navigationStore, usersStore, usersRepository));
            Save = new UpdateUserCommand(_usersStore.CurrentUser, usersRepository, new NavigateCommand(navigationStore, () => new ListUsersViewModel(navigationStore, usersStore, usersRepository))); // TODO too long line

        }
    }
}
