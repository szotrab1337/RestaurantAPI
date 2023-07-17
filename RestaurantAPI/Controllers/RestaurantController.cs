using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var userId = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpGet]
        [Authorize(Policy = "CreatedAtleast2Restaurants")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();

            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Atleast20")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "HasNationality")]
        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
            _restaurantService.Update(id, dto);

            return Ok();
        }
    }
}
