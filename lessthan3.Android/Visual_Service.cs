using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Telephony;

namespace меньше_3_Droid
{
    [Service]//редкий случай когда оно работает, не трогай))
    public class Visual_Service : Service
    {
        private SoundEnder receiver;
        private lil_listener listen;
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            receiver = new SoundEnder();
            RegisterReceiver(receiver, new IntentFilter(SoundEnder.StopByUser));
            
            listen = new lil_listener();        
            ((TelephonyManager)GetSystemService("phone")).Listen(listen, PhoneStateListenerFlags.CallState);
            //"phone" это типа Context.GetSystemService(что-то_там.telephonymanager) но зачем когда и так его знаю :))

            this.StartForeground(notif.ProperIdForNotifications, notif.running(SoundPlay.context));
            return base.OnStartCommand(intent, flags, startId);
        }
        public override void OnDestroy()
        {
            this.StopForeground(true);
            UnregisterReceiver(receiver);
            listen = null; //damn couldn`t find a method to unregister listeners like receivers
            notif.Obnull();
            base.OnDestroy();
        }
    }
}