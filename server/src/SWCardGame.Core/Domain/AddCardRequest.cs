using System.Collections.Generic;

namespace SWCardGame.Core.Domain
{
    public class AddCardRequest
    {
        public string Name { get; set; }
        public int DefinitionId { get; set; }
        public IEnumerable<Property> Properties { get; set; }
    }
}