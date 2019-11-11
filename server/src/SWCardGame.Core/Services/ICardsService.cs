using System.Collections.Generic;
using System.Threading.Tasks;

using SWCardGame.Core.Domain;

namespace SWCardGame.Core.Services
{
    public interface ICardsService
    {
        Task<IEnumerable<CardDefinition>> GetCardDefinitionsPaged(int pageNumber, int pageSize);

        Task<int> AddCard(AddCardRequest card);

        Task UpdateCard(UpdateCardRequest card);

        Task DeleteCard(int cardId);

        Task<IEnumerable<Card>> GetCardsPaged(int pageNumber, int pageSize);

        Task<Card> GetCard(int cardId);
    }
}