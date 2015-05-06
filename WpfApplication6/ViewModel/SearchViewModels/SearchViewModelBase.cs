using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.AccessControl;
using System.Text;
using System.Windows;
using DataModel;
using Main.ViewModel.Helpers;

namespace Main.ViewModel.SearchViewModels
{
    public abstract class SearchViewModelBase<T> : ViewModelBase where T :class,new()
    {
        private string _searchWord;
        private string _searchItem;
        private string _alertText;
        private bool _comboboxIsEnabled;
        private Boolean _searchTextBoxIsEnabled;
        protected SearchViewModelBase(IRepository<T> repository, List<string> itemCollection)
        {
            Repository = repository;
            ItemCollection = itemCollection;
            FiltrationTypeString = "Равно";
        }
        public string SearchWord
        {
            get { return _searchWord; }
            set
            {
                _searchWord = value;
                RaisePropertyChanged("SearchWord");
                OnSearch();
            }
        }
        public string SearchItem
        {
            get { return _searchItem; }
            set
            {
                _searchItem = value;
                CheckComboboxStatus();

                if (SearchItem == null)
                {
                    SearchTextBoxIsEnabled = false;
                    AlertText = "Выберите колонку для фильтра";
                }
                SearchTextBoxIsEnabled = true;
            }
        }
        public string AlertText
        {
            get { return _alertText; }
            set
            {
                _alertText = value;
                RaisePropertyChanged("AlertText");
            }
        }
        public Boolean ComboboxIsEnabled {
            get { return _comboboxIsEnabled; }
            set
            {
                _comboboxIsEnabled = value;
                RaisePropertyChanged("ComboboxIsEnabled"); 
                
            }
        }
        public Boolean SearchTextBoxIsEnabled
        {
            get { return _searchTextBoxIsEnabled; }
            set
            {
                _searchTextBoxIsEnabled = value;
                RaisePropertyChanged("SearchTextBoxIsEnabled");
            }
        }

        private string _filtrationTypeString;
        public string FiltrationTypeString
        {
            get { return _filtrationTypeString; }
            set
            {
                switch (value)
                {
                    case "Равно":
                    {
                        Filtration = FiltrationType.Equals;
                        break;
                    }
                    case "Больше":
                    {
                        Filtration = FiltrationType.Greater;
                        break;
                    }
                    case "Меньше":
                    {
                        Filtration = FiltrationType.Less;
                        break;
                    }

                }
                _filtrationTypeString = value;
                RaisePropertyChanged("FiltrationTypeString");
            } } //конвертериует строку в тип сортировки
        public List<string> ItemCollection { get; set; }
        public IRepository<T> Repository { get; set; }
        public FiltrationType Filtration { get; set; }
        public void OnSearch()
        {
            Repository.Filtration(SearchWord, SearchItem, Filtration);
            if (!String.IsNullOrEmpty(SearchWord))
            {
                AlertText = Repository.Collection.Count == 0
                    ? "Ничего не найдено"
                    : new StringBuilder().Append("Найдено записей:")
                        .Append(Repository.Collection.Count.ToString())
                        .ToString();
            }
            else AlertText = "Введите текст для поиска";
        }
        public abstract void CheckComboboxStatus();

    }



}




