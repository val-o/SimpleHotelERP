using System.Collections.Generic;
using DataModel;

namespace Main.ViewModel.SearchViewModels
{
    public class RoomsSearchViewModel : SearchViewModelBase<Rooms>
    {
        public RoomsSearchViewModel(IRepository<Rooms> repository)
            : base(repository, new List<string>() { "Номер Комнаты", "Вместимость", "Стоимость","Класс" })
        {
        }

        public override void CheckComboboxStatus()
        {
            if (SearchItem == "Номер Комнаты" || SearchItem == "Класс")
            {
                ComboboxIsEnabled = false;
                FiltrationTypeString = "Равно";
            }
            else ComboboxIsEnabled = true;
        }
    }
}
