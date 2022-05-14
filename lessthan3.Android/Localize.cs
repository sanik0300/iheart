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
        internal static readonly string[] wws;
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

        static Localize()
        {
            Locale android_locale = Locale.Default;
            string net_lan = android_locale.ToString().Replace('_', '-');
            string lang_name = new CultureInfo(net_lan).ThreeLetterISOLanguageName;

            if (lang_name == "rus" || lang_name == "ukr" || lang_name == "bel" || lang_name == "kaz")
            {
                wws = new string[12] {
                    "ВКЛ", "ВЫКЛ", "Беспалевный режим", "Палевный режим", "Оживляем телефон :3", "Остановить", "Звонок мешает воспроизведению",
                    "ждём конца звонка", "Аудио уже есть", "всё равно проигрывать?", "да", "нет"

                };
            }
            else
            {
                wws = new string[12] { "ON", "OFF", "Sound mode", "Vibrating mode", "Enlivening the phone :3", "Stop", "The current call prevents all the playback",
                "waiting till the end of the call", "Some audio is already playing", "start anyway?", "yes", "no"};
            }
        }
    }
}
