using ModelLayer.Models;
using ModelLayer.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModelLayer.Commands;
using ViewModelLayer.Services;
using ViewModelLayer.Stores;

namespace ViewModelLayer.ViewModels
{
    public class EditUserViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private UsersStore _usersStore;     

        public UserModel CurrentUser
        {
            get { return _usersStore.CurrentUser; }
            set { _usersStore.CurrentUser = value; }
        }

        public ICommand Cancel { get; }
        public ICommand Save { get; }

        public EditUserViewModel(NavigationService<ListUsersViewModel> navigationService, UsersStore usersStore, IUsersRepository usersRepository)
        {
            _usersStore = usersStore;
            Cancel = new NavigateCommand<ListUsersViewModel>(navigationService);
            Save = new UpdateUserCommand(_usersStore.CurrentUser, usersRepository, new(navigationService));
            _propertyNameToErrorsDictionary = new Dictionary<string, List<string>>();
        }


        // Error handling
        private readonly Dictionary<string, List<string>> _propertyNameToErrorsDictionary;
        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }
    }
}
