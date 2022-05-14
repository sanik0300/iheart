using меньше_3;
using Xamarin.Forms;
using Android.Media;
using Android.Content;
using System;
using Android.Telephony;
using Android.Widget;

[assembly: Dependency(typeof(меньше_3_Droid.SoundPlay))]
namespace меньше_3_Droid
{
    class SoundPlay : IBonk
    {
        static private Context _cntx;
        static public Context context { get { return _cntx; } set { _cntx = value; } }

        public int duration
        {
            get { return mpl.Duration; }
        }

        public bool InteractWithServiceThisTime { get; private set; }

        private MediaPlayer mpl = MediaPlayer.Create(_cntx, Resource.Raw.hbeat);

        bool IBonk.AlreadyMusic()
        {
            return ((AudioManager)_cntx.GetSystemService(Context.AudioService)).IsMusicActive;
        }

        bool IBonk.CallTakesPlace(string toasttxt)
        {
            bool conclusion = ((TelephonyManager)_cntx.GetSystemService(Context.TelephonyService)).CallState != CallState.Idle;
            if (conclusion)
                Toast.MakeText(_cntx, toasttxt, ToastLength.Short).Show();
            return conclusion;
        }

        void IBonk.SingleBonk()
        {
            if (mpl == null)
                mpl = MediaPlayer.Create(_cntx, Resource.Raw.hbeat); 
            mpl.Start();
        }

        void IBonk.BeginDisplaying(string title)
        {           
            _cntx.StartService(new Intent(_cntx, typeof(Visual_Service)));

            MessagingCenter.Subscribe<SoundEnder>(this, SoundEnder.StopByUser, MessageToMainpage);
            MessagingCenter.Subscribe<lil_listener>(this, lil_listener.StopByCall, MessageToMainpage);
            MessagingCenter.Subscribe<lil_listener>(this, lil_listener.CallEndedPlayAgain, MessageToMainpage);

        }
        private void MessageToMainpage(IDistantChanger s_ender)
        {
            this.InteractWithServiceThisTime = false;
            switch (s_ender.MessageAnyway)
            {
                case lil_listener.StopByCall:
                    notif.JustEbashAny(notif.waiting_during_call(_cntx));
                    break;
                case lil_listener.CallEndedPlayAgain:
                    notif.JustEbashAny(notif.running(_cntx));
                    break;
                default:
                    this.InteractWithServiceThisTime = true;
                    break;
            }
            MessagingCenter.Send<IBonk>(this, "command_received");
        }

        void IBonk.EndDisplaying() 
        {
            MessagingCenter.Unsubscribe<SoundEnder>(this, SoundEnder.StopByUser);
            MessagingCenter.Unsubscribe<lil_listener>(this, lil_listener.StopByCall);
            MessagingCenter.Unsubscribe<lil_listener>(this, lil_listener.CallEndedPlayAgain);

            _cntx.StopService(new Intent(_cntx, typeof(Visual_Service))); 
        }

        void IBonk.ReportTimeLeft(TimeSpan tsp) { notif.EbashChangedTime(tsp.ToString()); }

        void IBonk.Stop_and_Release_Ress()
        {        
            if (mpl.IsPlaying)
                mpl.Pause();
            mpl.Stop();
            mpl.Release();
            mpl.Dispose();
            mpl = null;
        }
    }
}