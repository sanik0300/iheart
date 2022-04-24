using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace меньше_3
{
    public partial class MainPage : ContentPage
    {
        static bool beating = false;
        /// <summary>
        /// на сколько сек выставлен таймер
        /// </summary>
        static TimeSpan tsp;
        /// <summary>
        /// сколько сек прошло
        /// </summary>
        static uint seconds_sofar;
        static TimerCallback callback = new TimerCallback(target);
        static Timer timer;
        private float frequency = 60;
        static IBonk bonker;
        static ILocal lol;
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
            lol = DependencyService.Get<ILocal>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<IBonk>(this, bonker.StopWordForMessages, ReceiveIbonkMessge);
            PerharpsChangeTheme();
        }
        public void PerharpsChangeTheme() {
            if (!lol.NeedToDarkenLeTheme())
                return;
            mode.TextColor = Color.White;
            bpms.BackgroundColor = Color.Transparent;
            bpms.ForegroundColor = Color.White;
        }
        private void ReceiveIbonkMessge(IBonk sender) { Button_Clicked(sender, null); }

        private static void update_timer_text() { link_to_indic.Text = beating ? new TimeSpan(seconds_sofar * 10000000).ToString() + "/" : null; }
        private static void target(object lel)
        {
            beating = seconds_sofar < tsp.TotalSeconds;
            seconds_sofar = seconds_sofar + 1;
            //всегда прокатывало писать прям так а теперь прости господи util exception у него
            Device.BeginInvokeOnMainThread(update_timer_text);
            
            bonker.ReportTimeLeft(new TimeSpan(((uint)tsp.TotalSeconds - seconds_sofar) * 10000000));

            if (beating)
                return;
            EndTimeredBeating();
            link_to_button.Text = lol.words[(int)Sentences.On];
        }

        private static void EndTimeredBeating()
        {
            timer.Dispose();
            link_to_timepicker.Time = new TimeSpan(0);
            seconds_sofar = 0;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (!beating) {
                if (bonker.CallTakesPlace(lol.words[(int)Sentences.ThereIsCall]))
                    return;

                if (nopalevo.IsToggled && bonker.AlreadyMusic()) {
                    if (!(await DisplayAlert("Аудио уже есть", "всё равно проигрывать?", "да", "нет")))
                        return;
                }
            }

            beating = setspan.IsEnabled = !beating;
            onoff.Text = beating ? lol.words[(int)Sentences.Off] : lol.words[(int)Sentences.On];                    
            if (!beating) {
                indic.Text = null;
                if (nopalevo.IsToggled)
                    bonker.Stop_and_Release_Ress();
                if (timer != null)
                    EndTimeredBeating();

                bonker.EndDisplaying();
            }
            else
            {
                bonker.BeginDisplaying(lol.words[(int)Sentences.Title]);
                if (setspan.Time.TotalSeconds > 1)
                    timer = new Timer(callback, indic, 0, 1000);
                DoABeat();              
            }        
        }
        private async void DoABeat()
        {
            while(beating)
            {
                if (nopalevo.IsToggled)
                    bonker.SingleBonk();
                else {
                    Vibration.Vibrate(100);
                    await Task.Delay(150);
                    Vibration.Vibrate(100);
                }
                if (!beating)
                    return;
                await Task.Delay(Convert.ToInt32(1750 * (60 / frequency)) - (nopalevo.IsToggled ? bonker.duration : 350));
            }         
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            mode.Text = nopalevo.IsToggled ? $"{lol.words[(int)Sentences.Shh]} 🎧" : $"{lol.words[(int)Sentences.Vibr]} 📳";
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
