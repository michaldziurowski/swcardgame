namespace SWCardGame.Persistence.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DefinitionId { get; set; }
        public CardDefinition Definition { get; set; }
        public string Properties { get; set; }
    }

}