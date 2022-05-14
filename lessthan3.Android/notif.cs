using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Xamarin.Forms;
using меньше_3;

namespace меньше_3_Droid
{
    public static class notif
    {
        private static bool channelexists = false;
        public const int ProperIdForNotifications = 1;

        private static NotificationCompat.Builder builder;
        private static NotificationManager manager;

        private static NotificationCompat.Builder blank(Context ctnx)
        {
            string CHANNELLID = "chnl";
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                manager = (NotificationManager)ctnx.GetSystemService(Context.NotificationService);
                if (!channelexists)
                    CreateNotificationChannel(manager, CHANNELLID);

                return new NotificationCompat.Builder(ctnx, CHANNELLID);
            }
            else
            {
                return new NotificationCompat.Builder(ctnx);//да не ругайся ты а вдруг кто на тапке запускать будет
            }
        }
        private static void AddTerminatingAction(this NotificationCompat.Builder b, Context context)
        {
            PendingIntent pi = PendingIntent.GetBroadcast(context, 2, new Intent(SoundEnder.StopByUser), PendingIntentFlags.CancelCurrent);
            b.AddAction(Resource.Drawable.abc_cab_background_top_material, Localize.wws[(int)Sentences.Stop], pi);
        }

        public static Notification running(Context ctnx) 
        {
            builder = blank(ctnx);

            builder.SetAutoCancel(false).SetContentTitle(Localize.wws[(int)Sentences.Title]).SetSmallIcon(Resource.Drawable.whiteIcon).AddTerminatingAction(ctnx);
            
            return builder.Build();
        }

        public static Notification waiting_during_call(Context ctx)
        {
            builder = blank(ctx);

            builder.SetContentTitle("Ждём конца звонка...").SetSmallIcon(Resource.Drawable.whiteIcon)
                .SetLargeIcon(BitmapFactory.DecodeResource(ctx.Resources, Resource.Drawable.questIcon)).AddTerminatingAction(ctx);            
            
            return builder.Build();
        }

        public static void JustEbashAny(Notification nn) { manager.Notify(ProperIdForNotifications, nn); }

        public static void EbashChangedTime(string smalltext) { //it means "launch" (:
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