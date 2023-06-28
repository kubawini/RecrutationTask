using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public ICommand LoadFileCommand { get; }
    }
}
