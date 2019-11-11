using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SWCardGame.Core.Services;

namespace SWCardGame.WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/cards")]
    public class CardsController : ControllerBase
    {
        private readonly ICardsService cardsService;
        private readonly ILogger<CardsController> logger;

        public CardsController(ICardsService cardsService, ILogger<CardsController> logger)
        {
            this.cardsService = cardsService;
            this.logger = logger;
        }

        /// <summary>
        /// Returns card definitions by page
        /// </summary>
        [HttpGet("definitions")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDefinitions(int page = 1, int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest();
            }

            var definitions = await cardsService.GetCardDefinitionsPaged(page, pageSize);

            return Ok(definitions.Select(d =>
                    new Contracts.Cards.CardDefinitionResponse
                    {
                        Id = d.Id,
                        Key = d.Key,
                        Name = d.Name,
                        Properties = d.Properties.ToArray()
                    })
                );
        }

        /// <summary>
        /// Returns cards by page
        /// </summary>
        [HttpGet(Name = nameof(GetCardsPaged))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCardsPaged(int page = 1, int pageSize = 10)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest();
            }

            var domainCards = await cardsService.GetCardsPaged(page, pageSize);

            return Ok(domainCards.Select(MapDomainToCardResponse));
        }

        /// <summary>
        /// Returns card by id 
        /// </summary>
        [HttpGet("{cardId}", Name = nameof(GetCard))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCard(int cardId)
        {
            try
            {
                var domainCard = await cardsService.GetCard(cardId);

                return Ok(MapDomainToCardResponse(domainCard));
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Adds card 
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddCard(Contracts.Cards.AddCardRequest card, ApiVersion version)
        {
            try
            {
                var domainCard = new Core.Domain.AddCardRequest
                {
                    DefinitionId = card.DefinitionId,
                    Name = card.Name,
                    Properties = card.Properties.Select(p => new Core.Domain.Property
                    {
                        Name = p.Name,
                        Value = p.Value
                    })
                };

                var addedCardId = await cardsService.AddCard(domainCard);
                return CreatedAtAction(nameof(GetCard), new { version = version.ToString(), cardId = addedCardId }, null);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates card 
        /// </summary>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCard(Contracts.Cards.UpdateCardRequest card)
        {
            try
            {
                var domainCard = new Core.Domain.UpdateCardRequest
                {
                    Id = card.Id,
                    Name = card.Name,
                    Properties = card.Properties.Select(p => new Core.Domain.Property
                    {
                        Name = p.Name,
                        Value = p.Value
                    })
                };

                await cardsService.UpdateCard(domainCard);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes card by id 
        /// </summary>
        [HttpDelete("{cardId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCard(int cardId)
        {
            try
            {
                await cardsService.DeleteCard(cardId);
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        private Contracts.Cards.CardResponse MapDomainToCardResponse(Core.Domain.Card domainCard) => new Contracts.Cards.CardResponse
        {
            Id = domainCard.Id,
            DefinitionId = domainCard.Definition.Id,
            Name = domainCard.Name,
            Properties = domainCard.Properties.Select(p => new Contracts.Cards.CardProperty
            {
                Name = p.Name,
                Value = p.Value
            })
        };

    }
}