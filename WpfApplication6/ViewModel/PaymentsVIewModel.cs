using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using Main.View;
using Main.ViewModel.EditViewModels;
using Main.ViewModel.Helpers;
using Microsoft.Office.Interop.Excel;
using PaymentsView = DataModel.PaymentsView;


namespace Main.ViewModel
{
    public class PaymentsViewModel : EntityViewModelBase<Payment>
    {
        
        public IViewRepository<PaymentsView> PaymentsViewRepository { get; set; }
        public List<PaymentsView> PaymentsViewCollection {
            get { return PaymentsViewRepository.Collection; }
        }

        public override Payment CurrentEntityObject
        {
            get { return _currentEntity; }
            set
            {
                _currentEntity = value;
                IsAddAndEditButtonsEnabled = value != null;
                RaisePropertyChanged("CurrentEntityObject");
            }
        }

        public PaymentsViewModel(IRepository<Payment> repository, IViewRepository<PaymentsView> paymentsViewRepository, IRepository<Guests> guestsRepository, IRepository<Registration> registratinoRepository, IRepository<ServiceRealization> realizationRepository )
            : base (repository)
        {
            PaymentsViewRepository = paymentsViewRepository;
            PaymentsViewRepository.ViewCollectionChanged += () => RaisePropertyChanged("PaymentsViewCollection");
            Repository.ModelCollectionChanged += () =>  RaisePropertyChanged("PaymentsViewCollection");
                PaymentsViewRepository.Refresh();
            IsAddAndEditButtonsEnabled = false;
            registratinoRepository.ModelCollectionChanged += () =>
            {
                 PaymentsViewRepository.Refresh();
                RaisePropertyChanged("PaymentsViewCollection");
            };
            realizationRepository.ModelCollectionChanged += () =>
            {
                PaymentsViewRepository.Refresh();
                RaisePropertyChanged("PaymentsViewCollection");
            };
            GuestsRepositoryRef = guestsRepository;
        }
        public IRepository<Guests> GuestsRepositoryRef { get; set; } 
        protected override void OnAddClickedCommand()
        {
            EditViewModelInstance = new PaymentsEditViewModel(Repository, PaymentsViewRepository);
            EditViewModelInstance.CloseEvent += () => EditViewModelInstance = null;
        }
        
        protected override void OnEditClickedCommand()
        {
            throw new NotImplementedException();
        }

        private void CreateReport()
        {
            {
                var excel = new Application { Visible = true, SheetsInNewWorkbook = 1 };
                excel.Workbooks.Add(Type.Missing);
                excel.get_Range("B1", "G1").Merge(Type.Missing);
                excel.get_Range("B1", "G1").Cells.Font.Size = 14;
                excel.get_Range("B2", "G2").Interior.ColorIndex = 34;
                excel.get_Range("B1", "G1").HorizontalAlignment = Constants.xlCenter;
                excel.Cells[1, 2] = "Список должников";
                excel.Cells[2, 2] = "Имя";
                excel.Cells[2, 3] = "Фамилия";
                excel.Cells[2, 4] = "Отчество";
                excel.Cells[2, 5] = "Задолженность за проживание";
                excel.Cells[2, 6] = "Задолженность по услугам";
                excel.Cells[2, 7] = "Общая задолженность";


                excel.Columns[2].ColumnWidth =
                excel.Columns[3].ColumnWidth =
                excel.Columns[4].ColumnWidth = 15;
                excel.Columns[5].ColumnWidth = 28;
                excel.Columns[6].ColumnWidth = 26;
                excel.Columns[7].ColumnWidth = 25;


                for (var i = 0; i < PaymentsViewRepository.Collection.Count; i++)
                {
                    excel.Cells[i + 3, 2] = PaymentsViewRepository.Collection[i].Name;
                    excel.Cells[i + 3, 3] = PaymentsViewRepository.Collection[i].SecName;
                    excel.Cells[i + 3, 4] = PaymentsViewRepository.Collection[i].ThirdName;
                    excel.Cells[i + 3, 5] = new StringBuilder().Append(PaymentsViewRepository.Collection[i].RentCost).Append(" Руб.").ToString();
                    excel.Cells[i + 3, 6] = new StringBuilder().Append(PaymentsViewRepository.Collection[i].ServicesTotalCost).Append(" Руб.").ToString();
                    excel.Cells[i + 3, 7] = new StringBuilder().Append(PaymentsViewRepository.Collection[i].Debt).Append(" Руб.").ToString();
                    (excel.Cells[i + 3, 7] as Range).Interior.ColorIndex = 22;
                }

            }

        }
        public ActionCommand CreateReportCommand { get { return new ActionCommand(CreateReport) {IsExecutable = true};} }
        protected override void OnDeleteClickedCommand()
        {
            Repository.Remove(CurrentEntityObject);
            PaymentsViewRepository.Refresh();
        }


    }


}
