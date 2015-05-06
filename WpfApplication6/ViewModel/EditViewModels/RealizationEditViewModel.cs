using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel.EditViewModels
{
    public class RealizationEditViewModel : ViewModelBase
    {
        private Boolean _isAddButtonEnabled;
        private ServiceRealization _entityObj;
        //Repository Main Collection
        public ObservableCollection<ServiceRealization> Collection
        {
            get { return Repository.Collection; }
            set
            {
                Repository.Collection = value;
                RaisePropertyChanged("Collection");
            }
        }
        public IRepository<ServiceRealization> Repository { get; set; }
        //Текущий объект
        public ServiceRealization EntityObj
        {
            get { return _entityObj; }
            set
            {
                _entityObj = value;
                RaisePropertyChanged("EntityObj");
            }
        }

        public Boolean IsAddButtonEnabled
        {
            get { return _isAddButtonEnabled; }
            set
            {
                _isAddButtonEnabled = value;
                RaisePropertyChanged("IsAddButtonEnabled");
            }
        }
        public ActionCommand AddOrEditCommand { get; set; }
        public IRepository<Services> ServicesRepository { get; set; }

        public ObservableCollection<Services> ServicesCollection
        {
            get { return ServicesRepository.Collection; }
            set
            {
                ServicesRepository.Collection = value;
                RaisePropertyChanged("ServicesCollection");
            }
        }
        public IRepository<Guests> GuestsRepositoryIns { get; set; }
        public ObservableCollection<Guests> GuestsCollection {get
        {
            return (GuestsRepositoryIns as GuestsRepository).GetGuestsByDate(DateTime.Now);
        }} 
        public RealizationEditViewModel(IRepository<ServiceRealization> repository, IRepository<Guests> guestsRepository, IRepository<Services> servicesRepository)
        {
            Repository = repository;
            ServicesRepository = servicesRepository;
            GuestsRepositoryIns = guestsRepository;
            EntityObj = new ServiceRealization();
            AddOrEditCommand = new ActionCommand(OnAddCommand) { IsExecutable = true };
        }

        protected virtual void OnAddCommand()
        {
            Repository.Add(EntityObj);
            RaisePropertyChanged("Collection");
        }

        public void Close()
        {
            CloseEvent();
        }
        public event Action CloseEvent;

        private void ValidateFields()
        {
            IsAddButtonEnabled = CurrentGuest != null && CurrentService != null;
        }
        public Guests CurrentGuest
        {
            get { return EntityObj.Guests; }
            set
            {
                EntityObj.Guests = value;
                RaisePropertyChanged("CurrentGuest");
                ValidateFields();
            }
        }

        public Services CurrentService
        {
            get { return EntityObj.Services; }
            set
            {
                EntityObj.Services = value;
                RaisePropertyChanged("CurrentService");
                ValidateFields();
            }
        }
    }

}
