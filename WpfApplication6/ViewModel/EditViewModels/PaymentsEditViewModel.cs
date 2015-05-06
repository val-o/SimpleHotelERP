using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DataModel;

namespace Main.ViewModel.EditViewModels
{
    public class PaymentsEditViewModel : EditViewModelBase<Payment>
    {
        private PaymentsView _currentPaymentsView;

        public PaymentsEditViewModel(IRepository<Payment> repository, IViewRepository<PaymentsView> paymentViewRepository)
            : base(repository)
        {
            ViewRepository = paymentViewRepository;
            PaymentsGuestsCollection = ViewRepository.Collection;
            EntityObj.PaymentDate = DateTime.Now;
            Repository.ModelCollectionChanged += () => RaisePropertyChanged("Collection");
            ViewRepository.ViewCollectionChanged +=
                () => RaisePropertyChanged("PaymentsGuestsCollection");
        }
        public IViewRepository<PaymentsView> ViewRepository { get; set; } 

        public PaymentsView CurrentPaymentsView
        {
            get
            {
                return _currentPaymentsView;
            }
            set
            {
                _currentPaymentsView = value;
                RaisePropertyChanged("CurrentPaymentsView");
                if (CurrentPaymentsView != null)
                EntityObj.GuestID = CurrentPaymentsView.GuestID;
                ValidateAllFields();
            }
        }

        public double PaymentSize
        {
            get { return EntityObj.PaymentSize; }
            set
            {
                EntityObj.PaymentSize = value;
                RaisePropertyChanged("PaymentSize");
                ValidateAllFields();
            }
        }

        public string PaymentComment
        {
            get
            {
                return EntityObj.PaymentComment;
            }
            set
            {
                EntityObj.PaymentComment = value;
                RaisePropertyChanged("PaymentComment");
            }
        }

        public DateTime PaymentDate
        {
            get { return EntityObj.PaymentDate; }
            set
            {
                EntityObj.PaymentDate = value;
            }
        }

        public List<PaymentsView> PaymentsGuestsCollection
        {
            get
            {
                return ViewRepository.Collection;
                
            }
            set
            {
                ViewRepository.Collection = value;
                RaisePropertyChanged("PaymentsGuestsCollection");
            }
        }

        protected override void ValidateAllFields()
        {
            IsAddButtonEnabled = CurrentPaymentsView != null && this["PaymentSize"] == "";
        }

        protected override void OnAddCommand()
        {
            Repository.Add(EntityObj);
            ViewRepository.Refresh();
        }

        protected override string IsValid(string propertyName)
        {
            switch (propertyName)
            {
                case "PaymentSize":
                {
                    if (PaymentSize != 0)
                    {
                        if (PaymentSize > CurrentPaymentsView.Debt)
                        {
                            return "Слишком большой платеж";
                        }
                        return "";
                    }
                    return "Поле обязательно для заполенения";
                }
                default: throw new Exception();
            }

        }

        
        public override string HeaderText
        {
            get { return IsEdit ? "Изменить платеж" : "Внести платеж"; }
        }
        }
}
