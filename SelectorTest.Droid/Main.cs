using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace SelectorTest.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Main : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var droid = FindViewById<Button>(Resource.Id.droid);

            droid.Click += Droid_Click;
        }

        private void Droid_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(DroidSelector));
        }
    }
}