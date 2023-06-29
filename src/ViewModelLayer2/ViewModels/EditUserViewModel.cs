using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public EditUserViewModel(UsersStore usersStore)
        {
            _usersStore = usersStore;
        }
    }
}
