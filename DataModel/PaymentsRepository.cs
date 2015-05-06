using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DataModel
{
    public class PaymentsRepository : IRepository<Payment>
    {
        public PaymentsRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = MainContext.GetPayments();
        }
        public MainEntititesContext MainContext { get; set; }
        private ObservableCollection<Payment> _collection; 
        public ObservableCollection<Payment> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null)
                ModelCollectionChanged(); 
            }
        }


        public ObservableCollection<Payment> FreshCollection
        {
            get { return MainContext.GetPayments(); }
        }
        
        public void Add(Payment obj)
        {
            obj.HotelID = MainContext.CurrentHotel.HotelID;
            MainContext.Context.Payment.Add(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }

        public void Remove(Payment obj)
        {
            MainContext.Context.Payment.Remove(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            Collection = MainContext.GetPayments();
        }

        public event Action ModelCollectionChanged;

        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            throw new NotImplementedException();
        }


        public Hotels CurrentHotel
        {
            get { throw new NotImplementedException(); }
        }
    }
}
