using System.Collections.Generic;

namespace SWCardGame.Core.Domain
{
    public class UpdateCardRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Property> Properties { get; set; }
    }
}