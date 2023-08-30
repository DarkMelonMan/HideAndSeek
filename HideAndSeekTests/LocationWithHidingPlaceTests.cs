using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using HideAndSeek;
using System.Linq;

namespace HideAndSeekTests
{
    [TestClass]
    public class LocationWithHidingPlaceTests
    {
        [TestMethod]
        public void TestHiding()
        {
            var hidingLocation = new LocationWithHidingPlace("Комната", "под кроватью");
            Assert.AreEqual("Комната", hidingLocation.Name);
            Assert.AreEqual("Комната", hidingLocation.ToString());
            Assert.AreEqual("под кроватью", hidingLocation.HidingPlace);

            var opponent1 = new Opponent("Оппонент1");
            var opponent2 = new Opponent("Оппонент2");
            hidingLocation.Hide(opponent1);
            hidingLocation.Hide(opponent2);
            List<Opponent> opponents = hidingLocation.CheckHidingPlace().ToList();
            System.Diagnostics.Debug.WriteLine(opponents.Count);
            CollectionAssert.AreEqual(new List<Opponent>() { opponent1, opponent2 },
                opponents);
            CollectionAssert.AreEqual(new List<Opponent>(), hidingLocation.CheckHidingPlace().ToList());
        }
    }
}
