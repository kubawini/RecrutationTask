using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CsvHelper;
using Microsoft.Win32;
using ModelLayer.Models;
using ModelLayer.Repositories;
using ViewModelLayer.Services;
using ViewModelLayer.Stores;
using ViewModelLayer.ViewModels;

namespace ViewModelLayer.Commands
{
    public class LoadFileCommand : CommandBase
    {
        private readonly LoadUsersViewModel _viewModel;
        private readonly NavigationService<ListUsersViewModel> _navigationService;
        private readonly IUsersRepository _usersRepository;
        private bool _errorOccured = false;

        public LoadFileCommand(LoadUsersViewModel loadUsersViewModel, NavigationService<ListUsersViewModel> navigationService, IUsersRepository usersRepository)
        {
            _viewModel = loadUsersViewModel;
            _navigationService = navigationService;
            _usersRepository = usersRepository;
        }

        public override void Execute(object parameter)
        {
            _errorOccured = false;
            ChooseAndReadFile();
            SaveIntoDb();
            if (!_errorOccured)
            {
                _navigationService.Navigate();
            }
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

        private void ChooseAndReadFile()
        {
            try
            {
                _viewModel.FilePath = OpenFileDialog();
                if (string.IsNullOrEmpty(_viewModel.FilePath)) return;
                _viewModel.UsersStore.Users = ReadUsers(_viewModel.FilePath);
            }
            catch (HeaderValidationException ex)
            {
                MessageBox.Show("Could not load file - wrong file or headers", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _errorOccured = true;
            }
            catch (CsvHelper.MissingFieldException ex)
            {
                MessageBox.Show("Could not load file - a field is missing", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                _errorOccured = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load file " + ex.GetType(), "Error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                _errorOccured = true;
            }
        }

        private void SaveIntoDb()
        {
            try
            {
                _usersRepository.SaveAllUsers(_viewModel.UsersStore.Users);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save users into database", "Error",
                   MessageBoxButton.OK, MessageBoxImage.Error);
                _errorOccured = true;
            }
        }
    }
}
