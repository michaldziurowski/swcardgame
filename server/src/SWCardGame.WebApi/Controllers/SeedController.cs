using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SWCardGame.Persistence;

namespace SWCardGame.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/seed")]
    public class SeedController : ControllerBase
    {
        private readonly TestDataProvider dataProvider;

        public SeedController(TestDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        /// <summary>
        /// Seeds database with test data
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await dataProvider.Seed();
            return Ok();
        }
    }
}