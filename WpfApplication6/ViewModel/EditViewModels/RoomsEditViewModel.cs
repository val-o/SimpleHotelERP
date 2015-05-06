using System;
using System.Collections.ObjectModel;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel.EditViewModels
{
    //реализовать остальные методы

    public class RoomsEditViewModel : EditViewModelBase<Rooms>
    {
        //конструктор при добавлении
        public RoomsEditViewModel(IRepository<Rooms> roomsRepository, IRepository<TypesOfRooms> typesRepository)
            : base(roomsRepository)
        {
            TypesRepository = typesRepository;
        }
        //Конструктор при изменении
        public RoomsEditViewModel(IRepository<Rooms> roomsRepository, IRepository<TypesOfRooms> typesRepository, Rooms room) 
            : base(roomsRepository, room)
        {
            TypesRepository = typesRepository; 
        }
        public Rooms Room
        {
            get { return EntityObj; }
            set
            {
                EntityObj = value;
                RaisePropertyChanged("Room");
            }
        }
        public IRepository<TypesOfRooms> TypesRepository { get; set; }
        public ObservableCollection<TypesOfRooms> TypesOfRoomsCollection
        {
            get { return TypesRepository.Collection; }
            set
            {
                TypesRepository.Collection = value;
                RaisePropertyChanged("TypesOfRoomsCollection");
            }
        } 
        public string RoomNum
        {
            get { return Room.RoomNum; }
            set
            {
                Room.RoomNum = value;
                RaisePropertyChanged("RoomNum");
                ValidateAllFields();
            }
        }
        public Byte? NumberOfPlaces
        {
            get { return Room.NumberOfPlaces; }
            set
            {
                Room.NumberOfPlaces = value;
                RaisePropertyChanged("NumberOfPlaces");
                ValidateAllFields();

            }
        }
        public Int32? TypeID
        {
            get { return Room.TypeID; }
            set
            {
                Room.TypeID = value;
                RaisePropertyChanged("TypeID");
                ValidateAllFields();

            }
        }
        public Int32? CostPerDay
        {
            get { return Room.CostPerDay; }
            set
            {
                Room.CostPerDay = value;
                RaisePropertyChanged("CostPerDay");
                ValidateAllFields();

            }
        }
        public TypesOfRooms TypesOfRooms
        {
            get { return Room.TypesOfRooms; }
            set
            {
                Room.TypesOfRooms = value;
                RaisePropertyChanged("TypesOfRooms");
                ValidateAllFields();

            }
        }
        protected override string IsValid(string propertyName)
        {
            switch (propertyName)
            {
                case "RoomNum":
                {
                    if (!string.IsNullOrEmpty(RoomNum))
                    {
                        if (RoomNum.Length > 4)
                            return "Слишком длинный номер";
                        return "";
                    }
                    return "Поле обязательно для заполнения";

                }
                default: throw new Exception();
            }
        }
        protected override void ValidateAllFields()
        {
            IsAddButtonEnabled = !String.IsNullOrEmpty(RoomNum) && this["RoomNum"] == "" && TypesOfRooms != null &&
                                 NumberOfPlaces != 0 && CostPerDay != 0;
        }
    }

}
