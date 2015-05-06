using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public interface IViewRepository<T> where T : class
    {
        MainEntititesContext MainContext { get; set; }
        List<T> Collection { get; set; }
        void Refresh();
        event Action ViewCollectionChanged;
        void Filtration(string searchWord, string searchItem, FiltrationType filtrationType);
    }

}
