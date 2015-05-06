using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using DataModel;
using Main.View;
using Main.ViewModel.Helpers;

namespace Main.ViewModel.EditViewModels
{
    public class HotelEditViewModel : ViewModelBase
    {
        public IRepository<Hotels> Repository { get; set; }
        private Hotels _currentHotel;
        public string ButtonText { get; set; }
        public Hotels CurrentHotel
        {
            get { return _currentHotel; }
            set
            {
                _currentHotel = value;
                RaisePropertyChanged("CurrentHotel");
            }
        }
        public Boolean IsAddButtonEnabled { get; set; }
        public string HotelTitle
        {
            get { return CurrentHotel.HotelName; }
            set
            {
                if (String.IsNullOrEmpty(HotelTitle))
                {
                    TitleError = "Поле обязательно для ввода";
                    RaisePropertyChanged("TitleError");
                }
                else if (HotelTitle.Length < 4)
                {
                    TitleError = "Слишком короткое название";
                    RaisePropertyChanged("TitleError");
                }
                else if (HotelTitle.Length > 20)
                {
                    TitleError = "Слишком длинное название";
                    RaisePropertyChanged("TitleError");
                }
                else
                {
                    TitleError = "";
                    RaisePropertyChanged("TitleError");
                }
                CurrentHotel.HotelName = value;
                RaisePropertyChanged("HotelTitle");
                ValidateFields();
            }
        }
        public ActionCommand CloseButtonClicked { get { return new ActionCommand(Close) { IsExecutable = true }; } }
        public ActionCommand DeleteCommand { get { return new ActionCommand(Delete) { IsExecutable = true }; } }
        public ActionCommand AddOrEditCommand { get; set; }

        public void Close()
        {
            Closing();
        }
        public event Action Closing;
        public string TitleError { get; set; }
        public string HotelAddress
        {
            get { return CurrentHotel.HotelAddress; }
            set
            {
                CurrentHotel.HotelAddress = value;
                RaisePropertyChanged("HotelAddress");
                ValidateFields();
            }
        }
        public int HotelRate
        {
            get { return CurrentHotel.HotelRating; }
            set
            {
                CurrentHotel.HotelRating = value;
                RaisePropertyChanged("HotelRate");
                ValidateFields();

            }
        }

        private void ValidateFields()
        {
            IsAddButtonEnabled = !String.IsNullOrEmpty(HotelTitle) && !String.IsNullOrEmpty(HotelAddress) &&
                                 TitleError == "";
            RaisePropertyChanged("IsAddButtonEnabled");

        }
        private void Add()
        {
            Repository.Add(CurrentHotel);
        }
        private void Update()
        {
            Repository.Update();
        }
        private void Delete()
        {
            try
            {
                Repository.Remove(CurrentHotel);
            }
            catch
            {
                InfoWindow.Show("Удаление отеля невозможно");
            }
        }
        public HotelEditViewModel(IRepository<Hotels> repository)
        {
            Repository = repository;
            CurrentHotel = new Hotels();
            AddOrEditCommand = new ActionCommand(Add){IsExecutable = true};
            ButtonText = "Добавить";
        }
        public HotelEditViewModel(IRepository<Hotels> repository, Hotels hotel)
        {
            Repository = repository;
            CurrentHotel = hotel;
            AddOrEditCommand = new ActionCommand(Update) { IsExecutable = true };
            ButtonText = "Изменить";

        }
    }
}
