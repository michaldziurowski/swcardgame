using System.Collections.Generic;

namespace SWCardGame.WebApi.Contracts.Game
{
    public class DealResponseCard
    {
        public string Name { get; set; }
        public CardResult Result { get; set; }
        public List<CardProperty> Properties { get; set; } = new List<CardProperty>();
    }

    public enum CardResult
    {
        Tie = 1,
        Winner = 2,
        Loser = 3,

    }
}