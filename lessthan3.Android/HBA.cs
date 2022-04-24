using Android.App;
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
        
        public string StopWordForMessages { get { return "xvatit"; } }

        public int duration
        {
            get { return mpl.Duration; }
        }
        private MediaPlayer mpl = MediaPlayer.Create(_cntx, Resource.Raw.hbeat);

        bool IBonk.AlreadyMusic()
        {
            return ((AudioManager)_cntx.GetSystemService(Context.AudioService)).IsMusicActive;
        }

        bool IBonk.CallTakesPlace(string toasttxt)
        {
            bool conclusion = ((TelephonyManager)_cntx.GetSystemService(Context.TelephonyService)).CallState != CallState.Idle;
            if (conclusion)
                Toast.MakeText(_cntx, toasttxt, ToastLength.Short);
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
            Intent lestart = new Intent(_cntx, typeof(Visual_Service)).PutExtra("title", title);
            _cntx.StartService(lestart);
            MessagingCenter.Subscribe<VerySimpleReceiver>(this, VerySimpleReceiver.StopWordForMessages, MessageToMainpage);
        }
        private void MessageToMainpage(VerySimpleReceiver sender) //и нафига мне этот сендер
        {
            MessagingCenter.Send<IBonk>(this, StopWordForMessages);
        }

        void IBonk.EndDisplaying() 
        {
            MessagingCenter.Unsubscribe<VerySimpleReceiver>(this, VerySimpleReceiver.StopWordForMessages);
            _cntx.StopService(new Intent(_cntx, typeof(Visual_Service))); 
        }

        void IBonk.ReportTimeLeft(TimeSpan tsp) { notif.Ebash(tsp.ToString()); }

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