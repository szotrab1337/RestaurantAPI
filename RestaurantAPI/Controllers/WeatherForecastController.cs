using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var result = _service.Get();
        //    return result;
        //}

        [HttpPost("generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Generate([FromQuery] int quantity, [FromBody]TemperatureRequest temperatureRequest)
        {
            if (quantity <= 0 || temperatureRequest.Min > temperatureRequest.Max)
            {
                return BadRequest();
            }

            var result = _service.Get(quantity, temperatureRequest.Min, temperatureRequest.Max);
            return Ok(result);
        }
    }
}