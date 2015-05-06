using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataModel;
using Main.View;
using Main.ViewModel.EditViewModels;
using Main.ViewModel.Helpers;

namespace Main.ViewModel
{
    public class ServicesViewModel : ViewModelBase
    {
        private bool _serviceAddButtonEnablence = true;
        private bool _controlServiceButtonEnablence;
        private bool _controlRealiztionButtonEnablence;
        private Services _currentService;
        private ServiceRealization _currentRealization;
        public bool RealizationControlButtonEnablence
        {
            get { return _controlRealiztionButtonEnablence; }
            set
            {
                _controlRealiztionButtonEnablence = value;
                RaisePropertyChanged("RealizationControlButtonEnablence");
            }
        }
        public bool ServiceControlButtonEnablence
        {
            get { return _controlServiceButtonEnablence; }
            set
            {
                _controlServiceButtonEnablence = value;
                RaisePropertyChanged("ServiceControlButtonEnablence");
            }
        }
        public bool ServiceAddButtonEnablence
        {
            get { return _serviceAddButtonEnablence; }
            set
            {
                _serviceAddButtonEnablence = value;
                RaisePropertyChanged("ServiceAddButtonEnablence");
            }
        }
        
        public Services CurrentService
        {
            get { return _currentService; }
            set
            {
                _currentService = value;
                ServiceControlButtonEnablence = value != null;
            }
        }
        public IRepository<Services> ServicesRepository { get; set; }
        public IRepository<Guests> GuestsRepository { get; set; }
        public IRepository<ServiceRealization> RealizationRepository { get; set; }
        public ObservableCollection<Services> ServicesCollection { get { return ServicesRepository.Collection; } }
        public ObservableCollection<ServiceRealization> RealizationCollection { get { return RealizationRepository.Collection;   } } 
        public ServicesViewModel(IRepository<Services> servicesRepository,
            IRepository<ServiceRealization> realizationRepository, IRepository<Guests> guestsRepository)
        {
            ServicesRepository = servicesRepository;
            RealizationRepository = realizationRepository;
            RealizationRepository.ModelCollectionChanged += () => RaisePropertyChanged("RealizationCollection");
            GuestsRepository = guestsRepository;
            ServicesRepository.ModelCollectionChanged += () => RaisePropertyChanged("ServicesCollection");
        }

        public ServicesEditViewModel ServicesEditViewModel
        {
            get
            {
                return _servicesEditViewModel;
            }
            set
            {
                _servicesEditViewModel = value;
                ServiceAddButtonEnablence = RealizationControlButtonEnablence = ServiceControlButtonEnablence = value == null;
                RaisePropertyChanged("ServicesEditViewModel");
            }
        }

        private ServicesEditViewModel _servicesEditViewModel;

        public RealizationEditViewModel RealizationEditViewModel
        {
            get
            {
                return _realizationEditViewModel;
            }
            set
            {
                _realizationEditViewModel = value;
                ServiceAddButtonEnablence = RealizationControlButtonEnablence = ServiceControlButtonEnablence = value == null;
                RaisePropertyChanged("RealizationEditViewModel");
            }
        }

        private RealizationEditViewModel _realizationEditViewModel;
        private void OnAddService()
        {
            ServicesEditViewModel = new ServicesEditViewModel(ServicesRepository);
            ServicesEditViewModel.CloseEvent += () => ServicesEditViewModel = null;
        }

        private void OnEditService()
        {
             ServicesEditViewModel = new ServicesEditViewModel(ServicesRepository, CurrentService);
            ServicesEditViewModel.CloseEvent += () => ServicesEditViewModel = null;
        }
        private void OnAddRealization()
        {
            RealizationEditViewModel = new RealizationEditViewModel(RealizationRepository, GuestsRepository, ServicesRepository);
            RealizationEditViewModel.CloseEvent += () => RealizationEditViewModel = null;
        }

        private void OnDeleteRealization()
        {
            RealizationRepository.Remove(CurrentRealization);
        }
        private void OnDeleteService()
        {
            try
            {
                ServicesRepository.Remove(CurrentService);
            }
            catch
            {
                InfoWindow.Show("Удаление запрещено");
            }
        }

        public ServiceRealization CurrentRealization
        {
            get
            {
                return _currentRealization;
            }
            set
            {
                _currentRealization = value;
                RealizationControlButtonEnablence = value != null;
            }
        }

        public ActionCommand DeleteRealizationCommand { get { return new ActionCommand(OnDeleteRealization) { IsExecutable = true }; } }
        public ActionCommand DeleteServiceCommand { get { return new ActionCommand(OnDeleteService) { IsExecutable = true }; } }
        public ActionCommand AddRealizationCommand { get { return new ActionCommand(OnAddRealization) { IsExecutable = true }; } }
        
        public ActionCommand AddServiceCommand { get { return new ActionCommand(OnAddService) { IsExecutable = true }; } }
        public ActionCommand EditServiceCommand { get { return new ActionCommand(OnEditService) { IsExecutable = true }; } }
    }
}
