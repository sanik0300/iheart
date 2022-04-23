using Android.App;
using Android.Content;
using Xamarin.Forms;


namespace меньше_3_Droid
{
    [BroadcastReceiver(Enabled = true)]
    public class VerySimpleReceiver : BroadcastReceiver
    {
        public const string filter_string = "just_stop";
        public const string StopWordForMessages = "Astanavis";
        public override void OnReceive(Context context, Intent intent)
        {
            MessagingCenter.Send(this, StopWordForMessages);
        }
    }
}