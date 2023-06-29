using ModelLayer.Models;
using ModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using ViewModelLayer.Commands;
using ViewModelLayer.Services;
using ViewModelLayer.Stores;

namespace ViewModelLayer.ViewModels
{
    public class LoadUsersViewModel : ViewModelBase
    {
        private string _filePath;
        public string FilePath 
        { 
            get
            {
                return _filePath;
            }
            set
            {
                _filePath = value;
            }
        }

        //public IEnumerable<UserModel> Users;
        public UsersStore UsersStore;
        public ICommand LoadFileCommand { get; }

        public LoadUsersViewModel(NavigationService<ListUsersViewModel> navigationService, UsersStore usersStore, IUsersRepository usersRepository)
        {
            LoadFileCommand = new LoadFileCommand(this, navigationService, usersRepository);
            UsersStore = usersStore;
        }
    }
}
