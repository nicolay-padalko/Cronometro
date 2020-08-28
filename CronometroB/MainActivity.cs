using Android.App;
using Android.Widget;
using Android.OS;
using System.Timers;
using System;
using Android.Views;
using Android.Content;

namespace CronometroB
{
    [Activity(Label = "CronometroB", MainLauncher = true)]
    public class MainActivity : Activity
    {
          
        Button btnStart, btnPause, btnLap;
        TextView txtTimer;
        LinearLayout container;
        Timer timer;
        int mins = 0, secs = 0, millisecond = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Main);
            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnPause = FindViewById<Button>(Resource.Id.btnPause);
            btnLap = FindViewById<Button>(Resource.Id.btnLap);
            container = FindViewById<LinearLayout>(Resource.Id.container);
            txtTimer = FindViewById<TextView>(Resource.Id.txtTimer);
            btnStart.Click += delegate {
                timer = new Timer();
                timer.Interval = 1; // 1 Milliseconds  
                timer.Elapsed += Timer_Elapsed;
                timer.Start();
            };
            btnPause.Click += delegate {
                timer.Stop();
                timer = null;
            };
            btnLap.Click += delegate {
                LayoutInflater inflater = (LayoutInflater)BaseContext.GetSystemService(Context.LayoutInflaterService);
                View addView = inflater.Inflate(Resource.Layout.row, null);
                TextView textContent = addView.FindViewById<TextView>(Resource.Id.textView1);
                textContent.Text = txtTimer.Text;
                container.AddView(addView);
            };
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            millisecond++;
            if (millisecond > 1000)
            {
                secs++;
                millisecond = 0;
            }
            if (secs == 59)
            {
                mins++;
                secs = 0;
            }
            RunOnUiThread(() => {
                txtTimer.Text = String.Format("{0}:{1:00}:{2:000}", mins, secs, millisecond);
            });
        }
    }
}


