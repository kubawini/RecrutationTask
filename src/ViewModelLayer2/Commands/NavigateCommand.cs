using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.Services;
using ViewModelLayer.Stores;
using ViewModelLayer.ViewModels;

namespace ViewModelLayer.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : ViewModelBase
    {
        //private readonly NavigationStore _navigationStore;
        //private readonly Func<ViewModelBase> _createViewModel;
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
            //_navigationStore = navigationStore;
            //_createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            //_navigationStore.CurrentViewModel = _createViewModel();
            _navigationService.Navigate();
        }
    }
}
