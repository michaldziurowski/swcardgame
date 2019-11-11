using System.Collections.Generic;

namespace SWCardGame.Core.Domain
{
    public class CardDefinition
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Properties { get; set; }

        public bool Equals(CardDefinition card)
        {
            return this.Key == card?.Key;
        }
    }
}