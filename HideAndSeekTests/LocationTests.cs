using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HideAndSeekTests
{
    using System.Collections.Generic;
    using HideAndSeek;
    using System.Linq;
    
    [TestClass]
    public class LocationTests
    {
        private Location center;

        /// <summary>
        /// Инициализирует каждый юнит-тест созданием новой центральной локации и
        /// добавлением комнат в каждом направлении перед тестом
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            center = new Location("Центр");
            center.AddExit(Direction.Восток, new Location("Восток"));
            center.AddExit(Direction.Запад, new Location("Запад"));
            center.AddExit(Direction.Юг, new Location("Юг"));
            center.AddExit(Direction.Север, new Location("Север"));
            center.AddExit(Direction.Северовосток, new Location("Северо-восток"));
            center.AddExit(Direction.Северозапад, new Location("Северо-запад"));
            center.AddExit(Direction.Юговосток, new Location("Юго-восток"));
            center.AddExit(Direction.Югозапад, new Location("Юго-запад"));
        }

        /// <summary>
        /// Проверяет, что GetExit возвращает локацию только в том случае, если она существует
        /// </summary>
        [TestMethod]
        public void TestGetExit()
        {
            Assert.AreEqual("Центр", center.GetExit(Direction.Наверх).Name);
        }

        /// <summary>
        /// Проверяет, что ExitList работает
        /// </summary>
        [TestMethod]
        public void TestExitList()
        {
            var exitList = new List<string>
            {
                "Юго-восток на Юго-востоке", "Северо-восток на Северо-востоке", 
                "Восток на Востоке", "Север на Севере", "Юг на Юге", "Запад на Западе",
                "Юго-запад на Юго-западе", "Северо-запад на Северо-западе"
            };
            CollectionAssert.AreEqual(exitList, center.ExitList.ToList());
        }

        /// <summary>
        /// Проверяет, что название комнат и возвращение выходов заданы правильно
        /// </summary>
        [TestMethod]
        public void TestReturnExits()
        {
            Assert.AreEqual("Восток", center.GetExit(Direction.Восток).Name);
            Assert.AreEqual("Запад", center.GetExit(Direction.Запад).Name);
            Assert.AreEqual("Юг", center.GetExit(Direction.Юг).Name);
            Assert.AreEqual("Север", center.GetExit(Direction.Север).Name);
            Assert.AreEqual("Северо-восток", center.GetExit(Direction.Северовосток).Name);
            Assert.AreEqual("Северо-запад", center.GetExit(Direction.Северозапад).Name);
            Assert.AreEqual("Юго-восток", center.GetExit(Direction.Юговосток).Name);
            Assert.AreEqual("Юго-запад", center.GetExit(Direction.Югозапад).Name);
        }

        /// <summary>
        /// Добавляет зал к одной из комнат и проверяет, что названия комнат
        /// и возвращение выходов заданы правильно
        /// </summary>
        [TestMethod]
        public void TestAddHall()
        {
            var hall = new Location("Зал");
            center.AddExit(Direction.Наверх, hall);
            Assert.AreEqual("Центр", hall.GetExit(Direction.Вниз).Name);
        }
    }
}
