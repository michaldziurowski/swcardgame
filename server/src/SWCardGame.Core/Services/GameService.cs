using System;
using System.Threading.Tasks;
using SWCardGame.Core.Domain;
using SWCardGame.Core.Interfaces;

namespace SWCardGame.Core.Services
{
    public class GameService : IGameService
    {
        private readonly ICardsRepository cardRepository;

        public GameService(ICardsRepository cardRepository)
        {
            this.cardRepository = cardRepository;
        }

        public async Task<DealResult> NewDeal(string cardDefinitionKey, string propertyName)
        {
            var dealResult = new DealResult();
            var leftCard = await cardRepository.GetRandomCard(cardDefinitionKey);
            var rightCard = await cardRepository.GetRandomCard(cardDefinitionKey);

            if (leftCard == null || rightCard == null)
            {
                throw new ArgumentException($"Cannot find cards of definition {cardDefinitionKey}.");
            }

            dealResult.LeftCard = leftCard;
            dealResult.RightCard = rightCard;

            var comparisonResult = leftCard.CompareByProperty(rightCard, propertyName);

            dealResult.Verdict = MapComparisonResultToVerdict(comparisonResult);

            return dealResult;
        }

        private Verdict MapComparisonResultToVerdict(int comparisonResult)
        {
            return comparisonResult switch
            {
                0 => Verdict.Tie,

                1 => Verdict.Left,

                -1 => Verdict.Right,

                _ => throw new ArgumentException(nameof(comparisonResult)),
            };
        }
    }
}