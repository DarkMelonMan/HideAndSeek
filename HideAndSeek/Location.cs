using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HideAndSeek
{
    public class Location
    {
        /// <summary>
        /// Название локации
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Выходы из данной локации
        /// </summary>
        public IDictionary<Direction, Location> Exits { get; private set; } = new Dictionary<Direction, Location>();

        /// <summary>
        /// Конструктор устанавливает название локации
        /// </summary>
        /// <param name="name">Название локации</param>
        public Location(string name) => Name = name;

        public override string ToString() => Name;

        /// <summary>
        /// Возвращает последовательность описаний выходов, сортированных по направлению
        /// </summary>
        public IEnumerable<string> ExitList => Exits.Keys.OrderBy(direction => direction).Select(key => $"{Exits[key]} {DescribeDirection(key)}");

        /// <summary>
        /// Добавляет выход к данной локации
        /// </summary>
        /// <param name="direction">Направление присоединяющейся локации</param>
        /// <param name="connectingLocation">Присоединяющаяся локация</param>
        public void AddExit(Direction direction, Location connectingLocation)
        {
            Exits.Add(direction, connectingLocation);
            connectingLocation.Exits.Add((Direction)(-(int)direction), this);
        }

        /// <summary>
        /// Получает выход локации по направлению
        /// </summary>
        /// <param name="direction">Направление выхода локации</param>
        /// <returns>Выход локации или this, если нет выхода в данном направлении</returns>
        public Location GetExit(Direction direction) => Exits.Keys.Contains(direction) ? Exits[direction] : this;

        /// <summary>
        /// Описывает направление
        /// </summary>
        /// <param name="d">Направление для описания</param>
        /// <returns>Строка, описывающая направление</returns>
        public string DescribeDirection(Direction d) => d switch
        {
            Direction.Наверх => "Наверху",
            Direction.Вниз => "Внизу",
            Direction.Внутрь => "Внутри",
            Direction.Наружу => "Снаружи",
            Direction.Северовосток => "на Северо-востоке",
            Direction.Северозапад => "на Северо-западе",
            Direction.Юговосток => "на Юго-востоке",
            Direction.Югозапад => "на Юго-западе",
            Direction.Юг => "на Юге",
            Direction.Север => "на Севере",
            Direction.Запад => "на Западе",
            Direction.Восток => "на Востоке",
            _ => $"{d}"
        };
    }
}
