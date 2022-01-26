using Android.App;
using меньше_3;
using Xamarin.Forms;
using Android.Media;
using Android.Content;

[assembly: Dependency(typeof(меньше_3_Droid.HeartBeatActivity))]
namespace меньше_3_Droid
{
    class HeartBeatActivity : Activity, IBonk
    {
        static private Context _cntx;
        static public Context context { set{
                _cntx = value;    
        } }

        public int duration
        {
            get { return mpl.Duration; }
        }
        private MediaPlayer mpl = MediaPlayer.Create(_cntx, Resource.Raw.hbeat);        

        void IBonk.Bonk()
        {
            if (mpl == null)
                mpl = MediaPlayer.Create(_cntx, Resource.Raw.hbeat); 
            mpl.Start();
        }
        void IBonk.Release_Ress()
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