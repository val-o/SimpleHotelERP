using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Main.ViewModel.EditViewModels;
using Main.ViewModel.Helpers;

namespace Main.ViewModel
{
    public class StartWindowViewModel : ViewModelBase
    {
        private HotelEditViewModel _hotelEditViewModel;
        private Hotels _selectedHotel;
        private void OnAddButtonClicked()
        {
            HotelEditViewModel = new HotelEditViewModel(Repository);
            HotelEditViewModel.Closing += () =>
            {
                HotelEditViewModel = null;
                ViewModelReturn();
            };
        }

        private void OnEditButtonClicked()
        {
            HotelEditViewModel = new HotelEditViewModel(Repository, SelectedHotel);
            HotelEditViewModel.Closing += () =>
            {
                HotelEditViewModel = null;
                ViewModelReturn();
            };
        }
        public Boolean IsEditButtonEnabled { get; set; }
        public HotelEditViewModel HotelEditViewModel
        {
            get { return _hotelEditViewModel; }
            set
            {
                _hotelEditViewModel = value; 
                RaisePropertyChanged("HotelEditViewModel");
            }
        }
        public IRepository<Hotels> Repository { get; set; }
        private ObservableCollection<Hotels> _hotelsCollection;
        public ObservableCollection<Hotels> HotelsCollection { get { return _hotelsCollection; }
            set
            {
                _hotelsCollection = value;
                RaisePropertyChanged("HotelsCollection");
            } }
        public StartWindowViewModel(IRepository<Hotels> repository)
        {
            Repository = repository;
            HotelsCollection = Repository.Collection;
            Repository.ModelCollectionChanged += () =>
            {
                HotelsCollection = Repository.Collection;
                RaisePropertyChanged("HotelsCollection");
            };
        }

        public event Action StartButtonClicked;
        public event Action ViewModelReturn;
        private void OnStartButtonClicked()
        {
            StartButtonClicked();
        }
        public ActionCommand StartButtonClick { get { return new ActionCommand(OnStartButtonClicked) { IsExecutable = true }; } }
        public ActionCommand EditButtonClick { get { return new ActionCommand(OnEditButtonClicked) { IsExecutable = true }; } }
        public ActionCommand AddButtonClick { get { return new ActionCommand(OnAddButtonClicked) { IsExecutable = true }; } }
        public Hotels SelectedHotel {
            get { return _selectedHotel; }
            set { 
                _selectedHotel = value;
            IsStartButtonEnabled = _selectedHotel != null;
                IsEditButtonEnabled = _selectedHotel != null;
                RaisePropertyChanged("IsEditButtonEnabled");
            } 
        }

        private bool _isStartButtonEnabled;
        public bool IsStartButtonEnabled { get { return _isStartButtonEnabled; } set { _isStartButtonEnabled = value; RaisePropertyChanged("IsStartButtonEnabled"); } }
    }
}
