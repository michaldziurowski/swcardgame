namespace SWCardGame.Core.Domain
{
    public class DealResult
    {
        public Card LeftCard { get; set; }
        public Card RightCard { get; set; }
        public Verdict Verdict { get; set; }
    }
}