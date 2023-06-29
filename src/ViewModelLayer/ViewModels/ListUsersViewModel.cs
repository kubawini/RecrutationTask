using ModelLayer.Models;
using ModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelLayer.Commands;
using ViewModelLayer.Services;
using ViewModelLayer.Stores;

namespace ViewModelLayer.ViewModels
{
    public class ListUsersViewModel : ViewModelBase
    {
        private UsersStore _usersStore;
        public IEnumerable<UserModel> Users
        {
            get { return _usersStore.Users; }
            set { _usersStore.Users = value; }
        }

        public UserModel CurrentUser
        {
            get { return _usersStore.CurrentUser; }
            set { _usersStore.CurrentUser = value; IsUserSelected = value is null ? false : true; }
        }

        private bool _isUserSelected;
        public bool IsUserSelected
        {
            get { return _isUserSelected;}
            set { _isUserSelected = value; OnPropertyChanged("IsUserSelected");  }
        }

        public ICommand OpenEditUser { get; }

        public ListUsersViewModel(NavigationService<EditUserViewModel> navigationService, UsersStore usersStore)
        {
            _usersStore = usersStore;
            OpenEditUser = new NavigateCommand<EditUserViewModel>(navigationService); // TODO What if CurrentUser is empty - simple converter
        }
    }
}
