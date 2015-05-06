using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IRepository<T> where T : class
    {
        MainEntititesContext MainContext { get; set; }
        ObservableCollection<T> Collection { get; set; }
        Hotels CurrentHotel { get; }
        void Add(T obj);
        void Remove(T obj);
        void Update();
        void Refresh();
        event Action ModelCollectionChanged;
        void Filtration(string searchWord, string searchItem, FiltrationType filtrationType);
    }
}
