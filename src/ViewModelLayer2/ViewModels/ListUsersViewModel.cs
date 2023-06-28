using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelLayer.ViewModels
{
    public class ListUsersViewModel : ViewModelBase
    {
        private readonly ObservableCollection<UserModel> _users;
        public IEnumerable<UserModel> Users => _users;

        public ListUsersViewModel(IEnumerable<UserModel> users)
        {
            _users = new ObservableCollection<UserModel>(users);
        }
    }
}
