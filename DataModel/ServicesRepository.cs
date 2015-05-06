using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ServicesRepository : IRepository<Services>
    {
        public MainEntititesContext MainContext { get; set; }

        public ServicesRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = mainContext.GetServices();
        }
        public ObservableCollection<Services> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null) ModelCollectionChanged();

            }
        }

        private ObservableCollection<Services> _collection;
        public Hotels CurrentHotel
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(Services obj)
        {
            obj.HotelID = MainContext.CurrentHotel.HotelID;
            MainContext.Context.Services.Add(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }

        public void Remove(Services obj)
        {
            if (obj.ServiceRealization.Count != 0)
            {
                throw new Exception("Удаление услуги запрещенно");
            }
            obj.HotelID = MainContext.CurrentHotel.HotelID;
                MainContext.Context.Services.Remove(obj);
                MainContext.Context.SaveChanges();
                Refresh();
        }

        public void Update()
        {
            MainContext.Context.SaveChanges();
            Refresh();
        }

        public void Refresh()
        {
            Collection = MainContext.GetServices();
        }

        public event Action ModelCollectionChanged;

        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            throw new NotImplementedException();
        }
    }
}
