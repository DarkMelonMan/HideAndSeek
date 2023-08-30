using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HideAndSeek
{
    public class LocationWithHidingPlace : Location
    {
        public string HidingPlace { get; private set; }
        public LocationWithHidingPlace(string name, string hidingPlace) : base(name) => HidingPlace = hidingPlace;
        private List<Opponent> hidingOpponents = new List<Opponent>();

        public void Hide(Opponent opponent)
        {
            hidingOpponents.Add(opponent);
        }

        public IEnumerable<Opponent> CheckHidingPlace()
        {
            var buffer = new List<Opponent>();
            foreach (var opponent in hidingOpponents)
                buffer.Add(opponent);
            hidingOpponents.Clear();
            return buffer;
        }
    }
}
