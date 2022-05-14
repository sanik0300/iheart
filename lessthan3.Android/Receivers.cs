using Android.Content;
using Android.Runtime;
using Android.Telephony;
using Xamarin.Forms;
using System.Diagnostics;
using System.Text;

namespace меньше_3_Droid
{
    interface IDistantChanger
    {
        string MessageAnyway { get; }
    }

    /// <summary>
    /// former very simple receiver, it is not so simple now
    /// </summary>
    [BroadcastReceiver(Enabled = true)]
    public class SoundEnder : BroadcastReceiver, IDistantChanger
    {
        public const string StopByUser = "just_stop";
        public string MessageAnyway { get { return StopByUser; } }

        public override void OnReceive(Context context, Intent intent)
        {
            MessagingCenter.Send(this, StopByUser);
        }    
    }

    class lil_listener : PhoneStateListener, IDistantChanger
    {
        private bool firsttime = true;
        public const string StopByCall = "call_began", CallEndedPlayAgain = "replay";

        private string whathappened = StopByCall;
        public string MessageAnyway { get { return whathappened; } }

        public override void OnCallStateChanged([GeneratedEnum] CallState state, string phoneNumber)
        {
            base.OnCallStateChanged(state, phoneNumber);
            if (!firsttime)
            {
                StringBuilder sb = new StringBuilder("АААА ");
                switch (state)
                {
                    case CallState.Ringing:
                        sb.Append("тебе звонят");
                        whathappened = StopByCall;
                        break;
                    case CallState.Idle:
                        sb.Append("вот и поговорили");
                        whathappened = CallEndedPlayAgain;
                        break;
                }
                Debug.WriteLine(sb.ToString());
                MessagingCenter.Send(this, whathappened);
            }
            firsttime = false;
        }
    }
}