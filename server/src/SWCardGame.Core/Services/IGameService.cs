using System.Threading.Tasks;

using SWCardGame.Core.Domain;

namespace SWCardGame.Core.Services
{
    public interface IGameService
    {
        Task<DealResult> NewDeal(string cardDefinitionKey, string propertyName);
    }
}