using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataModel
{
    public class GuestsRepository : IRepository<Guests>
    {
        public GuestsRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = MainContext.GetGuests();
        }
        public MainEntititesContext MainContext { get; set; }
        private ObservableCollection<Guests> _collection;
        public ObservableCollection<Guests> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null) ModelCollectionChanged();
            }
        }
        public void Add(Guests obj)
        {
            MainContext.Context.Guests.Add(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }

        public void Remove(Guests obj)
        {
            if (obj.Payment.Count != 0 ||
                obj.Registration.Count != 0 ||
                obj.Wishes.Count != 0 ||
                obj.ServiceRealization.Count != 0)
                throw new Exception("Удаление гостя невозможно"); //Удаление невозможно, бросаем исключение
            while (obj.GuestsDiscounts.Count != 0)
                MainContext.Context.GuestsDiscounts.Remove(obj.GuestsDiscounts.First());
            MainContext.Context.Guests.Remove(obj);
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
            
        }
        public ObservableCollection<Guests> GetFreeGuestsCollection (DateTime? starDate, DateTime? endDate)
        {
            var freeGuestsCollection = new ObservableCollection<Guests>();
            foreach (var guest in Collection)
            {
                if (guest.Registration.Count(reg => reg.EntryDate >= endDate || reg.ExitDate <= starDate) != 0 || guest.Registration.Count == 0)
                {
                    freeGuestsCollection.Add(guest);
                }
            }
            return freeGuestsCollection;
        }

        public ObservableCollection<Guests> GetGuestsByDate(DateTime date)
        {
            var freeGuestsCollection = new ObservableCollection<Guests>();
            foreach (var guest in Collection)
            {
                if (guest.Registration.Count(reg => reg.EntryDate <= date || reg.ExitDate >= date) != 0)
                {
                    freeGuestsCollection.Add(guest);
                }
            }
            return freeGuestsCollection;
        }
        public Hotels CurrentHotel { get { return MainContext.CurrentHotel; } }

        public void Refresh()
        {
            Collection = MainContext.GetGuests();
        }
    }

  
}
