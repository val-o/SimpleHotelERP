
using System;
using System.Collections.ObjectModel;
using System.Linq;
using DataModel;
using Main.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class RoomsRepositoryTests
    {

        [TestMethod]
        public void RepositoryAddTest()
        {
            var mainEntititesContext = new MainEntititesContext();
            var hotelRepository = new HotelsRepository(mainEntititesContext);
            mainEntititesContext.CurrentHotel = hotelRepository.Collection.First();
            var roomsRepository = new RoomsRepository(mainEntititesContext);
            Assert.IsNotNull(roomsRepository, "Репозиторий не заполнен данными из базы!");
            var room = new Rooms() {RoomNum = "404B"};
            roomsRepository.Add(room);
            Assert.IsTrue(roomsRepository.Collection.Contains(room), "Значение не было доабвленно в коллекцию!");
            roomsRepository.Remove(room);
        }
        [TestMethod]
        public void FiltrationTest()
        {
            /*var mainEntititesContext = new MainEntititesContext();
            var RoomsRepository = new RoomsRepository(mainEntititesContext);
            var room = new Rooms() { RoomNum = "AACA" };
            RoomsRepository.Add(room);
            Assert.IsNotNull(RoomsRepository.Filtration("404","RoomNum"));
            var testCollection = new ObservableCollection<Rooms>() {room};
            Assert.IsTrue(RoomsRepository.Filtration("AACA", "RoomNum").Contains(room), "asdf");
            RoomsRepository.Remove(room);*/
        }
        
    }
}
