using System;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeek
{
    public class Opponent
    {
        public readonly string Name;
        public Opponent(string name) => Name = name;
        public override string ToString() => Name;
        public LocationWithHidingPlace HiddenPlace { get; private set; }

        public void Hide()
        {
            var currentLocation = House.Entry;
            int i = 0;
            int randomInt = House.Random.Next(10, 50);
            while (!(currentLocation is LocationWithHidingPlace) && (i < randomInt))
            {
                currentLocation = House.RandomExit(currentLocation);
                System.Diagnostics.Debug.WriteLine(currentLocation);
            }
            var locationToHide = currentLocation as LocationWithHidingPlace;
            locationToHide.Hide(this);
            HiddenPlace = locationToHide;
            System.Diagnostics.Debug.WriteLine($"{Name} прячется {(currentLocation as LocationWithHidingPlace).HidingPlace} в {currentLocation.Name}");
        }
    }
}
