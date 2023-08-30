using System;
using System.Collections.Generic;
using System.Text;

namespace HideAndSeek
{
    public enum Direction
    {
        Север = -1,
        Юг = 1,
        Восток = -2,
        Запад = 2,
        Северовосток = -3,
        Югозапад = 3,
        Юговосток = -4,
        Северозапад = 4,
        Наверх = -5,
        Вниз = 5,
        Внутрь = -6,
        Наружу = 6
    }
}
