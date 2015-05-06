using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel.EditViewModels
{
    public abstract class EditViewModelBase<T> : ViewModelBase, IDataErrorInfo where T : class, new()
    {
        #region Private Fields
        private Boolean _isAddButtonEnabled;
        private T _entityObj;
        #endregion
        //Repository Main Collection
        public ObservableCollection<T> Collection
        {
            get { return Repository.Collection; }
            set
            {
                Repository.Collection = value;
                RaisePropertyChanged("Collection");
            }
        }
        public IRepository<T> Repository { get; set; }
        //Текущий объект
        public T EntityObj
        {
            get { return _entityObj; }
            set
            {
                _entityObj = value;
                RaisePropertyChanged("EntityObj");
            }
        }
        public Boolean IsEdit { get; set; }

        #region View Properties

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
        public virtual string HeaderText
        {
            get { return IsEdit ? "Изменение" : "Добавление"; }
        }

        #endregion
        public ActionCommand AddOrEditCommand { get; set; }
        public ActionCommand CancelCommand { get { return new ActionCommand(OnCancelCommand) {IsExecutable = true}; } }

        protected EditViewModelBase(IRepository<T> repository)
        {
            Repository = repository;
            EntityObj = new T();
            AddOrEditCommand = new ActionCommand(OnAddCommand) { IsExecutable = true };
            IsEdit = false;
        }
        protected EditViewModelBase(IRepository<T> repository, T editingObj)
        {
            Repository = repository;
            EntityObj = editingObj;
            AddOrEditCommand = new ActionCommand(OnEditCommand) { IsExecutable = true };
            IsEdit = true;
        }

        protected virtual void OnAddCommand()
        {
            Repository.Add(EntityObj);
            RaisePropertyChanged("Collection");
        }
        protected virtual void OnCancelCommand()
        {
            
        }
        protected virtual void OnEditCommand()
        {
            Repository.Update();
        }
        protected abstract void ValidateAllFields();

        public void Close()
        {
            CloseEvent();
        }
        public event Action CloseEvent;

        #region IDataErrorInfo implementation
        public string Error { get; set; }
        public string this[string columnName]
        {
            get { return IsValid(columnName); }
        }
        protected abstract string IsValid(string propertyName);
        #endregion
    }


}
