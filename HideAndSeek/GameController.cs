using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;

namespace HideAndSeek
{
    public class GameController
    {
        /// <summary>
        /// Текущая локация игрока в доме
        /// </summary>
        public Location CurrentLocation { get; private set; }

        /// <summary>
        /// Возвращает текущий статус игры
        /// </summary>
        public string Status { get 
            {
                string someoneHidesHere = CurrentLocation is LocationWithHidingPlace locationWithHidingPlace ? $"\r\nКто-то может прятаться {locationWithHidingPlace.HidingPlace}" : "";
                string foundOps = foundOpponents.Count() > 0 ? $"Вы нашли { foundOpponents.Count()} из { Opponents.Count()} оппонентов: { string.Join(", ", foundOpponents)}" : "Вы пока не нашли ни одного оппонента";
                return $@"Вы находитесь {InflectLocations(CurrentLocation)}. Вы видите следующие выходы:
{string.Join("\r\n", CurrentLocation.ExitList
     .Select(exit => $" - {exit}"))}{someoneHidesHere}
{foundOps}";
            }
        }
        /// <summary>
        /// Количество ходов, сделанных игроком
        /// </summary>
        public int MoveNumber { get; private set; } = 1;

        /// <summary>
        /// Возвращает true, если игра окончена
        /// </summary>
        public bool GameOver => Opponents.Count() == foundOpponents.Count();

        /// <summary>
        /// Строка, которую показывают игроку
        /// </summary>
        public string Prompt => $"{MoveNumber}: В каком направлении вы хотите пойти (или напишите 'проверить'): ";
        
        /// <summary>
        /// Приватный список, содержащий игроков, которых нужно найти
        /// </summary>
        public readonly IEnumerable<Opponent> Opponents = new List<Opponent>()
        {
            new Opponent("Паша"),
            new Opponent("Данил"),
            new Opponent("Виталя"),
            new Opponent("Ярик"),
            new Opponent("Гоша")
        };

        /// <summary>
        /// Приватный список, содержащий игроков, которых игрок уже нашёл
        /// </summary>
        private readonly List<Opponent> foundOpponents = new List<Opponent>();

        public GameController()
        {
            House.ClearHidingPlaces();
            foreach (var opponent in Opponents)
                opponent.Hide();
            CurrentLocation = House.Entry;
        }

        private string InflectLocations(Location location)
        {
            var result = location.Name switch
            {
                "Прихожая" => "в Прихожей",
                "Гараж" => "в Гараже",
                "Зал" => "в Зале",
                "Кухня" => "на Кухне",
                "Гостиная" => "в Гостиной",
                "Ванная" => "в Ванной",
                "Лестничная площадка" => "на Лестничной площадке",
                "Хозяйская спальня" => "в Хозяйской спальне",
                "Хозяйская ванная" => "в Хозяйской ванной",
                "Вторая ванная" => "во Второй ванной",
                "Детская комната" => "в Детской комнате",
                "Кладовая" => "в Кладовой",
                "Вторая детская комната" => "во Второй детской комнате",
                "Чердак" => "на Чердаке",
                _ => location.Name
            };
            return result;
        }

        /// <summary>
        /// Двигается по локации в заданном направлении
        /// </summary>
        /// <param name="direction">Заданное направление</param>
        /// <returns>True, если игрок может двигаться в эту локацию, false иначе</returns>
        public bool Move(Direction direction)
        {
            if (CurrentLocation.GetExit(direction).Name != CurrentLocation.Name)
            {
                CurrentLocation = CurrentLocation.GetExit(direction);
                return true;
            }
            return false;
        }

        public string firstLetterToUpper(string input)
        {
            var output = input.ToCharArray();
            output[0] = output[0].ToString().ToUpper()[0];
            return new string(output);
        }

