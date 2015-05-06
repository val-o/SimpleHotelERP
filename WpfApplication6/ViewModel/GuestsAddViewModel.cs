using System;
using System.ComponentModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using DataModel;
using Main.ViewModel.EditViewModels;
using Main.ViewModel.Helpers;

namespace Main.ViewModel
{

    /*
    public class GuestsAddViewModel : ViewModelBase, IDataErrorInfo
    {
        public GuestsRepository2 Repository;
        #region .ctors
        public GuestsAddViewModel()
        {
            AddViewHeader = "Добвавление нового постояльца";
            Guest = new Guests();
            AddGuestCommand = new ActionCommand(OnAddGuuestCommand) { IsExecutable = true };
            Cancel = new ActionCommand(OnCancel){IsExecutable = true};
        }
        public GuestsAddViewModel(Guests guest)
        {
            AddViewHeader = "Измение данных о постояльце";
            Guest = guest;
            AddGuestCommand = new ActionCommand(OnEdit) { IsExecutable = true };
            Cancel = new ActionCommand(OnCancel) { IsExecutable = true };
        }
        #endregion

        private ErrorViewModel _errorViewModel;
        public ErrorViewModel ErrorViewModelInstance
        {
            get
            {
                return _errorViewModel;
            }
            set
            {
                _errorViewModel = value;
                RaisePropertyChanged("ErrorViewModelInstance");
            } 
        }

        public string AddViewHeader { get; set; }
        public ActionCommand AddGuestCommand { get; set; }
        public ActionCommand Cancel { get; set; }
        public Guests Guest { get; set; }
        public event Action EdititngEndsEvent;
        
        #region Guest Properties
        public string GuestName
        {
            get { return Guest.Name; }
            set
            {
                    Guest.Name = value;
                    RaisePropertyChanged("GuestName");
                    AllFieldsAerFilled();
            }
        }
        public string SecName
        {
            get { return Guest.SecName; }
            set
            {
                Guest.SecName = value;
                RaisePropertyChanged("SecName");
                AllFieldsAerFilled();
            }
        }
        public string ThirdName
        {
            get { return Guest.ThirdName; }
            set
            {
                Guest.ThirdName = value;
                RaisePropertyChanged("ThirdName");
                AllFieldsAerFilled();
            }
        }
        public string PassData
        {
            get { return Guest.PassportData; }
            set
            {
                Guest.PassportData = value;
                RaisePropertyChanged("PassData");
                AllFieldsAerFilled();
            }


        }
        public string Address
        {
            get { return Guest.Address; }
            set
            {
                Guest.Address = value;
                RaisePropertyChanged("Address");
                AllFieldsAerFilled();
            }
        }
        public string PhoneNumber
        {
            get { return Guest.PhoneNumber; }
            set
            {
                Guest.PhoneNumber = value;
                RaisePropertyChanged("PhoneNumber");
                AllFieldsAerFilled();
            }
        }
        public DateTime? BirthDate
        {
            get { return Guest.BirthDate; }
            set
            {
                Guest.BirthDate = value;
                RaisePropertyChanged("BirthDate");
                AllFieldsAerFilled();
            }
        }

        #endregion
        public void AllFieldsAerFilled()
        {
            AddButIsEnabled = this["GuestName"] == "" &&
                              this["SecName"] == "" &&
                              this["ThirdName"] == "" &&
                              this["PassData"] == "" &&
                              this["Address"] == "" &&
                              this["PhoneNumber"] == "" &&
                              this["BirthDate"] == "" &&
                              GuestName != null && SecName != null && ThirdName != null &&
                              PassData != null && Address != null && PhoneNumber != null &&
                              BirthDate != null && GuestName != "" && SecName != "" && ThirdName != "" &&
                              PassData != "" && Address != "" && PhoneNumber != "";
            if (GuestName != null && SecName != null && ThirdName != null &&
                PassData != null && Address != null && PhoneNumber != null &&
                BirthDate != null && GuestName != "" && SecName != "" && ThirdName != "" &&
                              PassData != "" && Address != "" && PhoneNumber != "")
            {
                ErrorViewModelInstance = null;
                return;
            }
            if (ErrorViewModelInstance == null)
            ErrorViewModelInstance = new ErrorViewModel(){ErrorText = "Не все поля заполенены!"};
        }

        private Boolean _addButIsEnabled;
        public Boolean AddButIsEnabled
        {
            get { return _addButIsEnabled; }
            set
            {
                _addButIsEnabled = value;
                RaisePropertyChanged("AddButIsEnabled");
            }
        }
        private void OnAddGuuestCommand()
        {
            Repository.AddGuest(Guest);
        }
        private void OnEdit()
        {
            Repository.UpdateGuest(Guest);
            Repository.RefreshCollection();
            EdititngEndsEvent();

        }
        private void OnCancel()
        {

        }

        #region IDataErrorInfo Implementation

        //IDataErrorInfo Implemetion
        public string Error { get; set; }
        public string this[string propertyName]
        {
            get { return IsValid(propertyName); }
        }
        private string IsValid(string propertyName)
        {
            switch (propertyName)
            {
                case "GuestName":
                {
                    if (GuestName != null)
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
                    break;
                }

                case "SecName":
                {
                    if (SecName != null)
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
                    break;
                }

                case "ThirdName":
                {
                    if (SecName != null)
                    {
                        if (SecName.Length > 20)
                        {
                            return "Слишком длинное отчество";
                        }
                        if (SecName.Length < 2)
                        {
                            return "Слишком короткое отчество";
                        }
                        return "";
                    }
                    break;
                }

                case "PassData":
                {
                        return "";
                }

                case "Address":
                {
                    return "";
                }
                
                case "PhoneNumber":
                {
                    return "";
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
                    break;
                }
                default:
                    return null;
            }
            return null;
        }
    }
        #endregion*/
}
