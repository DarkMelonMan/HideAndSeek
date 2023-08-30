using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HideAndSeekTests
{
    using HideAndSeek;
    using System.Linq;

    [TestClass]
    public class HouseTests
    {
        [TestMethod]
        public void TestLayout()
        {
            Assert.AreEqual("Прихожая", House.Entry.Name);

            var garage = House.Entry.GetExit(Direction.Наружу);
            Assert.AreEqual("Гараж", garage.Name);

            var hallway = House.Entry.GetExit(Direction.Восток);
            Assert.AreEqual("Зал", hallway.Name);

            var kitchen = hallway.GetExit(Direction.Северозапад);
            Assert.AreEqual("Кухня", kitchen.Name);

            var bathroom = hallway.GetExit(Direction.Север);
            Assert.AreEqual("Ванная", bathroom.Name);

            var livingRoom = hallway.GetExit(Direction.Юг);
            Assert.AreEqual("Гостиная", livingRoom.Name);

            var landing = hallway.GetExit(Direction.Наверх);
            Assert.AreEqual("Лестничная площадка", landing.Name);

            var masterBedroom = landing.GetExit(Direction.Северозапад);
            Assert.AreEqual("Хозяйская спальня", masterBedroom.Name);

            var masterBath = masterBedroom.GetExit(Direction.Восток);
            Assert.AreEqual("Хозяйская ванная", masterBath.Name);

            var secondBathroom = landing.GetExit(Direction.Запад);
            Assert.AreEqual("Вторая ванная", secondBathroom.Name);

            var nursery = landing.GetExit(Direction.Югозапад);
            Assert.AreEqual("Детская комната", nursery.Name);

            var pantry = landing.GetExit(Direction.Юг);
            Assert.AreEqual("Кладовая", pantry.Name);

            var kidsRoom = landing.GetExit(Direction.Юговосток);
            Assert.AreEqual("Вторая детская комната", kidsRoom.Name);

            var attic = landing.GetExit(Direction.Наверх);
            Assert.AreEqual("Чердак", attic.Name);
        }

        [TestMethod]
        public void TestHidingPlace()
        {
            Assert.IsInstanceOfType(House.GetLocationByName("Гараж"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Кухня"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Гостиная"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Ванная"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Хозяйская спальня"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Хозяйская ванная"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Вторая ванная"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Детская комната"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Вторая детская комната"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Кладовая"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("Чердак"), typeof(LocationWithHidingPlace));
        }

        [TestMethod]
        public void TestClearHidingPlace()
        {
            var garage = House.GetLocationByName("Гараж") as LocationWithHidingPlace;
            garage.Hide(new Opponent("Оппонент1"));

            var attic = House.GetLocationByName("Чердак") as LocationWithHidingPlace;
            attic.Hide(new Opponent("Оппонент2"));
            attic.Hide(new Opponent("Оппонент3"));
            attic.Hide(new Opponent("Оппонент4"));

            House.ClearHidingPlaces();
            Assert.AreEqual(0, garage.CheckHidingPlace().Count());
            Assert.AreEqual(0, attic.CheckHidingPlace().Count());
        }
    }
}
