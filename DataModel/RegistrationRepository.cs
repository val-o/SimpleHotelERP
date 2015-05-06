using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataModel
{
  
    public class FreeRoomsViewRepository : IViewRepository<FreeRoomsView>
    {
        public MainEntititesContext MainContext { get; set; }

        public List<FreeRoomsView> Collection { get; set; }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public event Action ViewCollectionChanged;


        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            throw new NotImplementedException();
        }
    }

    public class RegistrationViewRepository : IViewRepository<RegistrationView>
    {
        public RegistrationViewRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = mainContext.GetRegistrationView();
        }
        public MainEntititesContext MainContext { get; set; }
        private List<RegistrationView> _collection;

        public List<RegistrationView> Collection
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

        public void Refresh()
        {
            Collection = MainContext.GetRegistrationView();
        }


        public event Action ViewCollectionChanged;


        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            if (!String.IsNullOrEmpty(searchWord))
            {
                Refresh();
                var searchResultCollection = new List<RegistrationView>();
                switch (searchItem)
                {
                       
                    case "Постоялец":
                        {
                            foreach (var reg in Collection.Where(reg => reg.SecName.ToLower().Contains(searchWord.ToLower())))
                            {
                                searchResultCollection.Add(reg);
                            }
                            Collection = searchResultCollection;
                            return;
                        }
                    case "Дата заселения":
                    {
                        switch (filtrationType)
                        {
                            case FiltrationType.Equals:
                            {
                                foreach (var reg in Collection.Where(reg => reg.EntryDate == DateTime.Parse(searchWord))
                                    )
                                {
                                    searchResultCollection.Add(reg);
                                }
                                Collection = searchResultCollection;
                                return;
                            }
                            case FiltrationType.Greater:
                            {
                                foreach (var reg in Collection.Where(reg => reg.EntryDate > DateTime.Parse(searchWord)))
                                {
                                    searchResultCollection.Add(reg);
                                }
                                Collection = searchResultCollection;
                                return;
                            }
                            case FiltrationType.Less:
                            {
                                foreach (var reg in Collection.Where(reg => reg.EntryDate < DateTime.Parse(searchWord)))
                                {
                                    searchResultCollection.Add(reg);
                                }
                                Collection = searchResultCollection;
                                return;
                            }
                        }
                    }
                        break;
                    case "Дата выселения":
                    {
                        switch (filtrationType)
                        {
                            case FiltrationType.Equals:
                            {
                                foreach (var reg in Collection.Where(reg => reg.ExitDate == DateTime.Parse(searchWord)))
                                {
                                    searchResultCollection.Add(reg);
                                }
                                Collection = searchResultCollection;
                                return;
                            }

                            case FiltrationType.Greater:
                            {
                                foreach (var reg in Collection.Where(reg => reg.ExitDate > DateTime.Parse(searchWord)))
                                {
                                    searchResultCollection.Add(reg);
                                }
                                Collection = searchResultCollection;
                                return;
                            }
                            case FiltrationType.Less:
                            {
                                foreach (var reg in Collection.Where(reg => reg.ExitDate < DateTime.Parse(searchWord)))
                                {
                                    searchResultCollection.Add(reg);
                                }
                                Collection = searchResultCollection;
                                return;
                            }
                        }

                    }
                        break;
                    default:
                        throw new Exception("Column not found");
                }
            }
            Refresh();
        }
    }

    public class RegistrationRepository : IRepository<Registration>
    {
        private ObservableCollection<Registration> _collection;

        public RegistrationRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = mainContext.GetRegistration();
        }
        public MainEntititesContext MainContext { get; set; }
        public ObservableCollection<Registration> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null) ModelCollectionChanged();
            }
        }
        public event Action ModelCollectionChanged;
        public void Add(Registration obj)
        {
            obj.HotelID = 1;
            if (obj.Guests.GuestsDiscounts.Count(gd => gd.GuestID == obj.GuestID) != 0)
            obj.DiscountID = obj.Guests.GuestsDiscounts.First(gd => gd.GuestID == obj.GuestID).DiscountID;
            MainContext.Context.Registration.Add(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }
        public void Remove(Registration obj)
        {
            MainContext.Context.Registration.Remove(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }
        public void Update()
        {
            throw new NotImplementedException();
        }
        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            
        }
        public void Refresh()
        {
            Collection = MainContext.GetRegistration();
        }
        public Hotels CurrentHotel
        {
            get { throw new NotImplementedException(); }
        }
    }

}
