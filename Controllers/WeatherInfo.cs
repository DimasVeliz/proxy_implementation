namespace proxy_app.Controllers
{
    public class WeatherInfo
    {
        public int StatusCode { get; set; }
        public IEnumerable<WeatherForecast> ForecastList { get; set; }
    }
}