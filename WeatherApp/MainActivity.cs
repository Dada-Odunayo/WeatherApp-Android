using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Net.Http;


namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TextView temp,txtCity;
        TextView date;
        TextView condition;
        TextView lon;
        TextView lat;
        TextView speed;
        EditText search;
        Button searchBtn;
        ProgressDialogFragment progressDialog;
        ProgressDialog progress;

        //api = https://api.openweathermap.org/data/2.5/weather?q={city name}&appid={API key}&units=metric
        //api key 1c47f9c78f7420e7e598df755946dba9
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            temp = FindViewById<TextView>(Resource.Id.temp);
            txtCity = FindViewById<TextView>(Resource.Id.txtCity);
            date = FindViewById<TextView>(Resource.Id.date);
            condition = FindViewById<TextView>(Resource.Id.condition);
            lon = FindViewById<TextView>(Resource.Id.lon);
            lat = FindViewById<TextView>(Resource.Id.lat);
            speed = FindViewById<TextView>(Resource.Id.speed);
            search = FindViewById<EditText>(Resource.Id.search);
            searchBtn = FindViewById<Button>(Resource.Id.searchBtn);
            
            searchBtn.Click += FetchWeather_Click;
            search.Text = String.Empty;

        }
        public void ShowProgressDialog()
        {
            progress = new ProgressDialog(this);
            progress.Indeterminate = true;
            progress.SetProgressStyle(ProgressDialogStyle.Spinner);
            progress.SetMessage("Loading");
            progress.SetCancelable(false);
            progress.Show();
            //progressDialog = new ProgressDialogFragment();
            //var trans = SupportFragmentManager.BeginTransaction();
            //progressDialog.Cancelable = false;
            //progressDialog.Show(trans, "progress");
        }
        public void CloseProgressDialog()
        {
            progress.Cancel();
            //if (progressDialog != null)
            //{
            //    progressDialog.Dismiss();
            //    progressDialog = null;
            //}
        }

        private void FetchWeather_Click(object sender, EventArgs e)
        {
            string city_name = search.Text;
            if (string.IsNullOrEmpty(city_name))
            {
                Toast.MakeText(this, "Enter a valid city", ToastLength.Long).Show();
            }
            else
            {
                FetchWeatherData(city_name.Trim().ToLower());
            }
        }

        void FetchWeatherData(string city_name)
        {
            
            string unit = "metric";
            var API_key = "1c47f9c78f7420e7e598df755946dba9";
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city_name}&appid={API_key}&units={unit}";
            try
            {
                ShowProgressDialog();
                using (var client = new HttpClient())
                {
                    
                    var endpoint = new Uri(url);
                    var result = client.GetStringAsync(url).Result;
                    var resultObject = JObject.Parse(result);
                    var longitude = "Lon: " + resultObject["coord"]["lon"].ToString().Trim();
                    var latitude =  "Lat: " + resultObject["coord"]["lat"].ToString().Trim();
                    var desc = resultObject["weather"][0]["description"].ToString();
                    var icon = resultObject["weather"][0]["icon"].ToString();
                    var temperature = resultObject["main"]["temp"].ToString() + " °C";
                    var country = resultObject["sys"]["country"].ToString();
                    var city = resultObject["name"].ToString();
                    var windSpeed = "Wind Speed: " + resultObject["wind"]["speed"].ToString() + " m/s";
                    
                    date.Text = DateTime.UtcNow.ToLongDateString();
                    desc = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(desc);

                    condition.Text = desc;
                    temp.Text = temperature;
                    lon.Text = longitude;
                    lat.Text = latitude;
                    speed.Text = windSpeed;
                    txtCity.Text = city + ", " + country;
                    CloseProgressDialog();
                }
            }
            catch(Exception)
            {
                CloseProgressDialog();
                Toast.MakeText(this, "Something went wrong", ToastLength.Short);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}