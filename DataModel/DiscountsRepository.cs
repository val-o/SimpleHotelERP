using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace DataModel
{
    public class DiscountsRepository : IRepository<Discounts>
    {
        public DiscountsRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = MainContext.GetDiscounts();
        }
        public MainEntititesContext MainContext { get; set; }

        private ObservableCollection<Discounts> _collection;

        public ObservableCollection<Discounts> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null) ModelCollectionChanged();
            }
        }

        public void Add(Discounts obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(Discounts obj)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
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

        public void Refresh()
        {
            Collection = MainContext.GetDiscounts();
        }
    }
}
