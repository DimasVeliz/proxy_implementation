using Microsoft.AspNetCore.Mvc;

namespace proxy_app.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
   
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    private readonly Mapper<string,WeatherInfo> mapper;
    private readonly ProxyHandler proxyHandler;

    public WeatherForecastController(Mapper<string,WeatherInfo> mapper,ProxyHandler proxyHandler)
    {
        this.mapper = mapper;
        this.proxyHandler = proxyHandler;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<WeatherInfo>> Get()
    {
        
        var proxy_response = await proxyHandler.solveGet(this.HttpContext.Request);
        
        WeatherInfo response = mapper.mapFrom(proxy_response);
        return Ok(response);
    }

    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<ActionResult<WeatherInfo>> Post([FromBody] WeatherForecast entity)
    {
        
        var proxy_response = await proxyHandler.solvePost(this.HttpContext.Request);
        
        WeatherInfo response = mapper.mapFrom(proxy_response);
        return Ok(response);
    }
}
