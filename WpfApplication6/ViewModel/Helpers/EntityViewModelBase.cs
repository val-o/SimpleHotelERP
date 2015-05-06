using System;
using System.Collections.ObjectModel;
using DataModel;
using Main.ViewModel.EditViewModels;

namespace Main.ViewModel.Helpers
{
    public abstract class EntityViewModelBase<T> : ViewModelBase where T : class, new()
    {
        protected T _currentEntity;
        private Boolean _isAddAndEditButtonsEnabled = true;
        private EditViewModelBase<T> _editViewModelInstance;

        protected EntityViewModelBase(IRepository<T> repository)
        {
            Repository = repository;
            Repository.ModelCollectionChanged += () => RaisePropertyChanged("Collection");
        }
        public Boolean IsAddAndEditButtonsEnabled
        {
            get { return _isAddAndEditButtonsEnabled; }
            set
            {
                _isAddAndEditButtonsEnabled = value;
                RaisePropertyChanged("IsAddAndEditButtonsEnabled");
            }
        }
        public IRepository<T> Repository { get; set; }
        public ObservableCollection<T> Collection
        {
            get
            {
                return Repository.Collection;
            }
            set
            {
                Repository.Collection = value;
            }
        } 
        public ActionCommand AddClickedCommand { get { return new ActionCommand(OnAddClickedCommand) { IsExecutable = true }; } }
        public ActionCommand EditClickedCommand { get { return new ActionCommand(OnEditClickedCommand) { IsExecutable = true }; } }
        public ActionCommand DeleteClickedCommand { get { return new ActionCommand(OnDeleteClickedCommand) { IsExecutable = true }; } }
        public EditViewModelBase<T> EditViewModelInstance
        {
            get
            {
                return _editViewModelInstance;
            }
            set
            {
                _editViewModelInstance = value;
                RaisePropertyChanged("EditViewModelInstance");
                IsAddAndEditButtonsEnabled = _editViewModelInstance == null;
            }
        }
        public virtual T CurrentEntityObject
        {
            get { return _currentEntity; }
            set
            {
                _currentEntity = value;
                IsAddAndEditButtonsEnabled = value != null;
                RaisePropertyChanged("CurrentEntityObject");
                if (CurrentEntityObjectChanged != null) CurrentEntityObjectChanged();
            }
        }
        public event Action CurrentEntityObjectChanged;
        protected abstract void OnAddClickedCommand();
        protected abstract void OnEditClickedCommand();
        protected abstract void OnDeleteClickedCommand();

    }
}
