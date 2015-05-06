using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class GuestsDiscountsRepository: IRepository<GuestsDiscounts>
    {
        private ObservableCollection<GuestsDiscounts> _collection;
        public MainEntititesContext MainContext { get; set; }

        public GuestsDiscountsRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = MainContext.GetGuestsDiscounts();
        }

        public ObservableCollection<GuestsDiscounts> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null) ModelCollectionChanged();
            }
        }
        public Hotels CurrentHotel
        {
            get { throw new NotImplementedException(); }
        }
        public void Add(GuestsDiscounts obj)
        {
            MainContext.Context.GuestsDiscounts.Add(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }
        public void Remove(GuestsDiscounts obj)
        {
            MainContext.Context.GuestsDiscounts.Remove(obj);
            Update();
        }
        public void Update()
        {
            MainContext.Context.SaveChanges();
            Refresh();
        }
        public void Refresh()
        {
            Collection = MainContext.GetGuestsDiscounts();
        }
        public event Action ModelCollectionChanged;
        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            throw new NotImplementedException();
        }
    }
}
