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
            Assert.AreEqual("��������", House.Entry.Name);

            var garage = House.Entry.GetExit(Direction.������);
            Assert.AreEqual("�����", garage.Name);

            var hallway = House.Entry.GetExit(Direction.������);
            Assert.AreEqual("���", hallway.Name);

            var kitchen = hallway.GetExit(Direction.�����������);
            Assert.AreEqual("�����", kitchen.Name);

            var bathroom = hallway.GetExit(Direction.�����);
            Assert.AreEqual("������", bathroom.Name);

            var livingRoom = hallway.GetExit(Direction.��);
            Assert.AreEqual("��������", livingRoom.Name);

            var landing = hallway.GetExit(Direction.������);
            Assert.AreEqual("���������� ��������", landing.Name);

            var masterBedroom = landing.GetExit(Direction.�����������);
            Assert.AreEqual("��������� �������", masterBedroom.Name);

            var masterBath = masterBedroom.GetExit(Direction.������);
            Assert.AreEqual("��������� ������", masterBath.Name);

            var secondBathroom = landing.GetExit(Direction.�����);
            Assert.AreEqual("������ ������", secondBathroom.Name);

            var nursery = landing.GetExit(Direction.��������);
            Assert.AreEqual("������� �������", nursery.Name);

            var pantry = landing.GetExit(Direction.��);
            Assert.AreEqual("��������", pantry.Name);

            var kidsRoom = landing.GetExit(Direction.���������);
            Assert.AreEqual("������ ������� �������", kidsRoom.Name);

            var attic = landing.GetExit(Direction.������);
            Assert.AreEqual("������", attic.Name);
        }

        [TestMethod]
        public void TestHidingPlace()
        {
            Assert.IsInstanceOfType(House.GetLocationByName("�����"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("�����"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("��������"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("������"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("��������� �������"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("��������� ������"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("������ ������"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("������� �������"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("������ ������� �������"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("��������"), typeof(LocationWithHidingPlace));
            Assert.IsInstanceOfType(House.GetLocationByName("������"), typeof(LocationWithHidingPlace));
        }

        [TestMethod]
        public void TestClearHidingPlace()
        {
            var garage = House.GetLocationByName("�����") as LocationWithHidingPlace;
            garage.Hide(new Opponent("��������1"));

            var attic = House.GetLocationByName("������") as LocationWithHidingPlace;
            attic.Hide(new Opponent("��������2"));
            attic.Hide(new Opponent("��������3"));
            attic.Hide(new Opponent("��������4"));

            House.ClearHidingPlaces();
            Assert.AreEqual(0, garage.CheckHidingPlace().Count());
            Assert.AreEqual(0, attic.CheckHidingPlace().Count());
        }
    }
}
