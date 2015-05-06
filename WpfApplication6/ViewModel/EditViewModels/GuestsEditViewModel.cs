using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel.EditViewModels
{
    public class GuestsEditViewModel : EditViewModelBase<Guests>
    {
        private ObservableCollection<Discounts> _discountsCollection;
        private Discounts _currentDiscount;
        private Boolean _isDiscountLinkEnabled;

        public GuestsEditViewModel(IRepository<Guests> repository, IRepository<Discounts> discountsRepository, IRepository<GuestsDiscounts> guestsDiscountsRepository)
            : base(repository)
        {
            DiscountsRepository = discountsRepository;
            DiscountsCollection = DiscountsRepository.Collection;
            GuestsDiscountsRepository = guestsDiscountsRepository;
        }
        public GuestsEditViewModel(IRepository<Guests> repository, Guests guest, IRepository<Discounts> discountsRepository, IRepository<GuestsDiscounts> guestsDiscountsRepository  )
            : base(repository, guest)
        {
            DiscountsRepository = discountsRepository;
            DiscountsCollection = DiscountsRepository.Collection;
            GuestsDiscountsRepository = guestsDiscountsRepository;
            CurrentDiscounts =
                guest.GuestsDiscounts.Count(gd => gd.Discounts.HotelID == Repository.CurrentHotel.HotelID) != 0
                    ? guest.GuestsDiscounts.Single(gd => gd.Discounts.HotelID == Repository.CurrentHotel.HotelID).Discounts
                    : null;
        }
        public IRepository<Discounts> DiscountsRepository { get; set; }
        public IRepository<GuestsDiscounts> GuestsDiscountsRepository { get; set; }
        public ObservableCollection<Discounts> DiscountsCollection
        {
            get
            {
                return _discountsCollection;
            }
            set
            {
                _discountsCollection = value;
                RaisePropertyChanged("DiscountsCollection");
            }
        }
        public ActionCommand DeleteDiscount { get { return new ActionCommand(OnDeleteDiscount) {IsExecutable = true};} }

        private void OnDeleteDiscount()
        {
            CurrentDiscounts = null;
        }
        public Discounts CurrentDiscounts
        {
            get
            {   
                return _currentDiscount;
            }
            set
            {
                _currentDiscount = value;
                RaisePropertyChanged("CurrentDiscounts");
                IsDiscountLinkEnabled = value != null;
                ValidateAllFields();
            }
        }
        public string GuestName
        {
            get { return EntityObj.Name; }
            set
            {
                EntityObj.Name = value;
                RaisePropertyChanged("GuestName");
                ValidateAllFields();
            }
        }
        public string SecName
        {
            get { return EntityObj.SecName; }
            set
            {
                EntityObj.SecName = value;
                RaisePropertyChanged("SecName");
                ValidateAllFields();
            }
        }
        public string ThirdName
        {
            get { return EntityObj.ThirdName; }
            set
            {
                EntityObj.ThirdName = value;
                RaisePropertyChanged("ThirdName");
                ValidateAllFields();
            }
        }
        public string PassData
        {
            get { return EntityObj.PassportData; }
            set
            {
                EntityObj.PassportData = value;
                RaisePropertyChanged("PassData");
                ValidateAllFields();
            }


        }
        public string Address
        {
            get { return EntityObj.Address; }
            set
            {
                EntityObj.Address = value;
                RaisePropertyChanged("Address");
                ValidateAllFields();
            }
        }
        public string PhoneNumber
        {
            get { return EntityObj.PhoneNumber; }
            set
            {
                EntityObj.PhoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
                ValidateAllFields();
            }
        }
        public DateTime? BirthDate
        {
            get { return EntityObj.BirthDate; }
            set
            {
                EntityObj.BirthDate = value;
                RaisePropertyChanged("BirthDate");
                ValidateAllFields();
            }
        }

        public Boolean IsDiscountLinkEnabled
        {
            get { return _isDiscountLinkEnabled; }
            set
            {
                _isDiscountLinkEnabled = value;
                RaisePropertyChanged("IsDiscountLinkEnabled");
            }
        }

        protected override void ValidateAllFields()
        {
            IsAddButtonEnabled = !String.IsNullOrEmpty(GuestName) && this["GuestName"] == "" &&
                                 !String.IsNullOrEmpty(SecName) && this["SecName"] == "" &&
                                 !String.IsNullOrEmpty(ThirdName) && this["ThirdName"] == "" &&
                                 !String.IsNullOrEmpty(PassData) && this["PassData"] == "" &&
                                 !String.IsNullOrEmpty(Address) && this["Address"] == "" &&
                                 !String.IsNullOrEmpty(PhoneNumber) && this["PhoneNumber"] == "" &&
                                 BirthDate != null && this["BirthDate"] == "";
        }
        protected override void OnAddCommand()
        {
            Repository.Add(EntityObj);
            if (_currentDiscount != null)
            {
                GuestsDiscountsRepository.Add(new GuestsDiscounts() { GuestID = EntityObj.GuestID, DiscountID = CurrentDiscounts.DiscountID });
            }
            Repository.Refresh();
        }

        protected override void OnEditCommand()
        {
                if (EntityObj.GuestsDiscounts.Count(gd => gd.Discounts.HotelID == Repository.CurrentHotel.HotelID) != 0)
                {
                    if (CurrentDiscounts != null)
                        EntityObj.GuestsDiscounts.Single(gd => gd.Discounts.HotelID == Repository.CurrentHotel.HotelID)
                            .DiscountID = CurrentDiscounts.DiscountID;
                    else
                        GuestsDiscountsRepository.Remove(EntityObj.GuestsDiscounts.Single(gd => gd.Discounts.HotelID == Repository.CurrentHotel.HotelID));
                }
                else
                    GuestsDiscountsRepository.Add(new GuestsDiscounts() { GuestID = EntityObj.GuestID, DiscountID = CurrentDiscounts.DiscountID });
            GuestsDiscountsRepository.Update();
            Repository.Refresh();
        }

        protected override string IsValid(string propertyName)
        {
            switch (propertyName)
            {
                case "GuestName":
                {
                    if (!String.IsNullOrEmpty(GuestName))
                    {
                        if (GuestName.Length > 20)
                        {
                            return "Слишком длинное имя";
                        }
                        if (GuestName.Length < 2)
                        {
                            return "Слишком короткое имя";
                        }
                        return "";
                    }
                    return "Поле обязательно для заполенения";
                }

                case "SecName":
                {
                    if (!String.IsNullOrEmpty(SecName))
                    {
                        if (SecName.Length > 20)
                        {
                            return "Слишком длинная фамилия";
                        }
                        if (SecName.Length < 2)
                        {
                            return "Слишком короткая фамилия";
                        }
                        return "";
                    }
                    return "Поле обязательно для заполенения";
                }

                case "ThirdName":
                {
                    if (!String.IsNullOrEmpty(ThirdName))
                    {
                        if (ThirdName.Length > 20)
                        {
                            return "Слишком длинное отчество";
                        }
                        if (ThirdName.Length < 2)
                        {
                            return "Слишком короткое отчество";
                        }
                        return "";
                    }
                    return "Поле обязательно для заполенения";

                }

                case "PassData":
                {
                    if (!String.IsNullOrEmpty(PassData))
                        return "";
                    return "Поле обязательно для заполенения";
                }

                case "Address":
                {
                    if (!String.IsNullOrEmpty(Address))
                        return "";
                    return "Поле обязательно для заполенения";
                }

                case "PhoneNumber":
                {
                    if (!String.IsNullOrEmpty(PhoneNumber))
                        return "";
                    return "Поле обязательно для заполенения";
                }

                case "BirthDate":
                {
                    if (BirthDate != null)
                    {
                        if (BirthDate < DateTime.Parse("01.01.1900"))
                            return "Неправильное значение";
                        if (BirthDate > DateTime.Now.AddYears(-18)) // Если больше 18 лет
                            return "Постоялец должен быть совершеннолетним";
                        return "";
                    }
                    return "Поле обязательно для заполенения";
                }
                default:
                    return null;
            }
        }
    }
}
