
using System;

namespace меньше_3
{
    public interface IBonk
    {
        bool InteractWithServiceThisTime { get; }
        void SingleBonk();
        void Stop_and_Release_Ress();

        void BeginDisplaying(string title);
        void EndDisplaying();
        int duration { get; }

        bool AlreadyMusic();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toasttext">text of toast in case of fail</param>
        /// <returns></returns>
        bool CallTakesPlace(string toasttext);
        void ReportTimeLeft(TimeSpan tsp);
    }
}
