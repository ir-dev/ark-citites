using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ark.immanuel.cities.web.Api
{
    [Route("api/{country}")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        [Route("cities/{search}")]
        public async Task<dynamic> LocationRequest([FromRoute] string country, [FromRoute] string search)
        {
            return SearchManager.SearchCities(country, search);
        }
        [HttpGet]
        [Route("pincode/{search}")]
        public async Task<dynamic> PincodeSearch([FromRoute] string country, [FromRoute] string search)
        {
            return SearchManager.SearchPincode(country, search);
        }
    }
}
