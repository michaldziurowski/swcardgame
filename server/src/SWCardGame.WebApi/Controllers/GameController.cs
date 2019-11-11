using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SWCardGame.Core.Services;
using SWCardGame.WebApi.Contracts.Game;

namespace SWCardGame.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/game")]
    public class GameController : ControllerBase
    {
        private readonly IGameService gameService;
        private readonly ILogger<GameController> logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            this.gameService = gameService;
            this.logger = logger;
        }

        /// <summary>
        /// Deals a new game.
        /// </summary>
        [HttpGet]
        [Route("deal")]
        public async Task<IActionResult> Deal(string cardType, string propertyName)
        {
            try
            {
                var dealResult = await gameService.NewDeal(cardType, propertyName);

                return Ok(new DealResponse
                {
                    LeftCard = MapCardFromDomain(dealResult.LeftCard, isLeft: true, dealResult.Verdict, propertyName),
                    RightCard = MapCardFromDomain(dealResult.RightCard, isLeft: false, dealResult.Verdict, propertyName),
                    Verdict = (int)dealResult.Verdict
                });
            }
            catch (ArgumentException exc)
            {
                logger.LogError(exc.Message);
                return BadRequest();
            }
        }

        private DealResponseCard MapCardFromDomain(Core.Domain.Card card, bool isLeft, Core.Domain.Verdict verdict, string selectedPropertyName)
        {
            return new DealResponseCard
            {
                Name = card.Name,
                Result = verdict == Core.Domain.Verdict.Tie ? CardResult.Tie : (isLeft && verdict == Core.Domain.Verdict.Left) || (!isLeft && verdict == Core.Domain.Verdict.Right) ? CardResult.Winner : CardResult.Loser,
                Properties = card.Properties.Select(p => new CardProperty
                {
                    Name = p.Name,
                    Value = p.Value,
                    Selected = p.Name == selectedPropertyName
                }).ToList()
            };
        }
    }
}