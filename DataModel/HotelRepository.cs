using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class HotelsRepository:IRepository<Hotels>
    {
        public MainEntititesContext MainContext { get; set; }
        private ObservableCollection<Hotels> _collection; 
        public HotelsRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = MainContext.GetHotels();
        }
        public ObservableCollection<Hotels> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null) ModelCollectionChanged();
            }
        }
        public void Add(Hotels obj)
        {
            MainContext.Context.Hotels.Add(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }
        public void Remove(Hotels obj)
        {
            if (obj.Discounts.Count != 0 &&
                obj.Registration.Count != 0 &&
                obj.Payment.Count != 0 &&
                obj.Rooms.Count != 0 &&
                obj.Services.Count != 0)
            {
                throw new Exception("Object has bounds");
            }
            MainContext.Context.Hotels.Remove(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }
        public void Update()
        {
            MainContext.Context.SaveChanges();
            Refresh();
        }

        public event Action ModelCollectionChanged;

        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            throw new NotImplementedException();
        }
        public void Refresh()
        {
            Collection = MainContext.GetHotels();
        }
        public Hotels CurrentHotel
        {
            get { return MainContext.CurrentHotel; }
        }
    }
}
