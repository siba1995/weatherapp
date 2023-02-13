using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace weatherapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string apikey = "a17c8fa929142f1bd94af17a94d917c4";
        private string requesturl = "https://api.openweathermap.org/data/2.5/weather";
        public MainWindow()
        {
            InitializeComponent();
            updateUi("Heilbronn");
         
             
        }
        public void updateUi(string city)
        {
            Weathermapresponse result = GetWeatherData(city);
            string finalImage = "sun.png";
            string currentweather = result.weather[0].main.ToLower();
            if (currentweather.Contains("cloud"))
            {
                finalImage = "cloud.png";
            }
            else if (currentweather.Contains("rain"))
            {
                finalImage = "rain.png";
            }
            else if (currentweather.Contains("snow"))
            {
                finalImage = "snow.png";
            }
            backgroungImage.ImageSource = new BitmapImage(new Uri("images/" + finalImage, UriKind.Relative));
            labeltemprature.Content = result.main.temp.ToString("F1") + " °C";  //f1 float1 ein comma stelle
            Labelinfo.Content = result.weather[0].main;

        }
        public Weathermapresponse GetWeatherData(string city)
        {
            HttpClient httpClient = new HttpClient();
            var finalUri = requesturl + "?q=" + city + "&appid=" + apikey + "&units=metric";
            HttpResponseMessage httpResponse = httpClient.GetAsync(finalUri).Result;
            string response = httpResponse.Content.ReadAsStringAsync().Result;
            Weathermapresponse weathermapresponse = JsonConvert.DeserializeObject<Weathermapresponse>(response);
            return weathermapresponse;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string query = textbox.Text;
            updateUi(query);
        }
    }
}
