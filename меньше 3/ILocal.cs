using System;
using System.Collections.Generic;
using System.Globalization;

namespace меньше_3
{
    public interface ILocal
    {
        Dictionary<string, string> words { get; }
        ILocal GetCurrentCultureInfo();
    }
}
