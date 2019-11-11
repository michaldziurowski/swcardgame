using System.Collections.Generic;
using Newtonsoft.Json;

namespace SWCardGame.WebApi.Contracts.Cards
{
    public class AddCardRequest
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public string Name { get; set; }
        public int DefinitionId { get; set; }
        [JsonProperty(Required = Required.DisallowNull)]
        public IEnumerable<CardProperty> Properties { get; set; }

    }
}