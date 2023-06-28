using ModelLayer.Models;
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

        public IEnumerable<UserModel> Users;
        public ICommand LoadFileCommand { get; }

        public LoadUsersViewModel(NavigationStore navigationStore)
        {
            LoadFileCommand = new LoadFileCommand(this, navigationStore);
        }
    }
}
