using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DataModel;
using Main.ViewModel.EditViewModels;
using Main.ViewModel.Helpers;
using Main.ViewModel.SearchViewModels;

namespace Main.ViewModel
{
    
    //доделать
    public class RoomsViewModel : EntityViewModelBase<Rooms>
    {
        public RoomsViewModel(IRepository<Rooms> repository, IRepository<TypesOfRooms> typesOfRoomsRepository)
            : base(repository)
        {
            IsAddAndEditButtonsEnabled = false;
            TypesOfRoomsRepository = typesOfRoomsRepository;
        }

        public override Rooms CurrentEntityObject
        {
            get { return _currentEntity; }
            set
            {
                _currentEntity = value;
                IsAddAndEditButtonsEnabled = value != null;
                RaisePropertyChanged("CurrentEntityObject");
            }
        }
        public IRepository<TypesOfRooms> TypesOfRoomsRepository { get; set; }
        public ObservableCollection<TypesOfRooms> TypesOfRoomsCollection
        {
            get { return TypesOfRoomsRepository.Collection; }
            set
            {
                TypesOfRoomsRepository.Collection = value;
                RaisePropertyChanged("TypesOfRoomsCollection");
            }
        }
        protected override void OnAddClickedCommand()
        {
            EditViewModelInstance = new RoomsEditViewModel(Repository, TypesOfRoomsRepository); //создаем модель представления изменения или добавлния кмоанты
            EditViewModelInstance.CloseEvent += () => EditViewModelInstance = null; //подписываем метод удаления модели представления на соьытия закрытия предсталения
        }
        protected override void OnEditClickedCommand()
        {
            EditViewModelInstance = new RoomsEditViewModel(Repository, TypesOfRoomsRepository, CurrentEntityObject);
            EditViewModelInstance.CloseEvent += () => EditViewModelInstance = null; //подписываем метод удаления модели представления на соьытия закрытия предсталения
        }
        protected override void OnDeleteClickedCommand()
        {
            try
            {
                Repository.Remove(CurrentEntityObject);
            }
            catch
            {
                MessageBox.Show("Удаление невозможно");
            }
        }

        public RoomsSearchViewModel RoomsSearchViewModelInstance
        {
            get { return new RoomsSearchViewModel(Repository);}
        }
    }

}
