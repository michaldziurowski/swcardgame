using Newtonsoft.Json;

namespace SWCardGame.WebApi.Contracts.Cards
{
    public class CardProperty
    {
        [JsonProperty(Required = Required.DisallowNull)]
        public string Name { get; set; }
        public int Value { get; set; }
    }
}