        /// <summary>
        /// Обрабатывает входные данные от игрока и обновляет статус
        /// </summary>
        /// <param name="input">Входные данные, требующие обработки</param>
        /// <returns>Результат обработки входных данных</returns>
        public string ParseInput(string input)
        {
            var checkedInput = input switch
            {
                "Северо-восток" => "Северовосток",
                "Северо-запад" => "Северозапад",
                "Юго-восток" => "Юговосток",
                "Юго-запад" => "Югозапад",
                _ => firstLetterToUpper(input)
            };
            System.Diagnostics.Debug.WriteLine(checkedInput);
            if (Enum.TryParse(checkedInput, out Direction direction))
            {
                if (Move(direction))
                {
                    MoveNumber++;
                    if (new List<Direction>() {
                        Direction.Вниз, Direction.Внутрь,
                        Direction.Наверх, Direction.Наружу}
                    .Contains(direction)) return $"Движемся {direction}";
                    else if (direction == Direction.Северовосток)
                        return "Движемся на Северо-восток";
                    else if (direction == Direction.Северозапад)
                        return "Движемся на Северо-запад";
                    else if (direction == Direction.Юговосток)
                        return "Движемся на Юго-восток";
                    else if (direction == Direction.Югозапад)
                        return "Движемся на Юго-запад";
                    else
                        return $"Движемся на {direction}";
                }
                else
                    return "В данном направлении нет выхода";
            }
            else if ((checkedInput == "Проверить") && (CurrentLocation is LocationWithHidingPlace locationWithHidingPlace))
            {
                MoveNumber++;
                var hidingOpponents = locationWithHidingPlace.CheckHidingPlace();
                var output = $"Кто-то может прятаться {locationWithHidingPlace.HidingPlace}";
                if (hidingOpponents.Count() > 0)
                {
                    foundOpponents.AddRange(hidingOpponents);
                    var s = hidingOpponents.Count() == 1 ? "а" : "ов";
                    var s2 = hidingOpponents.Count() == 1 ? "егося" : "ихся";
                    return $"Вы нашли {hidingOpponents.Count()} оппонент{s}, прячущ{s2} {locationWithHidingPlace.HidingPlace}";
                }
                else
                    return $"Никто не прячется {locationWithHidingPlace.HidingPlace}";
            }
            else if ((checkedInput == "Проверить") && !(CurrentLocation is LocationWithHidingPlace))
            {
                MoveNumber++;
                return $"{firstLetterToUpper(InflectLocations(CurrentLocation))} негде спрятаться";
            }
            else if (checkedInput.Split(" ")[0] == "Сохранить")
            {
                Dictionary<string, string> opponentLocations = new Dictionary<string, string>();
                foreach (Opponent opponent in Opponents)
                    opponentLocations[opponent.Name] = opponent.HiddenPlace.Name;
                var newSavedGame = new SavedGame() 
                { 
                    FoundOpponentNames = foundOpponents.Select(opponent => opponent.Name).ToList(),
                    PlayerLocation = CurrentLocation.Name, 
                    PlayerMoveNumber = MoveNumber, 
                    OpponentLocations = opponentLocations
                };
                string savedGameJson = JsonSerializer.Serialize(newSavedGame);
                File.WriteAllText(checkedInput.Split(" ")[1] + ".json", savedGameJson);
                House.ClearHidingPlaces();
                foreach (var opponent in Opponents)
                    opponent.Hide();
                CurrentLocation = House.Entry;
                MoveNumber = 1;
                foundOpponents.Clear();
                return $"Сохранение текущей игры на {checkedInput.Split(" ")[1]}";
            }
            else if (checkedInput.Split(" ")[0] == "Загрузить")
            {
                string loadedGameJson = File.ReadAllText(checkedInput.Split(" ")[1] + ".json");
                SavedGame loadedGame = JsonSerializer.Deserialize<SavedGame>(loadedGameJson);
                CurrentLocation = House.GetLocationByName(loadedGame.PlayerLocation);
                MoveNumber = loadedGame.PlayerMoveNumber;
                foreach (var opponent in Opponents) 
                {
                    var opponentHidingLocation = House.GetLocationByName(loadedGame.OpponentLocations[opponent.Name]) as LocationWithHidingPlace;
                    opponentHidingLocation.Hide(opponent);
                    if (loadedGame.FoundOpponentNames.Contains(opponent.Name))
                    {
                        foundOpponents.Add(opponent);
                        opponentHidingLocation.CheckHidingPlace();
                    }
                }
                return $"Загрузка игры с {checkedInput.Split(" ")[1]}";
            }
            else
                return "Данное направление некорректно";
        }
    }
}
