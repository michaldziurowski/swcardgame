using System.Collections.Generic;

namespace SWCardGame.Persistence.Entities
{
    public class CardDefinition
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Properties { get; set; }
        public ICollection<Card> Cards { get; set; }
    }

}