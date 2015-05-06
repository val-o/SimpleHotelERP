using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel
{
    public class PeekWishesViewModel : ViewModelBase
    {
        public IRepository<Wishes> Repository { get; set; }
        public List<Wishes> Collection { get; set; }


        public PeekWishesViewModel(IRepository<Wishes> repository, Guests currentGuest)
        {
            Repository = repository;
            Collection = Repository.Collection.Where(wish => wish.GuestID == currentGuest.GuestID).ToList();
            CurrentGuest = currentGuest;
            Repository.ModelCollectionChanged += () =>
            {
                Collection = Repository.Collection.Where(wish => wish.GuestID == currentGuest.GuestID).ToList();
                RaisePropertyChanged("Collection");
            };
        }
        public Guests CurrentGuest { get; set; }
        public void Close()
        {
            ClosingEvent();
        }
        public event Action ClosingEvent;
        private PeekWishesEditViewModel _peekWishesEditViewModelInstance;
        public PeekWishesEditViewModel PeekWishesEditViewModelInstance { get
        {
            return _peekWishesEditViewModelInstance;
        }
            set { _peekWishesEditViewModelInstance = value; 
                RaisePropertyChanged("PeekWishesEditViewModelInstance"); }
        }
        private void ShowAll()
        {
            Collection = Repository.Collection.ToList();
            RaisePropertyChanged("Collection");
        }

        private Wishes _currentWish;
        public Wishes CurrentWish
        {
            get { return _currentWish; }
            set
            {
                _currentWish = value;
                RaisePropertyChanged("CurrentWish");
            }
        }
        private void ShowEditViewModel()
        {
            PeekWishesEditViewModelInstance = new PeekWishesEditViewModel(Repository, CurrentGuest);
            PeekWishesEditViewModelInstance.ClosingEvent += () =>
            {
                PeekWishesEditViewModelInstance = null;
                ViewModelReturn();
            };

        }

        public event Action ViewModelReturn;
        public ActionCommand ShowEditViewModelCommand { get { return new ActionCommand(ShowEditViewModel) { IsExecutable = true }; } }
        public ActionCommand ShowAllActionCommand { get { return new ActionCommand(ShowAll) { IsExecutable = true }; } }
    }
}
