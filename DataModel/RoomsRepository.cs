using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace DataModel
{
    public class RoomsRepository : IRepository<Rooms>
    {
        private ObservableCollection<Rooms> _collection;
        public ObservableCollection<Rooms> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null) ModelCollectionChanged();
            }
        }
        public MainEntititesContext MainContext { get; set; }
        public event Action ModelCollectionChanged;
        public RoomsRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = mainContext.GetRooms();
        }

        public void Add(Rooms obj)
        {
            obj.HotelID = MainContext.CurrentHotel.HotelID;
            MainContext.Context.Rooms.Add(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }
        public void Remove(Rooms obj)
        {
            if (obj.Registration.Count() != 0)
                throw new Exception();
            obj.HotelID = MainContext.CurrentHotel.HotelID;
            MainContext.Context.Rooms.Remove(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }
        public void Update(Rooms obj)
        {
            MainContext.Context.SaveChanges();
            Refresh();
        }
        public ObservableCollection<Rooms> GetFreeRoomsCollection(DateTime? starDate, DateTime? endDate)
        {
            var freeRoomsCollection = new ObservableCollection<Rooms>();
            foreach (var room in Collection)
            {
                if (room.Registration.Count(reg => reg.EntryDate <= endDate && reg.ExitDate >= starDate) == 0)
                {
                    freeRoomsCollection.Add(room);
                }
            }
            return freeRoomsCollection;
        }
        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            if (!String.IsNullOrEmpty(searchWord))
            {
                Refresh();
                var searchResultCollection = new ObservableCollection<Rooms>();
                switch (searchItem)
                {
                    case "Номер Комнаты":
                    {
                        foreach ( var room in Collection.Where(room => room.RoomNum.ToLower().Contains(searchWord.ToLower())))
                        {
                            searchResultCollection.Add(room);
                        }
                        Collection = searchResultCollection;
                        return;
                    }
                    case "Вместимость":
                    {
                        switch (filtrationType)
                        {
                            case FiltrationType.Equals:
                            {
                                foreach (var room in Collection.Where(room => room.NumberOfPlaces == Byte.Parse(searchWord)))
                                {
                                    searchResultCollection.Add(room);
                                }
                                Collection = searchResultCollection;
                                return;
                            }
                            case FiltrationType.Greater:
                            {
                                foreach (var room in Collection.Where(room => room.NumberOfPlaces > Byte.Parse(searchWord)))
                                {
                                    searchResultCollection.Add(room);
                                }
                                Collection = searchResultCollection;
                                return;
                            }
                            case FiltrationType.Less:
                            {
                                foreach (var room in Collection.Where(room => room.NumberOfPlaces < Byte.Parse(searchWord)))
                                {
                                    searchResultCollection.Add(room);
                                }
                                Collection = searchResultCollection;
                                return;
                            }
                            default:
                                return;
                        }
                    }

                    case "Стоимость":
                    {
                        switch (filtrationType)
                        {
                            case FiltrationType.Equals:
                                {
                                    foreach (var room in Collection.Where(room => room.CostPerDay == Single.Parse(searchWord)))
                                    {
                                        searchResultCollection.Add(room);
                                    }
                                    Collection = searchResultCollection;
                                    return;
                                }
                            case FiltrationType.Greater:
                                {
                                    foreach (var room in Collection.Where(room => room.CostPerDay > Single.Parse(searchWord)))
                                    {
                                        searchResultCollection.Add(room);
                                    }
                                    Collection = searchResultCollection;
                                    return;
                                }
                            case FiltrationType.Less:
                                {
                                    foreach (var room in Collection.Where(room => room.CostPerDay < Single.Parse(searchWord)))
                                    {
                                        searchResultCollection.Add(room);
                                    }
                                    Collection = searchResultCollection;
                                    return;
                                }
                            default:
                                return;
                        }
                    }
                    case "Класс":
                    {
                        foreach (var room in Collection.Where(room => room.TypesOfRooms.Type.ToLower().Contains(searchWord.ToLower())))
                        {
                            searchResultCollection.Add(room);
                        }
                        Collection = searchResultCollection;
                        return;
                    }
                    default:
                        throw new Exception("Column not found");
                }
            }
            Refresh();
        }
        public void Refresh()
        {
            Collection = MainContext.GetRooms(); ;
        }
        public Hotels CurrentHotel
        {
            get { throw new NotImplementedException(); }
        }
        public void Update()
        {
            MainContext.Context.SaveChanges();
            Refresh();
        }
    }

    public enum FiltrationType
    {
        Equals,
        Greater,
        Less
    }
}
