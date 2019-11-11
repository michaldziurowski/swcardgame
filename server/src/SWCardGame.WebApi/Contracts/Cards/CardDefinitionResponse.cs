using System.Collections.Generic;

namespace SWCardGame.WebApi.Contracts.Cards
{
    public class CardDefinitionResponse
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Properties { get; set; }
    }
}