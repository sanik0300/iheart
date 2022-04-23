
using System;

namespace меньше_3
{
    public interface IBonk
    {
        string StopWordForMessages { get; }
        void SingleBonk();
        void Stop_and_Release_Ress();

        void BeginDisplaying(string title);
        void EndDisplaying();
        int duration { get; }

        void ReportTimeLeft(TimeSpan tsp);
    }
}
