using System.Collections.Generic;
using System.Threading.Tasks;
using SWCardGame.Core.Domain;

namespace SWCardGame.Core.Interfaces
{
    public interface ICardsRepository
    {
        Task<Card> GetRandomCard(string cardDefinitionKey);

        Task<CardDefinition> GetCardDefinition(int cardDefinitionId);

        Task<CardDefinition> GetCardDefinitionByKey(string cardDefinitionKey);

        Task<IEnumerable<CardDefinition>> GetCardDefinitionsPaged(int pageNumber, int pageSize);

        Task<Card> GetCard(int cardId);

        Task<int> AddCard(AddCardRequest card);

        Task UpdateCard(UpdateCardRequest card);

        Task DeleteCard(int cardId);

        Task<IEnumerable<Card>> GetCardsPaged(int pageNumber, int pageSize);

    }
}