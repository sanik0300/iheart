using Android.App;
using Xamarin.Forms;
using System.Globalization;
using меньше_3;
using Java.Util;
using System.Collections.Generic;

[assembly: Dependency(typeof(меньше_3_Droid.Localize))]
namespace меньше_3_Droid
{
    public class Localize : ILocal
    {
        private Dictionary<string, string> wws = new Dictionary<string, string>(4, default);
        public Dictionary<string, string> words
        {
            get { return wws; }
        }

        public ILocal GetCurrentCultureInfo()
        {
            Locale android_locale = Locale.Default;
            string net_lan = android_locale.ToString().Replace('_', '-');
            string lang_name = new CultureInfo(net_lan).ThreeLetterISOLanguageName;
            bool ruski = lang_name == "rus" || lang_name == "ukr" || lang_name == "bel" || lang_name == "kaz";
            wws.Add("vkl", ruski ? "ВКЛ" : "ON");
            wws.Add("vikl", ruski ? "ВЫКЛ" : "OFF");
            wws.Add("shh", ruski ? "Беспалевный режим" : "Sound mode");
            wws.Add("vibr", ruski ? "Палевный режим" : "Vibrating mode");
            return this;
        }
    }
}
