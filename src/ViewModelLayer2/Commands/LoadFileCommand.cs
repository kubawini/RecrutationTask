using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.Win32;
using ModelLayer.Models;
using ViewModelLayer.Stores;
using ViewModelLayer.ViewModels;

namespace ViewModelLayer.Commands
{
    public class LoadFileCommand : CommandBase
    {
        private readonly LoadUsersViewModel _viewModel;
        private readonly NavigationStore _navigationStore;

        public LoadFileCommand(LoadUsersViewModel loadUsersViewModel, NavigationStore navigationStore)
        {
            _viewModel = loadUsersViewModel;
            _navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            _viewModel.FilePath = OpenFileDialog();
            if (string.IsNullOrEmpty(_viewModel.FilePath)) return; //TODO Check what happens when not clicking ok button
            _viewModel.Users = ReadUsers(_viewModel.FilePath);
            _navigationStore.CurrentViewModel = new ListUsersViewModel(_viewModel.Users);
        }

        public string OpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Excel sheets (.csv)|*.csv";
            var result = dialog.ShowDialog();
            return dialog.FileName;
        }

        public IEnumerable<UserModel> ReadUsers(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<UserModel>().ToList();
            }
        }
    }
}
