using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SWCardGame.Core.Domain;
using SWCardGame.Core.Interfaces;

namespace SWCardGame.Persistence
{
    public class CardsRepository : ICardsRepository
    {
        private readonly SWCardGameContext context;

        public CardsRepository(SWCardGameContext context)
        {
            this.context = context;
        }

        public async Task<int> AddCard(AddCardRequest card)
        {
            var cardDefinition = await context.CardDefinitions.FindAsync(card.DefinitionId);

            var cardEntity = new Entities.Card
            {
                Definition = cardDefinition,
                Name = card.Name,
                Properties = JsonConvert.SerializeObject(card.Properties)
            };

            context.Cards.Add(cardEntity);
            await context.SaveChangesAsync();

            return cardEntity.Id;
        }

        public async Task DeleteCard(int cardId)
        {
            var cardEntityToDelete = new Entities.Card
            {
                Id = cardId
            };

            context.Cards.Remove(cardEntityToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<Card> GetCard(int cardId)
        {
            var cardEntity = await context.Cards.Include(c => c.Definition).SingleAsync(c => c.Id == cardId);

            return MapEntityToCard(cardEntity);
        }

        public async Task<CardDefinition> GetCardDefinition(int cardDefinitionId)
        {
            var cardDefinitionEntity = await context.CardDefinitions.FindAsync(cardDefinitionId);

            return MapEntityToCardDefinition(cardDefinitionEntity);
        }

        public async Task<CardDefinition> GetCardDefinitionByKey(string cardDefinitionKey)
        {
            var cardDefinitionEntity = await context.CardDefinitions.SingleAsync(d => d.Key == cardDefinitionKey);

            return MapEntityToCardDefinition(cardDefinitionEntity);
        }

        public async Task<IEnumerable<CardDefinition>> GetCardDefinitionsPaged(int pageNumber, int pageSize)
        {
            return (await context.CardDefinitions.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArrayAsync()).Select(MapEntityToCardDefinition);
        }

        public async Task<IEnumerable<Card>> GetCardsPaged(int pageNumber, int pageSize)
        {
            return (await context.Cards.Include(c => c.Definition).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArrayAsync()).Select(MapEntityToCard);
        }

        public async Task<Card> GetRandomCard(string cardDefinitionKey)
        {
            var cardsOfDefinitionCount = await context.Cards.Where(c => c.Definition.Key == cardDefinitionKey).CountAsync();

            if (cardsOfDefinitionCount == 0)
            {
                return null;
            }

            var randomSkip = (int)(new Random().NextDouble() * cardsOfDefinitionCount);
            var cardEntity = await context.Cards.Include(c => c.Definition).Where(c => c.Definition.Key == cardDefinitionKey).Skip(randomSkip).Take(1).FirstAsync();

            return MapEntityToCard(cardEntity);
        }

        public async Task UpdateCard(UpdateCardRequest card)
        {
            var cardEntity = await context.Cards.FindAsync(card.Id);
            cardEntity.Name = card.Name;

            var existingProperties = JsonConvert.DeserializeObject<IEnumerable<Property>>(cardEntity.Properties);
            foreach (var propToUpdate in card.Properties)
            {
                existingProperties.First(p => p.Name == propToUpdate.Name).Value = propToUpdate.Value;
            }
            cardEntity.Properties = JsonConvert.SerializeObject(existingProperties);

            await context.SaveChangesAsync();
        }

        private Card MapEntityToCard(Entities.Card cardEntity) => new Card
        {
            Id = cardEntity.Id,
            Name = cardEntity.Name,
            Definition = MapEntityToCardDefinition(cardEntity.Definition),
            Properties = JsonConvert.DeserializeObject<List<Property>>(cardEntity.Properties)
        };

        private CardDefinition MapEntityToCardDefinition(Entities.CardDefinition cardDefinitionEntity) => new CardDefinition
        {
            Id = cardDefinitionEntity.Id,
            Key = cardDefinitionEntity.Key,
            Name = cardDefinitionEntity.Name,
            Properties = cardDefinitionEntity.Properties.Split(',').ToArray()
        };
    }
}