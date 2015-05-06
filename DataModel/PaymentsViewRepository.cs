using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class PaymentsViewRepository : IViewRepository<PaymentsView>
    {
        public MainEntititesContext MainContext { get; set; }
        private List<PaymentsView> _collection;
        public List<PaymentsView> Collection
        {
            get
            {
                return _collection;
            }
            set
            {
                _collection = value;
                if (ViewCollectionChanged != null)
                    ViewCollectionChanged();
            }
        }

        public PaymentsViewRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = MainContext.GetPaymentsView();
        }
        public event Action ViewCollectionChanged;
        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            if (!String.IsNullOrEmpty(searchWord))
            {
              
            }
            Refresh();
        }
        public void Refresh()
        {
            Collection = MainContext.GetPaymentsView();
        }



    }
}
