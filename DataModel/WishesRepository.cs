using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class WishesRepository:IRepository<Wishes>
    {

        public MainEntititesContext MainContext { get; set; }

        public WishesRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = MainContext.GetWishes();
        }

        private ObservableCollection<Wishes> _collection;
        public ObservableCollection<Wishes> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null)
                    ModelCollectionChanged();
            }
        }

        public ObservableCollection<Wishes> FreshCollection
        {
            get { return MainContext.GetWishes(); }
        }


        public void Add(Wishes obj)
        {
            obj.HotelID = MainContext.CurrentHotel.HotelID;
            MainContext.Context.Wishes.Add(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }

        public void Remove(Wishes obj)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            Collection = MainContext.GetWishes();
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
