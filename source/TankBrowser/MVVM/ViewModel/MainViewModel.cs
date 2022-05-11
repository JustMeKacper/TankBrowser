using System;
using TankBrowser.Core;

namespace TankBrowser.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand ContentViewCommand { get; set; }
        
        public RelayCommand EditContentViewCommand { get; set; }
        public RelayCommand AddTankContentViewCommand { get; set; }

        public ContentViewModel ContentVm{ get; set; }
        public EditContentViewModel EditModeVm { get; set; }
        public AddContentViewModel AddModeVm { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            ContentVm = new ContentViewModel();
            EditModeVm = new EditContentViewModel();
            AddModeVm = new AddContentViewModel();
            
            CurrentView = ContentVm;

            ContentViewCommand = new RelayCommand(o =>
            {
                CurrentView = ContentVm;
            });

            EditContentViewCommand = new RelayCommand(o =>
            {
                CurrentView = EditModeVm;
            });

            AddTankContentViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddModeVm;
            });

        }

    }
}
