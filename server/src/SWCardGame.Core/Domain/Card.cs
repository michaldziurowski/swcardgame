using System;
using System.Collections.Generic;
using System.Linq;

namespace SWCardGame.Core.Domain
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CardDefinition Definition { get; set; }
        public IEnumerable<Property> Properties { get; set; }

        public int CompareByProperty(Card second, string propertyName)
        {
            if (!Definition.Equals(second.Definition))
            {
                throw new ArgumentException("Cannot compare cards of different types.");
            }

            var firstCardProperty = Properties.FirstOrDefault(p => p.Name == propertyName);
            var secondCardProperty = second.Properties.FirstOrDefault(p => p.Name == propertyName);
            if (firstCardProperty == null || secondCardProperty == null)
            {
                throw new ArgumentException($"Property {propertyName} doesn't exist on at least one of the cards.");
            }

            if (firstCardProperty.Value > secondCardProperty.Value)
            {
                return 1;
            }

            if (firstCardProperty.Value < secondCardProperty.Value)
            {
                return -1;
            }

            return 0;
        }
    }
}