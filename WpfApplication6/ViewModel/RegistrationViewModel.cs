using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using DataModel;
using Main.ViewModel.EditViewModels;
using Main.ViewModel.Helpers;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace Main.ViewModel
{
    public class RegistrationViewModel : EntityViewModelBase<Registration>
    {
        private RegistrationView _currentViewObject;
        private bool _comboboxIsEnabled;
        private bool _isDatePickerEnabled;
        private DateTime _searchDateTime;
        private DateTime _endDateTime;
        private bool _isTextBoxEnabled;
        private string _searchTextBox;
        private string _selectedItem;
        private string _filtrationTypeString;
        private bool _isDelButtonEnabled;

        public bool IsDelButtonEnabled
        {
            get { return _isDelButtonEnabled; }
            set
            {
                _isDelButtonEnabled = value;
                RaisePropertyChanged("IsDelButtonEnabled");
            }
        }

        public RegistrationViewModel(IRepository<Registration> repository,
           IViewRepository<RegistrationView> viewRepository,
           RoomsRepository rooomsRepository,
           IRepository<Guests> guestsRepository,
            IRepository<Discounts> discountsRepository,
            IRepository<GuestsDiscounts> guestsDiscountsRepository)
            : base(repository)
        {
            RoomsRepository = rooomsRepository;
            GuestsRepository = guestsRepository;
            ViewRepository = viewRepository;
            DiscountsRepository = discountsRepository;
            GuestsDiscountsRepository = guestsDiscountsRepository;
            repository.ModelCollectionChanged += () =>
            {
                ViewRepository.Refresh();
                RaisePropertyChanged("ViewCollection");
            };
            GuestsRepository.ModelCollectionChanged += () =>
            {
                ViewRepository.Refresh();
                RaisePropertyChanged("ViewCollection");
            };
            rooomsRepository.ModelCollectionChanged += () =>
            {
                ViewRepository.Refresh();
                RaisePropertyChanged("ViewCollection");
            };
            ViewRepository.ViewCollectionChanged += () => RaisePropertyChanged("ViewCollection");

        }
        public RegistrationView CurrentViewObject {
            get { return _currentViewObject; }
            set
            {
                _currentViewObject = value;
                IsDelButtonEnabled = value!= null;
                RaisePropertyChanged("CurrentViewObject");
                RaisePropertyChanged("CurrentEntityObject");
            }
            }
        public override Registration CurrentEntityObject {
            get { return CurrentViewObject != null ?  Collection.Single(obj => obj.RegistrationID == CurrentViewObject.RegistrationID) : null ; }
        }
        public IRepository<Rooms> RoomsRepository { get; set; }
        public IRepository<Guests> GuestsRepository { get; set; }
        public IRepository<Discounts> DiscountsRepository { get; set; } 
        public IViewRepository<RegistrationView> ViewRepository { get; set; }
        public IRepository<GuestsDiscounts> GuestsDiscountsRepository { get; set; }
        public List<RegistrationView> ViewCollection { get { return ViewRepository.Collection; } } 
        protected override void OnAddClickedCommand()
        {
            IsDelButtonEnabled = false;
            EditViewModelInstance = new RegistrationEditViewModel(Repository,RoomsRepository, GuestsRepository, DiscountsRepository, GuestsDiscountsRepository);
            EditViewModelInstance.CloseEvent += () =>
            {
                EditViewModelInstance = null;
                IsDelButtonEnabled = true;
            };
        }
        protected override void OnEditClickedCommand()
        {
            //check!
            EditViewModelInstance = new RegistrationEditViewModel(Repository, RoomsRepository, GuestsRepository, DiscountsRepository, GuestsDiscountsRepository );
        }
        protected override void OnDeleteClickedCommand()
        {
            Repository.Remove(CurrentEntityObject);
            ViewRepository.Refresh();
        }
        public string SearchTextBox
        {
            get
            {
                return _searchTextBox;
            }
            set
            {
                _searchTextBox = value;
                RaisePropertyChanged("SearchTextBox");
                ViewRepository.Filtration(value, SelectedItem, Filtration);

            }
        }
        public DateTime SearchDateTime
        {
            get { return _searchDateTime; }
            set
            {
                _searchDateTime = value;
                SearchTextBox = value.ToShortDateString();
            }
        }
        public bool IsDatePickerEnabled
        {
            get
            {
                return _isDatePickerEnabled;
            }
            set
            {
                _isDatePickerEnabled = value;
                RaisePropertyChanged("IsDatePickerEnabled");
            }
        }
        public bool ComboboxIsEnabled
        {
            get
            {
                return _comboboxIsEnabled;
            }
            set
            {
                _comboboxIsEnabled = value;
                RaisePropertyChanged("ComboboxIsEnabled");
            }
        }
        public string SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                if (!String.IsNullOrEmpty(value))
                {
                    ComboboxIsEnabled = true;
                    IsTextBoxEnabled = true;
                }
                if (value == "Постоялец")
                {
                    FiltrationTypeString = "Равно";
                    ComboboxIsEnabled = false;
                    IsDatePickerEnabled = false;
                    IsTextBoxEnabled = true;
                }
                if (value == "Дата заселения" || value == "Дата заселения")
                {
                    IsTextBoxEnabled = false;
                    IsDatePickerEnabled = true;
                }
                RaisePropertyChanged("SelectedItem");
                FiltrationTypeString = "Равно";

            }
        }
        public bool IsTextBoxEnabled
        {
            get
            {
                return _isTextBoxEnabled;
            }
            set
            {
                _isTextBoxEnabled = value;
                RaisePropertyChanged("IsTextBoxEnabled");
            }
        }
        public List<string> ItemsList {get {return new List<string>(){"Постоялец", "Дата заселения", "Дата выселения"};}} 
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
            }
        } //конвертериует строку в тип сортировки
        public FiltrationType Filtration { get; set; }


        private void CreateReport()
        {
            {
                var excel = new Application { Visible = true, SheetsInNewWorkbook = 1 };
                excel.Workbooks.Add(Type.Missing);
                excel.get_Range("B1", "G1").Merge(Type.Missing);
                excel.get_Range("B1", "G1").Cells.Font.Size = 14;
                excel.get_Range("B2", "G2").Interior.ColorIndex = 34;
                excel.get_Range("B1", "H1").HorizontalAlignment = Constants.xlCenter;
                excel.Cells[1, 2] = "Регистрация";
                excel.Cells[2, 2] = "Имя";
                excel.Cells[2, 3] = "Фамилия";
                excel.Cells[2, 4] = "Отчество";
                excel.Cells[2, 5] = "Дата заселения";
                excel.Cells[2, 6] = "Дата выселения";
                excel.Cells[2, 7] = "Скидка";
                excel.Cells[2, 8] = "Стоимость проживания";


                excel.Columns[2].ColumnWidth =
                excel.Columns[3].ColumnWidth =
                excel.Columns[4].ColumnWidth = 15;
                excel.Columns[5].ColumnWidth = 28;
                excel.Columns[6].ColumnWidth = 26;
                excel.Columns[7].ColumnWidth = 15;
                excel.Columns[8].ColumnWidth = 25;


                for (var i = 0; i < ViewRepository.Collection.Count; i++)
                {
                    excel.Cells[i + 3, 2] = ViewRepository.Collection[i].Name;
                    excel.Cells[i + 3, 3] = ViewRepository.Collection[i].SecName;
                    excel.Cells[i + 3, 4] = ViewRepository.Collection[i].ThirdName;
                    excel.Cells[i + 3, 5] = ViewRepository.Collection[i].EntryDate;
                    excel.Cells[i + 3, 6] = ViewRepository.Collection[i].ExitDate;
                    excel.Cells[i + 3, 7] = ViewRepository.Collection[i].DiscountTitle ?? "Отсутсвует";
                    excel.Cells[i + 3, 8] = new StringBuilder().Append(ViewRepository.Collection[i].TotalCost).Append(" Руб.").ToString();
                    (excel.Cells[i + 3, 8] as Range).Interior.ColorIndex = 22;
                }

            }

        }
        public ActionCommand CreateReportCommand { get { return new ActionCommand(CreateReport) { IsExecutable = true }; } }

    }
}
