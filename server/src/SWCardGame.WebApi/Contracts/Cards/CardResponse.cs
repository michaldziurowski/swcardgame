using System.Collections.Generic;

namespace SWCardGame.WebApi.Contracts.Cards
{
    public class CardResponse
    {
        public int Id { get; set; }
        public int DefinitionId { get; set; }
        public string Name { get; set; }
        public IEnumerable<CardProperty> Properties { get; set; }
    }
}