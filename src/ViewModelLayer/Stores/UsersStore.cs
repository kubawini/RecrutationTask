using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.Stores
{
    public class UsersStore
    {
        private IEnumerable<UserModel> _users;
        public IEnumerable<UserModel> Users
        {
            get { return _users; }
            set { _users = value; }
        }

        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public UsersStore(ObservableCollection<UserModel> users)
        {
            _users = users;
        }

        public UsersStore()
        {
            _users = new ObservableCollection<UserModel>();
        }
    }
}
