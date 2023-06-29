using ModelLayer.Models;
using ModelLayer.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public string CurrentUserName
        {
            get { return _usersStore.CurrentUser.name; }
            set
            {
                CheckForErrors(value, @"^[A-Za-z ]+$", nameof(CurrentUserName));
                OnPropertyChanged(nameof(HasNoErrors));
                _usersStore.CurrentUser.name = value;
            }
        }

        public string CurrentUserSurname
        {
            get { return _usersStore.CurrentUser.surename; }
            set
            {
                CheckForErrors(value, @"^[A-Za-z ]+$", nameof(CurrentUserSurname));
                OnPropertyChanged(nameof(HasNoErrors));
                _usersStore.CurrentUser.surename = value;
            }
        }

        public string CurrentUserEmail
        {
            get { return _usersStore.CurrentUser.email; }
            set
            {
                CheckForErrors(value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){1,3})+)$", nameof(CurrentUserEmail));
                OnPropertyChanged(nameof(HasNoErrors));
                _usersStore.CurrentUser.email = value;
            }
        }

        public string CurrentUserPhone
        {
            get { return _usersStore.CurrentUser.phone; }
            set
            {
                CheckForErrors(value, @"[0-9]+$",nameof(CurrentUserPhone));
                OnPropertyChanged(nameof(HasNoErrors));
                _usersStore.CurrentUser.phone = value;
            }
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
        public bool HasNoErrors => !HasErrors;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName, new List<string>());
        }

        private void CheckForErrors(string value, String regex, string nameOfProperty)
        {
            _propertyNameToErrorsDictionary.Remove(nameOfProperty);
            if (string.IsNullOrEmpty(value) || !Regex.IsMatch(value, regex))
            {
                List<string> errors = new List<string>()
                    {
                        $"The {nameOfProperty} is wrong or empty"
                    };
                _propertyNameToErrorsDictionary.Add(nameOfProperty, errors);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameOfProperty));
            }
        }
    }
}
