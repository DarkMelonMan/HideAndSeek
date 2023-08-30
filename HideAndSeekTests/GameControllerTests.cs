using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeekTests
{
    using HideAndSeek;
    using System.Linq;

    [TestClass]
    public class GameControllerTests
    {
        GameController gameController;
        [TestInitialize]
        public void Initialize()
        {
            gameController = new GameController();
        }

        [TestMethod]
        public void TestMovement()
        {
            Assert.AreEqual("Прихожая", gameController.CurrentLocation.Name);

            Assert.IsFalse(gameController.Move(Direction.Наверх));
            Assert.AreEqual("Прихожая", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Восток));
            Assert.AreEqual("Зал", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Наверх));
            Assert.AreEqual("Лестничная площадка", gameController.CurrentLocation.Name);

            Assert.IsTrue(gameController.Move(Direction.Наверх));
            Assert.AreEqual("Чердак", gameController.CurrentLocation.Name);
        }

        public void TestParseInput()
        {
            var initialStatus = gameController.Status;

            Assert.AreEqual("Данное направление некорректно", gameController.ParseInput("X"));
            Assert.AreEqual(initialStatus, gameController.Status);

            Assert.AreEqual("В данном направлении нет выхода", gameController.ParseInput("Наверх"));
            Assert.AreEqual(initialStatus, gameController.Status);

            Assert.AreEqual("Движемся на Восток", gameController.ParseInput("Восток"));
            Assert.AreEqual(@"Вы находитесь в Зале. Вы видите следующие выходы:
 - Ванная на Севере
 - Гостиная на Юге
 - Прихожая на Западе
 - Кухная на Северо-западе
 - Лестничная площадка Наверху
Вы пока не нашли ни одного оппонента", gameController.Status);

            Assert.AreEqual("Движемся на Юг", gameController.ParseInput("Юг"));
            Assert.AreEqual(@"Вы находитесь в Гостиной. Вы видите следующие выходы:
 - Зал на Севере
Кто-то может прятаться за диваном
Вы пока не нашли ни одного оппонента", gameController.Status);
        }

        [TestMethod]
        public void TestParseCheck()
        {
            Assert.IsFalse(gameController.GameOver);

            House.ClearHidingPlaces();
            var pavel = gameController.Opponents.ToList()[0];
            (House.GetLocationByName("Гараж") as LocationWithHidingPlace).Hide(pavel);
            var danil = gameController.Opponents.ToList()[1];
            (House.GetLocationByName("Кухня") as LocationWithHidingPlace).Hide(danil);
            var vitaly = gameController.Opponents.ToList()[2];
            (House.GetLocationByName("Чердак") as LocationWithHidingPlace).Hide(vitaly);
            var yareYarik = gameController.Opponents.ToList()[3];
            (House.GetLocationByName("Чердак") as LocationWithHidingPlace).Hide(yareYarik);
            var gosha = gameController.Opponents.ToList()[4];
            (House.GetLocationByName("Кухня") as LocationWithHidingPlace).Hide(gosha);

            Assert.AreEqual(1, gameController.MoveNumber);
            Assert.AreEqual("В Прихожей негде спрятаться", gameController.ParseInput("проверить"));
            Assert.AreEqual(2, gameController.MoveNumber);

            gameController.ParseInput("Наружу");
            Assert.AreEqual(3, gameController.MoveNumber);

            Assert.AreEqual("Вы нашли 1 оппонента, прячущегося за машиной", gameController.ParseInput("Проверить"));
            Assert.AreEqual(@"Вы находитесь в Гараже. Вы видите следующие выходы:
 - Прихожая Внутри
Кто-то может прятаться за машиной
Вы нашли 1 из 5 оппонентов: Паша", gameController.Status);
            Assert.AreEqual("4: В каком направлении вы хотите пойти (или напишите 'проверить'): ", gameController.Prompt);
            Assert.AreEqual(4, gameController.MoveNumber);

            gameController.ParseInput("Внутрь");
            gameController.ParseInput("Восток");
            gameController.ParseInput("Север");
            Assert.AreEqual("Никто не прячется за занавесками", gameController.ParseInput("проверить"));
            Assert.AreEqual(8, gameController.MoveNumber);

            gameController.ParseInput("Юг");
            gameController.ParseInput("Северо-запад");
            Assert.AreEqual("Вы нашли 2 оппонентов, прячущихся под столом", gameController.ParseInput("проверить"));
            Assert.AreEqual(@"Вы находитесь на Кухне. Вы видите следующие выходы:
 - Зал на Юго-востоке
Кто-то может прятаться под столом
Вы нашли 3 из 5 оппонентов: Паша, Данил, Гоша", gameController.Status);
            Assert.AreEqual("11: В каком направлении вы хотите пойти (или напишите 'проверить'): ", gameController.Prompt);
            Assert.AreEqual(11, gameController.MoveNumber);

            Assert.IsFalse(gameController.GameOver);

            gameController.ParseInput("Юго-восток");
            gameController.ParseInput("Наверх");
            Assert.AreEqual(13, gameController.MoveNumber);

            gameController.ParseInput("Юг");
            Assert.AreEqual("Никто не прячется за мешками", gameController.ParseInput("проверить"));
            Assert.AreEqual(15, gameController.MoveNumber);

            gameController.ParseInput("Север");
            gameController.ParseInput("Наверх");
            Assert.AreEqual(17, gameController.MoveNumber);

            Assert.AreEqual("Вы нашли 2 оппонентов, прячущихся за коробками", gameController.ParseInput("проверить"));
            Assert.AreEqual(@"Вы находитесь на Чердаке. Вы видите следующие выходы:
 - Лестничная площадка Внизу
Кто-то может прятаться за коробками
Вы нашли 5 из 5 оппонентов: Паша, Данил, Гоша, Виталя, Ярик", gameController.Status);
            Assert.AreEqual("18: В каком направлении вы хотите пойти (или напишите 'проверить'): ", gameController.Prompt);
            Assert.AreEqual(18, gameController.MoveNumber);

            Assert.IsTrue(gameController.GameOver);
        }
    }
}
