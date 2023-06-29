using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelLayer.Commands;
using ViewModelLayer.Stores;

namespace ViewModelLayer.ViewModels
{
    public class ListUsersViewModel : ViewModelBase
    {
        //private readonly ObservableCollection<UserModel> _users;
        //public IEnumerable<UserModel> Users => _users;

        private UsersStore _usersStore;
        public IEnumerable<UserModel> Users
        {
            get { return _usersStore.Users; }
            set { _usersStore.Users = value; }
        }

        public UserModel CurrentUser
        {
            get { return _usersStore.CurrentUser; }
            set { _usersStore.CurrentUser = value; }
        }

        public ICommand OpenEditUser { get; }

        public ListUsersViewModel(NavigationStore navigationStore, UsersStore usersStore)
        {
            _usersStore = usersStore;
            OpenEditUser = new NavigateCommand(navigationStore, () => new EditUserViewModel(usersStore));
        }
    }
}
