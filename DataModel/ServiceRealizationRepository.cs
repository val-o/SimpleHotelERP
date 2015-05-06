using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ServiceRealizationRepository : IRepository<ServiceRealization>
    {
          public MainEntititesContext MainContext { get; set; }

          public ServiceRealizationRepository(MainEntititesContext mainContext)
        {
            MainContext = mainContext;
            Collection = mainContext.GetServicRealizations();
        }
        public ObservableCollection<ServiceRealization> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                if (ModelCollectionChanged != null) ModelCollectionChanged();

            }
        }

        private ObservableCollection<ServiceRealization> _collection;
        public Hotels CurrentHotel
        {
            get { throw new NotImplementedException(); }
        }


        public void Add(ServiceRealization obj)
        {
            obj.RealizationDate = DateTime.Now;
            MainContext.Context.ServiceRealization.Add(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }

        public void Remove(ServiceRealization obj)
        {
            MainContext.Context.ServiceRealization.Remove(obj);
            MainContext.Context.SaveChanges();
            Refresh();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            Collection = MainContext.GetServicRealizations();
        }

        public event Action ModelCollectionChanged;

        public void Filtration(string searchWord, string searchItem, FiltrationType filtrationType)
        {
            throw new NotImplementedException();
        }
    }
}
