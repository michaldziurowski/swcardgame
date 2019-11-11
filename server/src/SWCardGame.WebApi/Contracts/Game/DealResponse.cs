namespace SWCardGame.WebApi.Contracts.Game
{
    public class DealResponse
    {
        public DealResponseCard LeftCard { get; set; }
        public DealResponseCard RightCard { get; set; }
        public int Verdict { get; set; }
    }
}