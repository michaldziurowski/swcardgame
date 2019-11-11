using System.Collections.Generic;

namespace SWCardGame.WebApi.Contracts.Cards
{
    public class UpdateCardRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CardProperty> Properties { get; set; }
    }
}