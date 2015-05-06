using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel
{
    public class PeekWishesEditViewModel : ViewModelBase
    {
        public Guests CurrentGuest { get; set; }

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

        public string WishTitle
        {
            get { return CurrentWish.WishTitile; }
            set
            {
                CurrentWish.WishTitile = value;
                RaisePropertyChanged("WishTitle");
                ValidateFields();
            }
        }
        public string WishContent
        {
            get { return CurrentWish.WishContent; }
            set
            {
                CurrentWish.WishContent = value;
                RaisePropertyChanged("WishContent");
                ValidateFields();
            }
        }
        public IRepository<Wishes> Repository { get; set; }
        public PeekWishesEditViewModel(IRepository<Wishes> repository, Guests currentGuest)
        {
            CurrentGuest = currentGuest;
            Repository = repository;
            CurrentWish=new Wishes();
        }
        public void Close()
        {
            ClosingEvent();
        }
        public event Action ClosingEvent;
        private void Add()
        {
            CurrentWish.GuestID = CurrentGuest.GuestID;
            CurrentWish.WishDate = DateTime.Now;
            Repository.Add(CurrentWish);
        }
        public bool IsAddButtonEnabled { get; set; }
        private void ValidateFields()
        {
            IsAddButtonEnabled = !String.IsNullOrEmpty(WishTitle) && !String.IsNullOrEmpty(WishContent);
            if (IsAddButtonEnabled) RaisePropertyChanged("IsAddButtonEnabled");
        }
        public ActionCommand AddCommand { get { return new ActionCommand(Add) { IsExecutable = true }; } }
    }
}
