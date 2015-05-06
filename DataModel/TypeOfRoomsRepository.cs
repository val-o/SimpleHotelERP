using System.Collections.ObjectModel;

namespace DataModel
{
    public class TypeOfRoomsRepository : IRepository<TypesOfRooms>
    {
        public ObservableCollection<TypesOfRooms> Collection { get; set; }
        public BaseEntitiesContext Context { get; set; }

        public TypeOfRoomsRepository(MainEntititesContext mainContext)
        {
            Collection = mainContext.GetTypesOfRooms();
        }

        public void Add(TypesOfRooms obj)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(TypesOfRooms obj)
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }


        public void Filtration(string searchWord, string searchItem)
        {
            throw new System.NotImplementedException();
        }


        public event System.Action ModelCollectionChanged;


        public ObservableCollection<TypesOfRooms> FreshCollection
        {
            get { throw new System.NotImplementedException(); }
        }


        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            throw new System.NotImplementedException();
        }

        public MainEntititesContext MainContext
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }


        public void Refresh(TypesOfRooms obj)
        {
            throw new System.NotImplementedException();
        }


        public Hotels CurrentHotel
        {
            get { throw new System.NotImplementedException(); }
        }


        public void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}
