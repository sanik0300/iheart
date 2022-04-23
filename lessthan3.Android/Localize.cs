using Xamarin.Forms;
using System.Globalization;
using меньше_3;
using Java.Util;
using Android.OS;

[assembly: Dependency(typeof(меньше_3_Droid.Localize))]
namespace меньше_3_Droid
{
    public class Localize : ILocal
    {
        private string[] wws;
        public string[] words
        {
            get { return wws; }
        }     

        public bool NeedToDarkenLeTheme()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.Q)
                return false;
            return Xamarin.Forms.Application.Current.RequestedTheme == OSAppTheme.Dark;
        }

        public Localize()
        {
            Locale android_locale = Locale.Default;
            string net_lan = android_locale.ToString().Replace('_', '-');
            string lang_name = new CultureInfo(net_lan).ThreeLetterISOLanguageName;

            if (lang_name == "rus" || lang_name == "ukr" || lang_name == "bel" || lang_name == "kaz")
            {
                wws = new string[6] { "ВКЛ", "ВЫКЛ", "Беспалевный режим", "Палевный режим", "Оживляем телефон :3", "Остановить" };
            }
            else
            {
                wws = new string[6] { "ON", "OFF", "Sound mode", "Vibrating mode", "Enlivening the phone :3", "Stop" };
            }
        }
    }
}
