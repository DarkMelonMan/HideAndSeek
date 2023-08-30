using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeekTests
{
    using HideAndSeek;
    using System.Linq;

    [TestClass]
    public class OpponentTests
    {
        [TestMethod]
        public void TestOpponentHiding()
        {
            var opponent1 = new Opponent("Оппонент1");
            Assert.AreEqual("Оппонент1", opponent1.Name);

            House.Random = new MockRandomWithValueList(new int[] { 0, 1, 2 });
            opponent1.Hide();
            var bathroom = House.GetLocationByName("Ванная") as LocationWithHidingPlace;
            CollectionAssert.AreEqual(new[] { opponent1 }, bathroom.CheckHidingPlace().ToArray());

            var opponent2 = new Opponent("Оппонент2");
            Assert.AreEqual("Оппонент2", opponent2.Name);

            House.Random = new MockRandomWithValueList(new int[] { 0, 1, 4 });
            opponent2.Hide();
            var kitchen = House.GetLocationByName("Кухня") as LocationWithHidingPlace;
            CollectionAssert.AreEqual(new[] { opponent2 }, kitchen.CheckHidingPlace().ToArray());
        }
    }
}
