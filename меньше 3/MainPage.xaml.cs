using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Android.App;
using System.Globalization;

namespace меньше_3
{
    public partial class MainPage : ContentPage
    {
        static bool beating = false;
        static TimeSpan tsp;
        static uint seconds_sofar;
        static TimerCallback callback = new TimerCallback(target);
        static Timer timer;
        private float frequency = 60;
        static Activity uiaccess = new Activity();
        IBonk bonker;
        static ILocal lol;
        int saved_duration;
        static Label link_to_indic;
        static TimePicker link_to_timepicker;
        static Button link_to_button;
        public MainPage()
        {
            InitializeComponent();
            link_to_indic = this.indic;
            link_to_timepicker = this.setspan;
            link_to_button = this.onoff;
            bonker = DependencyService.Get<IBonk>();
            lol = DependencyService.Get<ILocal>().GetCurrentCultureInfo();
            saved_duration = bonker.duration;
        }

        private static void target(object lel)
        {
            beating = seconds_sofar < tsp.TotalSeconds;
            seconds_sofar = seconds_sofar + 1;
            uiaccess.RunOnUiThread(whattime);
        }
        private static void whattime() 
        {
            link_to_indic.Text = beating ? new TimeSpan(seconds_sofar*10000000).ToString()+"/" : null;
            if (beating)
                return;
            EndTimeredBeating();
            link_to_button.Text = lol.words["vkl"];
        }

        private static void EndTimeredBeating()
        {
            timer.Dispose();
            link_to_timepicker.Time = new TimeSpan(0);
            seconds_sofar = 0;
        }   

        private void Button_Clicked(object sender, EventArgs e)
        {                     
            beating = !beating;
            setspan.IsEnabled = !beating;
            onoff.Text = beating ? lol.words["vkl"] : lol.words["vikl"];                    
            if (!beating)
            {
                indic.Text = null;
                if (nopalevo.IsToggled)
                    bonker.Release_Ress();
                if (timer != null)
                    EndTimeredBeating();
            }
            else
            {
                if (setspan.Time.TotalSeconds > 1)
                    timer = new Timer(callback, indic, 0, 1000);
                Beat();
            }        
        }
        private async void Beat()
        {
            while(beating)
            {
                await Task.Delay(Convert.ToInt32(1750*(60/frequency)) - (nopalevo.IsToggled? saved_duration : 350));
                if (!beating)
                    return;
                if (nopalevo.IsToggled)
                    bonker.Bonk();
                else
                {
                    Vibration.Vibrate(100);
                    await Task.Delay(150);
                    Vibration.Vibrate(100);
                }
            }
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            mode.Text = nopalevo.IsToggled ? $"{lol.words["shh"]} 🎧" : $"{lol.words["vibr"]} 📳";
        }

        private void setspan_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == TimePicker.TimeProperty.PropertyName)
                tsp = setspan.Time;         
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            frequency = Convert.ToSingle(slider.Value);
            frqer.Text = Math.Round(frequency).ToString();
        }
    }
}
