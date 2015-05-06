using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel.EditViewModels
{
    public class RegistrationEditViewModel : EditViewModelBase<Registration>
    {

        private Rooms _currentRoom;
        private Guests _currentGuest;
        public RegistrationEditViewModel(IRepository<Registration> repository, IRepository<Rooms> freeRoomsRepository, IRepository<Guests> guestsRepository, IRepository<Discounts> discountsRepository, IRepository<GuestsDiscounts> guestsDiscountsRepository )
            : base(repository)
        {
            GuestsRepositoryRef = guestsRepository;
            RoomsRepository = freeRoomsRepository;
            DiscountsRepository = discountsRepository;
            GuestsDiscountsRepository = guestsDiscountsRepository;
            EntryDate = DateTime.Now;
            ExitDate = DateTime.Now.AddDays(7);
            GuestsRepositoryRef.ModelCollectionChanged += () => RaisePropertyChanged("FreeGuestsCollection");
        }

        public RegistrationEditViewModel(IRepository<Registration> repository, IRepository<Rooms> freeRoomsRepository,
            IRepository<Guests> guestsRepository, Registration currentRegistration)
            : base(repository, currentRegistration)
        {
            GuestsRepositoryRef = guestsRepository;
            RoomsRepository = freeRoomsRepository;
            EntryDate = DateTime.Now;
            ExitDate = DateTime.Now.AddDays(7);
        }
        public IRepository<Rooms> RoomsRepository { get; set; }
        public IRepository<Guests> GuestsRepositoryRef { get; set; }
        public IRepository<Discounts> DiscountsRepository { get; set; }
        public IRepository<GuestsDiscounts> GuestsDiscountsRepository { get; set; }

        public DateTime AvailableDates { get { return DateTime.Now; } }
        public int? GuestID
        {
            get
            {
                return EntityObj.GuestID;
            }
            set
            {
                EntityObj.GuestID = value;
            }
        }
        public int? RoomID
        {
            get
            {
                return EntityObj.RoomID;
            }
            set { EntityObj.RoomID = value; }
        }
        public Rooms CurrentRoom
        {
            get { return _currentRoom; }
            set
            {
                _currentRoom = value;
                EntityObj.RoomID = CurrentRoom.RoomID;
                EntityObj.Rooms = CurrentRoom;
                RaisePropertyChanged("CurrentRoom");
                ValidateAllFields();
            }
        }
        public Guests CurrentGuest
        {
            get
            {
                return _currentGuest;
            }
            set
            {
                _currentGuest = value;
                EntityObj.GuestID = CurrentGuest.GuestID;
                EntityObj.Guests = CurrentGuest;
                RaisePropertyChanged("CurrentGuest");
                ValidateAllFields();
            }
        }
        public DateTime? EntryDate { get { return EntityObj.EntryDate; } 
            set
        {
            EntityObj.EntryDate = value;
            RaisePropertyChanged("EntryDate");
            RaisePropertyChanged("FreeRoomsCollection");
            if (EntryDate > ExitDate) ExitDate = EntryDate.Value.AddDays(1);
        } 
        }
        public DateTime? ExitDate
        {
            get
            {
                return EntityObj.ExitDate;
            }
            set
            {
                EntityObj.ExitDate = value;
                RaisePropertyChanged("ExitDate");
                RaisePropertyChanged("FreeRoomsCollection");

            } 
        }
        public ObservableCollection<Rooms> FreeRoomsCollection
        {
            get { return (RoomsRepository as RoomsRepository).GetFreeRoomsCollection(EntryDate, ExitDate); }
        }
        public ObservableCollection<Guests> FreeGuestsCollection
        {
            get { return (GuestsRepositoryRef as GuestsRepository).GetFreeGuestsCollection(EntryDate, ExitDate); }
        }

        protected override void ValidateAllFields()
        {
            IsAddButtonEnabled = CurrentGuest != null && CurrentRoom != null;
        }

        protected override string IsValid(string propertyName)
        {
            throw new NotImplementedException();
        }
        public ActionCommand AddGuestCommand {
            get { return new ActionCommand(OnGuestAddButtonClicked) { IsExecutable = true }; }
        }

        private void OnGuestAddButtonClicked()
        {
            GuestAddViewModel = new GuestsEditViewModel(GuestsRepositoryRef, DiscountsRepository, GuestsDiscountsRepository);
            GuestAddViewModel.CloseEvent += () => GuestAddViewModel = null;
        }

        protected override void OnAddCommand()
        {
            Repository.Add(EntityObj);
            RaisePropertyChanged("Collection");
        }

        private GuestsEditViewModel _guestAddViewModel;
        public GuestsEditViewModel GuestAddViewModel { get { return _guestAddViewModel; } set
        {
            _guestAddViewModel = value;
            RaisePropertyChanged("GuestAddViewModel");
        } }
    }
}
