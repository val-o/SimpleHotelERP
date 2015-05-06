using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DataModel;
using Main.ViewModel.Helpers;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;
using Border = System.Windows.Controls.Border;

namespace Main.ViewModel
{
    public class PaymentReportViewModel : ViewModelBase

    {
        private DateTime _startDate;
        private DateTime _endDate;

        public DateTime StartTime
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public DateTime EndTime
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public IViewRepository<PaymentsView> Repository { get; set; }  
        public PaymentReportViewModel(IViewRepository<PaymentsView> paymentsViewRepository)
        {
            Repository = paymentsViewRepository;
        }
        public ActionCommand CreateReportCommand { get { return new ActionCommand(CreateReport) { IsExecutable = true }; } }
        private void CreateReport()
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


            for (var i = 0; i < Repository.Collection.Count; i++)
            {
                excel.Cells[i + 3, 2] = Repository.Collection[i].Name;
                excel.Cells[i + 3, 3] = Repository.Collection[i].SecName;
                excel.Cells[i +3, 4] = Repository.Collection[i].ThirdName;
                excel.Cells[i + 3, 5] = new StringBuilder().Append(Repository.Collection[i].RentCost).Append(" Руб.").ToString();
                excel.Cells[i + 3, 6] = new StringBuilder().Append(Repository.Collection[i].ServicesTotalCost).Append(" Руб.").ToString();
                excel.Cells[i + 3, 7] = excel.Cells[i + 3, 6] = new StringBuilder().Append(Repository.Collection[i].Debt).Append(" Руб.").ToString();
                (excel.Cells[i + 3, 7] as Range).Interior.ColorIndex = 22;
            }

        }
    }
}
