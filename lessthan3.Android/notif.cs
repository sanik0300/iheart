using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using меньше_3;

namespace меньше_3_Droid
{
    public class notif
    {
        private static bool channelexists = false;
        public const int ProperIdForNotifications = 1;

        private static NotificationCompat.Builder builder;
        private static NotificationManager manager;
        public static Notification blank(Context ctnx, string bigtext) {

            string CHANNELLID = "chnl";
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                manager = (NotificationManager)ctnx.GetSystemService(Context.NotificationService);
                if (!channelexists)
                    CreateNotificationChannel(manager, CHANNELLID);

                builder = new NotificationCompat.Builder(ctnx, CHANNELLID);        
            }
            else
            {
                builder = new NotificationCompat.Builder(ctnx);//да не ругайся ты а вдруг кто на тапке запускать будет
            }

            PendingIntent stomp = PendingIntent.GetBroadcast(ctnx, 2, new Intent(VerySimpleReceiver.filter_string), PendingIntentFlags.CancelCurrent);
            builder.SetAutoCancel(false).SetContentTitle(bigtext).SetSmallIcon(Resource.Drawable.whiteIcon)
                .AddAction(Resource.Drawable.abc_cab_background_top_material, "астанавитесь", stomp);
            return builder.Build();
        }
        public static void Ebash(string smalltext) { //it means "launch" (:
            if (builder == null)
                return;
            builder.SetContentText(smalltext);
            manager.Notify(ProperIdForNotifications, builder.Build());
        }
        static private void CreateNotificationChannel(NotificationManager manger, string chid)
        {
            NotificationChannel channel = new NotificationChannel(chid, "bonk", NotificationImportance.Default);
            manger.CreateNotificationChannel(channel);
            channelexists = true;
        }

        static internal void Obnull() //освобождаю ресурсы как учили
        {
            manager = null;
            builder = null;
        }
    }
}