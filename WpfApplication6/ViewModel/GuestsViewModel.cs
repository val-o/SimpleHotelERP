using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DataModel;
using Main.View;
using Main.ViewModel.EditViewModels;
using Main.ViewModel.Helpers;

namespace Main.ViewModel
{
    public class GuestsViewModel : EntityViewModelBase<Guests>
    {
        private PeekWishesViewModel _wishesViewModel;

        public IRepository<Discounts> DiscountsRepository { get; set; }
        public IRepository<Wishes> WishesRepository { get; set; }
        public IRepository<GuestsDiscounts> GuestsDiscountsRepository { get; set; }

        public GuestsViewModel(IRepository<Guests> repository, IRepository<Wishes> wishesRepository, IRepository<Discounts> discountsRepository, IRepository<GuestsDiscounts> guestsDiscountsRepository ) : base(repository)
        {
            WishesRepository = wishesRepository;
            CurrentEntityObjectChanged += () =>
            {
                ShowPeekWishesCommand = new ActionCommand(OnShowPeekWishesCommand){IsExecutable = true};
                RaisePropertyChanged("ShowPeekWishesCommand");
            };
            GuestsDiscountsRepository = guestsDiscountsRepository;
            DiscountsRepository = discountsRepository;
            IsAddAndEditButtonsEnabled = false;
        }
        
        public PeekWishesViewModel WishesViewModel
        {
            get { return _wishesViewModel; }
            set
            {
                _wishesViewModel = value;
                RaisePropertyChanged("WishesViewModel");
            }
        }
        public ActionCommand ShowPeekWishesCommand { get; set; }

        protected override void OnAddClickedCommand()
        {
            EditViewModelInstance = new GuestsEditViewModel(Repository, DiscountsRepository, GuestsDiscountsRepository);
            EditViewModelInstance.CloseEvent += () => EditViewModelInstance = null;
        }

        protected override void OnEditClickedCommand()
        {
            EditViewModelInstance = new GuestsEditViewModel(Repository, CurrentEntityObject, DiscountsRepository, GuestsDiscountsRepository);
            EditViewModelInstance.CloseEvent += () => EditViewModelInstance = null;
        }
        private void OnShowPeekWishesCommand()
        {
            WishesViewModel = new PeekWishesViewModel(WishesRepository, CurrentEntityObject);
            WishesViewModel.ClosingEvent += () => WishesViewModel = null;
        }
        protected override void OnDeleteClickedCommand()
        {
            try
            {
                Repository.Remove(CurrentEntityObject);
            }
            catch (Exception ex)
            {
                InfoWindow.Show(ex.Message);
            }
        }


    }
}