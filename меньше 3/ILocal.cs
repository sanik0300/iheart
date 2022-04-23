using System;
using System.Collections.Generic;
using System.Globalization;

namespace меньше_3
{
    enum Sentences : int { On, Off, Shh, Vibr, Title }
    public interface ILocal
    {
        string[] words { get; }
        bool NeedToDarkenLeTheme();
    }
}
