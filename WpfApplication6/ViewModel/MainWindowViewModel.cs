using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private GuestsViewModel _guestsViewModel;
        private RoomsViewModel _roomsViewModel;
        private PaymentsViewModel _paymentsViewModel;
        private RegistrationViewModel _registrationViewModel;
        private ServicesViewModel _servicesViewModel;
        private ObservableCollection<Hotels> _hotelsCollection;
        public string CurrentHotel { get; set; }

        public ObservableCollection<Hotels> HotelsCollection { get { return _hotelsCollection; }
            set
            {
                _hotelsCollection = value;
                RaisePropertyChanged("HotelsCollection");
            } } 
        public RoomsViewModel RoomsViewModelInstance
        {
            get { return _roomsViewModel; }
            set
            {
                _roomsViewModel = value;
                RaisePropertyChanged("RoomsViewModelInstance");
            }
        }
        public ServicesViewModel ServicesViewModelInstance
        {
            get { return _servicesViewModel; }
            set
            {
                _servicesViewModel = value;
                RaisePropertyChanged("ServicesViewModel");
            }
        }

        public GuestsViewModel GuestsViewModelInstance
        {
            get { return _guestsViewModel; }
            set
            {
                _guestsViewModel = value;
                RaisePropertyChanged("GuestsViewModelInstance");
            }
        }
        public RegistrationViewModel RegistrationViewModelInstance {
            get { return _registrationViewModel; }
            set
            {
                _registrationViewModel = value;
                RaisePropertyChanged("RegistrationViewModelInstance");
            }
        }
        public PaymentsViewModel PaymentsViewModelInstance
        {
            get { return _paymentsViewModel; }
            set
            {
                _paymentsViewModel = value;
                RaisePropertyChanged("PaymentsViewModel");
            }
        }

        public MainWindowViewModel(IRepository<Hotels> hotelsRepository,
            IRepository<Guests> guestsRepository, 
            RoomsRepository roomsRepository,
            TypeOfRoomsRepository typesRepository,
            RegistrationViewRepository registrationViewRepository,
            RegistrationRepository registrationRepository,
            IRepository<Payment> paymentsRepository,
            IViewRepository<PaymentsView> paymentsViewRepository,
            IRepository<Wishes> wishesRepository,
            IRepository<Discounts> discountsRepository ,
            IRepository<GuestsDiscounts> guestsDiscountRepository,
            IRepository<Services> servicesRepository,
            IRepository<ServiceRealization> realizationRepository)
        {
            GuestsViewModelInstance = new GuestsViewModel(guestsRepository, wishesRepository, discountsRepository, guestsDiscountRepository);
            RoomsViewModelInstance = new RoomsViewModel(roomsRepository, typesRepository);
            ServicesViewModelInstance = new ServicesViewModel(servicesRepository, realizationRepository, guestsRepository);
            RegistrationViewModelInstance = new RegistrationViewModel(registrationRepository,registrationViewRepository,roomsRepository, guestsRepository, discountsRepository, guestsDiscountRepository);
            PaymentsViewModelInstance = new PaymentsViewModel(paymentsRepository, paymentsViewRepository, guestsRepository, registrationRepository, realizationRepository);
            CurrentHotel = hotelsRepository.CurrentHotel.HotelName;
            RaisePropertyChanged("CurrentHotel");
        }
        public string CurrentDate {get { return DateTime.Now.ToShortDateString(); }}
    }
}
