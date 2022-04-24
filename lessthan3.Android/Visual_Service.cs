using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace меньше_3_Droid
{
    [Service]//редкий случай когда оно работает, не трогай))
    public class Visual_Service : Service
    {
        private VerySimpleReceiver receiver;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            receiver = new VerySimpleReceiver();
            RegisterReceiver(receiver, new IntentFilter(VerySimpleReceiver.filter_string));
            this.StartForeground(notif.ProperIdForNotifications, notif.blank(SoundPlay.context, intent.GetStringExtra("title")));
            return base.OnStartCommand(intent, flags, startId);
        }
        public override void OnDestroy()
        {
            this.StopForeground(true);
            UnregisterReceiver(receiver);
            notif.Obnull();
            base.OnDestroy();
        }
    }
}