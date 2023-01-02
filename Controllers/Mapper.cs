namespace proxy_app.Controllers
{
    public interface Mapper<TSource, TResult>
    {
        TSource mapTo (TResult source);
        TResult mapFrom (TSource result);

    }

    public class WeatherMapper : Mapper<string, WeatherForecast>
    {
        public WeatherForecast mapFrom(string result)
        {
            throw new NotImplementedException();
        }

        public string mapTo(WeatherForecast source)
        {
            throw new NotImplementedException();
        }
    }
}