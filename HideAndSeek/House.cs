using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HideAndSeek
{
    public static class House
    {
        public static Location Entry = new Location("Прихожая");
        public static Random Random = new Random();
        private static readonly List<Location> locations; 
        static House()
        {
            var hallway = new Location("Зал");
            var garage = new LocationWithHidingPlace("Гараж", "за машиной");
            var kitchen = new LocationWithHidingPlace("Кухня", "под столом");
            var bathroom = new LocationWithHidingPlace("Ванная", "за занавесками");
            var livingRoom = new LocationWithHidingPlace("Гостиная", "за диваном");
            var landing = new Location("Лестничная площадка");
            var masterBedroom = new LocationWithHidingPlace("Хозяйская спальня", "под кроватью");
            var masterBathroom = new LocationWithHidingPlace("Хозяйская ванная", "за стиральной машиной");
            var secondBathroom = new LocationWithHidingPlace("Вторая ванная", "за дверью");
            var nursery = new LocationWithHidingPlace("Детская комната", "за шторами");
            var pantry = new LocationWithHidingPlace("Кладовая", "за мешками");
            var kidsRoom = new LocationWithHidingPlace("Вторая детская комната", "в шкафу");
            var attic = new LocationWithHidingPlace("Чердак", "за коробками");
            locations = new List<Location> { Entry, hallway, garage, kitchen, bathroom,
            livingRoom, landing, masterBedroom, masterBathroom, secondBathroom, nursery,
            pantry, kidsRoom, attic};

            Entry.AddExit(Direction.Наружу, garage);
            Entry.AddExit(Direction.Восток, hallway);
            hallway.AddExit(Direction.Северозапад, kitchen);
            hallway.AddExit(Direction.Север, bathroom);
            hallway.AddExit(Direction.Юг, livingRoom);

            hallway.AddExit(Direction.Наверх, landing);
            landing.AddExit(Direction.Северозапад, masterBedroom);
            masterBedroom.AddExit(Direction.Восток, masterBathroom);
            landing.AddExit(Direction.Запад, secondBathroom);
            landing.AddExit(Direction.Югозапад, nursery);
            landing.AddExit(Direction.Юг, pantry);
            landing.AddExit(Direction.Юговосток, kidsRoom);
            landing.AddExit(Direction.Наверх, attic);
        }

        public static Location GetLocationByName(string locationName)
        {
            foreach (Location location in locations) 
            {
                if (locationName == location.Name)
                    return location;
            }
            return Entry;
         } 

        public static void ClearHidingPlaces()
        {
            foreach (Location location in locations)
            {
                if (location is LocationWithHidingPlace locationWithHidingPlace)
                    locationWithHidingPlace.CheckHidingPlace();
            }       
        }

        public static Location RandomExit(Location location)
        {
            var randomKey = location.Exits.Keys.ToArray()[Random.Next(location.Exits.Keys.Count)];
            return location.Exits[randomKey];
        }
    }
}
