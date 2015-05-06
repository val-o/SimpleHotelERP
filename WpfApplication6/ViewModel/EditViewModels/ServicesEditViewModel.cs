using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel.EditViewModels
{
    public class ServicesEditViewModel : ViewModelBase
    {
        private Boolean _isAddButtonEnabled;
        private Services _entityObj;
        public ServicesEditViewModel(IRepository<Services> repository)
        {
            Repository = repository;
            EntityObj = new Services();
            AddOrEditCommand = new ActionCommand(OnAddCommand) { IsExecutable = true };
            IsEdit = false;
        }

        public ServicesEditViewModel(IRepository<Services> repository, Services editingObj)
        {
            Repository = repository;
            EntityObj = editingObj;
            AddOrEditCommand = new ActionCommand(OnEditCommand) { IsExecutable = true };
            IsEdit = true;
            ValidateAllFields();
        }

        public ObservableCollection<Services> Collection
        {
            get { return Repository.Collection; }
            set
            {
                Repository.Collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        public IRepository<Services> Repository { get; set; }
        //Текущий объект
        public Services EntityObj
        {
            get { return _entityObj; }
            set
            {
                _entityObj = value;
                RaisePropertyChanged("EntityObj");
            }
        }

        public Boolean IsEdit { get; set; }
        public Boolean IsAddButtonEnabled
        {
            get { return _isAddButtonEnabled; }
            set
            {
                _isAddButtonEnabled = value;
                RaisePropertyChanged("IsAddButtonEnabled");
            }
        }

        public string ButtonText
        {
            get { return IsEdit ? "Изменить" : "Добавить"; }
        }
        public ActionCommand AddOrEditCommand { get; set; }

        protected virtual void OnAddCommand()
        {
            Repository.Add(EntityObj);
            RaisePropertyChanged("Collection");
        }

        protected virtual void OnEditCommand()
        {
            Repository.Update();
        }

        protected void ValidateAllFields()
        {
            IsAddButtonEnabled = !String.IsNullOrEmpty(ServiceTitle) && ServiceCost != 0 &&
                                 String.IsNullOrEmpty(TitleError);

        }
        public void Close()
        {
            CloseEvent();
        }

        public string ServiceTitle
        {
            get { return EntityObj.ServiceTitle; }
            set
            {
                EntityObj.ServiceTitle = value;
                if (String.IsNullOrEmpty(value))
                    TitleError = "Поле обязательно для заполнения";
                else if (value.Length > 20)
                    TitleError = "Слишком длинное наименование";
                else if (value.Length < 4)
                    TitleError = "Слишком короткое наименование";
                else TitleError = "";
                RaisePropertyChanged("ServiceTitle");
                ValidateAllFields();
            }
        }
        public double ServiceCost
        {
            get { return EntityObj.ServiceCost; }
            set
            {
                EntityObj.ServiceCost = value;
                RaisePropertyChanged("ServiceCost");
                ValidateAllFields();
            }
        }

        public event Action CloseEvent;

        private string _titleError;

        public string TitleError
        {
            get
            {
                return _titleError;
            }
            set
            {
                _titleError = value;
                RaisePropertyChanged("TitleError");
            }
        }

    }
}

