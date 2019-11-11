using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SWCardGame.Core.Domain;
using SWCardGame.Core.Interfaces;

namespace SWCardGame.Core.Services
{
    public class CardsService : ICardsService
    {
        private readonly ICardsRepository cardsRepository;

        public CardsService(ICardsRepository cardsRepository)
        {
            this.cardsRepository = cardsRepository;
        }

        public async Task<IEnumerable<CardDefinition>> GetCardDefinitionsPaged(int pageNumber, int pageSize)
                => await cardsRepository.GetCardDefinitionsPaged(pageNumber, pageSize);

        public async Task<int> AddCard(AddCardRequest card)
        {
            var cardDefinition = await cardsRepository.GetCardDefinition(card.DefinitionId);
            if (cardDefinition == null)
            {
                throw new ArgumentException($"Cannot find card definition {card.DefinitionId}.");
            }

            if (ValidateIfCardMatchDefinition(card, cardDefinition))
            {
                throw new ArgumentException($"Provided card doesnt match definition {card.DefinitionId}.");
            }

            return await cardsRepository.AddCard(card);
        }

        public async Task UpdateCard(UpdateCardRequest card)
        {
            var existingCard = await cardsRepository.GetCard(card.Id);
            if (existingCard == null)
            {
                throw new ArgumentException($"Cannot update card. Card with id {card.Id} doesnt exist.");
            }

            if (ValidateIfPropertiesBelongToDefinition(card.Properties, existingCard.Definition))
            {
                throw new ArgumentException($"Cannot update card. Property doesnt match any of definition properties");
            }

            await cardsRepository.UpdateCard(card);
        }

        public async Task DeleteCard(int cardId)
        {
            var existingCard = await cardsRepository.GetCard(cardId);
            if (existingCard == null)
            {
                throw new ArgumentException($"Cannot delete card. Card with id {cardId} doesnt exist.");
            }

            await cardsRepository.DeleteCard(cardId);
        }

        public async Task<IEnumerable<Card>> GetCardsPaged(int pageNumber, int pageSize) => await cardsRepository.GetCardsPaged(pageNumber, pageSize);

        public async Task<Card> GetCard(int cardId)
        {
            var existingCard = await cardsRepository.GetCard(cardId);
            if (existingCard == null)
            {
                throw new ArgumentException($"Card with id {cardId} doesnt exist.");
            }

            return await cardsRepository.GetCard(cardId);
        }

        private bool ValidateIfCardMatchDefinition(AddCardRequest card, CardDefinition cardDefinition)
        {
            return cardDefinition.Properties.Any(p => !card.Properties.Any(cp => cp.Name == p));
        }

        private bool ValidateIfPropertiesBelongToDefinition(IEnumerable<Property> properties, CardDefinition definition)
        {
            return properties.Any(p => !definition.Properties.Contains(p.Name));
        }
    }
}