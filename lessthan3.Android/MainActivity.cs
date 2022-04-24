using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using меньше_3;
using Android;
using Android.Support.V4.App;
using Android.Telephony;

namespace меньше_3_Droid
{
    [Activity(Label = "<3", Icon = "@drawable/hicon", LaunchMode = LaunchMode.SingleInstance, Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            SoundPlay.context = this;

            LoadApplication(new App());
        }
        private VerySimpleReceiver recVer;
        protected override void OnStart()
        {
            base.OnStart();
            recVer = new VerySimpleReceiver();
            RegisterReceiver(recVer, new Android.Content.IntentFilter(TelephonyManager.ActionPhoneStateChanged));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            /*for(int i =0; i < permissions.Length; i++)
            {
                if(permissions[i] == Manifest.Permission.ReadPhoneState && grantResults[i] == Permission.Granted)
                {
                    
                }
            }*/
        }
        protected override void OnStop()
        {
            UnregisterReceiver(recVer);
            base.OnStop();
        }
    }
}