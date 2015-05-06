using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class MainEntitesContext<T> where T: class, new()
    {

    }
        
//    public class MainEntititesContext
//    {
//        public BaseEntitiesContext Context { get; set; }
//        public Hotels CurrentHotel { get; set; }
//        public MainEntititesContext()
//        {
//            Context = new BaseEntitiesContext(); 
//        }
//        public ObservableCollection<Hotels> GetHotels()
//        {
//            IQueryable<Hotels> query = Context.Hotels;
//            return new ObservableCollection<Hotels>(query.ToList());
//        }
//        public ObservableCollection<Guests> GetGuests()
//        {
//            IQueryable<Guests> query = Context.Guests;
//            return new ObservableCollection<Guests>(query.ToList());
////            return new ObservableCollection<Guests>(query.ToList().Where(guest => guest.Registration.Count(registration => registration.HotelID == CurrentHotel.HotelID) > 0));
//        }
//        public ObservableCollection<Rooms> GetRooms()
//        {
//            IQueryable<Rooms> query = Context.Rooms.Where(room => room.HotelID == CurrentHotel.HotelID);
//            return new ObservableCollection<Rooms>(query.ToList());
//        }
//        public List<RegistrationView> GetRegistrationView()
//        {
//            var ctx = ((IObjectContextAdapter)Context).ObjectContext;
//            var query = Context.RegistrationView.Where(reg => reg.HotelID == CurrentHotel.HotelID);
//            ctx.Refresh(RefreshMode.ClientWins, query);
//            return query.ToList();
//        }
//        public ObservableCollection<Registration> GetRegistration()
//        {
//            IQueryable<Registration> query = Context.Registration.Where( reg => reg.HotelID == CurrentHotel.HotelID);
//            return new ObservableCollection<Registration>(query.ToList());
//        }
//        public ObservableCollection<TypesOfRooms> GetTypesOfRooms()
//        {
//            IQueryable<TypesOfRooms> query = Context.TypesOfRooms;
//            return new ObservableCollection<TypesOfRooms>(query.ToList());
//        }
//        public ObservableCollection<FreeRoomsView> GetFreeRoomsView()
//        {
//            IQueryable<FreeRoomsView> query = Context.FreeRoomsView;
//            return new ObservableCollection<FreeRoomsView>(query.ToList());
//        }
//        public ObservableCollection<Payment> GetPayments()
//        {
//            IQueryable<Payment> query = Context.Payment.Where(payment => payment.HotelID == CurrentHotel.HotelID);
//            return new ObservableCollection<Payment>(query.ToList());
//        }
//        public List<PaymentsView> GetPaymentsView()
//        {
//            var ctx = ((IObjectContextAdapter)Context).ObjectContext;
//            var query = Context.PaymentsView.Where(payment => payment.HotelID == CurrentHotel.HotelID);
//            ctx.Refresh(RefreshMode.ClientWins, query);
//            return query.ToList();
//        }
//        internal ObservableCollection<Wishes> GetWishes()
//        {
//            IQueryable<Wishes> query = Context.Wishes.Where(wish => wish.HotelID == CurrentHotel.HotelID);
//            return new ObservableCollection<Wishes>(query.ToList());
//        }
//        internal ObservableCollection<Services> GetServices()
//        {
//            IQueryable<Services> query = Context.Services.Where(serv => serv.HotelID == CurrentHotel.HotelID);
//            return new ObservableCollection<Services>(query.ToList());
//        }
//        internal ObservableCollection<ServiceRealization> GetServicRealizations()
//        {
//            IQueryable<ServiceRealization> query = Context.ServiceRealization.Where(serv => serv.Services.HotelID == CurrentHotel.HotelID);
//            return new ObservableCollection<ServiceRealization>(query.ToList());
//        }
//        internal ObservableCollection<Discounts> GetDiscounts()
//        {
//            IQueryable<Discounts> query = Context.Discounts.Where(discounts => discounts.HotelID == CurrentHotel.HotelID);
//            return new ObservableCollection<Discounts>(query.ToList());
//        }
//        internal ObservableCollection<GuestsDiscounts> GetGuestsDiscounts()
//        {
//            IQueryable<GuestsDiscounts> query = Context.GuestsDiscounts;
//            return new ObservableCollection<GuestsDiscounts>(query.ToList());
//        }
//    }
}
